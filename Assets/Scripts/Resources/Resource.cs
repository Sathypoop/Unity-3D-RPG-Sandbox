using Mirror;
using UnityEngine;

public class Resource : NetworkBehaviour
{
    // I.e Wood, Stone, Grass, Ore
    public string resourceType = "";

    private const int respawnTimer = 2;
    private float respawnTime = 0;
    private bool hasSpawned = true;

    [SyncVar(hook = nameof(OnPlayerInteraction))]
    public bool isBeingInteractedWith = false;


    // Update is called once per frame
    void Update()
    {
        if(!hasSpawned)
        {
            if (RespawnCheck())
                RespawnResource();
        }
    }

    private bool RespawnCheck()
    {
        bool isReady = false;
        
        respawnTime += Time.deltaTime;
        
        if(respawnTimer < respawnTime)
        {
            respawnTime= 0;
            isReady = true;
        }

        return isReady;
    }

    private void RespawnResource()
    {
        hasSpawned= true;
        RpcRespawnResource();
    }

    [ClientRpc]
    public void RpcRespawnResource()
    {
        hasSpawned = true;
        isBeingInteractedWith = false;
        GetComponent<Collider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }

    public void CmdInteractWithResource()
    {
        isBeingInteractedWith = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        hasSpawned = false;
        RpcUpdateResourceInteraction(true);
    }

    [ClientRpc]
    private void RpcUpdateResourceInteraction(bool interaction)
    {
        isBeingInteractedWith = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    private void OnPlayerInteraction(bool oldInteraction, bool interaction)
    {
        isBeingInteractedWith = interaction;
    }
}
