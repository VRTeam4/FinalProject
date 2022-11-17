using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallSpawn : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnBall();
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}