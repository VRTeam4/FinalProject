using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallSpawn : MonoBehaviour
{
    public GameObject ballPrefab;
    public float spawnDelay = 2.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnBall();
    }

    public void BallRemoved()
    {
        StartCoroutine(SpawnAfterDelay());
    }
    
    IEnumerator SpawnAfterDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnBall();
    }

    public void SpawnBall()
    {
        GameObject newBall = Instantiate(ballPrefab, transform.position, transform.rotation)
            .GameObject();
        newBall.GetComponent<XRGrabInteractable>().selectEntered.AddListener(eventArgs => BallRemoved());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
