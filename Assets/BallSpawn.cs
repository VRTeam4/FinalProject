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
    public int id_ON_SERVER;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //SpawnBall();
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

    [Command(requiresAuthority = false)]
    public void setId(int id)
    {
        id_ON_SERVER = id;
    }

    [Command(requiresAuthority = false)]
    public void SpawnBall()
    {
        Debug.Log("BALL SPAWN");
        GameObject newBall = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation)
            .GameObject();
        NetworkServer.Spawn(newBall);
        Debug.Log("ID");
        Debug.Log(id_ON_SERVER);
        newBall.GetComponent<PongBall>().spawnID = id_ON_SERVER;
    }

    // public override void OnStartClient()

    // {
    //     if (isServer)
    //     {
    //         SpawnBall();
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        
    }
}
