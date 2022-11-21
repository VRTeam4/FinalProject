using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Mirror;

public class BallSpawn : NetworkBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //SpawnBall();
    }

    public void BallRemoved(SelectEnterEventArgs args)
    {
        args.interactableObject.selectEntered.RemoveAllListeners();
        StartCoroutine(SpawnAfterDelay());
    }
    
    IEnumerator SpawnAfterDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnBall();
    }

    public void SpawnBall()
    {
        GameObject newBall = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation)
            .GameObject();
        newBall.GetComponent<XRGrabInteractable>().selectEntered.AddListener(eventArgs => BallRemoved(eventArgs));
        if (isServer)
        {
            NetworkServer.Spawn(newBall);
        }
    }

    public override void OnStartServer()

    {
        SpawnBall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
