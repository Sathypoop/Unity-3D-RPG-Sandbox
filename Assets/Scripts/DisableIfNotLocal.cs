using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfNotLocal : NetworkBehaviour
{

    [SerializeField]
    private List<MonoBehaviour> monoBehaviours= new List<MonoBehaviour>();

    // Start is called before the first frame update
    void Start()
    {
        if(!isLocalPlayer)
        {
            for(int i = 0; i < monoBehaviours.Count; i++)
            {
                monoBehaviours[i].enabled = false;
            }
        }
    }
}
