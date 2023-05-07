using Mirror;
using System;
using UnityEngine;

public class UpdatePlayerPreferredName : NetworkBehaviour
{

    [SyncVar(hook = nameof(OnPlayerNameChanged))]
    public string playerName = "";

    public TMPro.TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        if(isLocalPlayer)
        {
            //TODO: Don't do this find object please, do another 
            GameObject canvas = GameObject.Find("UsernameCanvas");
            CmdSetPlayerName(canvas.GetComponentInChildren<PlayerNamePreference>().playerPreferredName);
            canvas.SetActive(false);
        }
    }

    [Command]
    public void CmdSetPlayerName(string name)
    {
        playerName = name;
        RpcUpdatePlayerName(playerName);
    }

    [ClientRpc]
    private void RpcUpdatePlayerName(string name)
    {
        playerName = name;
    }

    private void OnPlayerNameChanged(string oldName, string newName)
    {
        textMeshPro.text = newName;
    }
}
