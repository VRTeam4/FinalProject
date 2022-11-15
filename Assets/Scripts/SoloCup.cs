using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloCup : MonoBehaviour
{
    private Pong gameReference;
    // Start is called before the first frame update
    void Start()
    {
        gameReference = transform.parent.GetComponent<Pong>();
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
            Destroy(gameObject);
        }
    }
}
