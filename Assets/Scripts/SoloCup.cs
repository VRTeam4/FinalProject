using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SoloCup : NetworkBehaviour
{
    private Pong gameReference;
    
    [SyncVar]
    public int TeamID;
    // Start is called before the first frame update
    void Start()
    {
        gameReference = GameObject.Find("GameManager").GetComponent<GameManager>().pong;;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ClientRpc]
    public void SetTeamID(int id) {
        TeamID = id;
    }

    public void CupScored(Collider other)
    {
        if (other.CompareTag("PongBall")) {
            gameReference.PointScored(TeamID);
            destroy();
            other.gameObject.GetComponent<PongBall>().destroy();
        }
    }

    [Command(requiresAuthority = false)]
    public void destroy() {
        //Destroy(gameObject);
        NetworkServer.Destroy(gameObject);
    }
}
