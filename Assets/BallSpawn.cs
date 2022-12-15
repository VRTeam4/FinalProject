using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Mirror;

public class BallSpawn : NetworkBehaviour
{
    public Transform spawnPoint;
    public float spawnDelay = 2.0f;
    public int id_ON_SERVER;
    private GameManager gameManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void BallRemoved()
    {
        StartCoroutine(SpawnAfterDelay());
    }
    
    IEnumerator SpawnAfterDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnBall(gameManager.small, gameManager.tBall);
    }

    [Command(requiresAuthority = false)]
    public void setId(int id)
    {
        id_ON_SERVER = id;
    }

    [Command(requiresAuthority = false)]
    public void SpawnBall(bool small, bool tball)
    {
        GameObject newBall = null;
        gameManager = FindObjectOfType<GameManager>();
        if (small) {
            newBall = Instantiate(gameManager.smallBallPrefab, spawnPoint.position, spawnPoint.rotation)
                .GameObject();
        } else if (tball) {
            newBall = Instantiate(gameManager.tBallPrefab, spawnPoint.position, new Quaternion(0f,135f,0f,-90f))
                .GameObject();
        } else {
            newBall = Instantiate(gameManager.ballPrefab, spawnPoint.position, spawnPoint.rotation)
                .GameObject();
        }
        NetworkServer.Spawn(newBall);
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
