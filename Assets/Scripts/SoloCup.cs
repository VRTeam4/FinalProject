using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SoloCup : NetworkBehaviour
{
    private Pong gameReference;
    // Start is called before the first frame update
    void Start()
    {
        gameReference = GameObject.Find("GameManager").GetComponent<GameManager>().pong;;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CupScored(Collider other)
    {
        Debug.Log("TriggerEntered!");
        if (other.CompareTag("PongBall")) {
            Debug.Log("Point Scored!");
            gameReference.PointScored();
            destroy();
            other.gameObject.GetComponent<PongBall>().destroy();
        }
    }

    [Command(requiresAuthority = false)]
    public void destroy() {
        Destroy(gameObject);
    }
}
