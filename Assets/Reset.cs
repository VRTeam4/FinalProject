using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Reset : NetworkBehaviour
{
    public NetworkManager networkManager;
    public string ipAddress;


    // Update is called once per frame
    void JoinRemoteServer()
    {
        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();
        
    }

    [Command (requiresAuthority=false)]
    public void cmdLeaveRemoteServer()
    {
        LeaveRemoteServer();
    }

    [ClientRpc]
    void LeaveRemoteServer()
    {
        networkManager.StopClient();
        JoinRemoteServer();
    }
 }
