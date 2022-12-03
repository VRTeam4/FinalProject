using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AutoJoin : MonoBehaviour
{
    public NetworkManager networkManager;
    public string ipAddress;
    // Start is called before the first frame update
    void Start()
    {
        // If not headless mode
        if (!Application.isBatchMode)
        {
            Debug.Log("CLIENT CONNECTING");
            JoinRemoteServer();
        } else
        {
            Debug.Log("STARTING SERVER");
        }
    }

    // Update is called once per frame
    void JoinRemoteServer()
    {
        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();
    }
}
