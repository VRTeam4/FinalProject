using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Mirror;

public class BallSpawn : NetworkBehaviour
{
    public GameObject ballPrefab;
    public GameObject smallBallPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2.0f;
    public int id_ON_SERVER;
    private GameManager gameManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //SpawnBall();
        gameManager = FindObjectOfType<GameManager>();
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
        List<string> ballTypes = gameManager.ballTypes;
        System.Random rnd = new System.Random();
        int index = rnd.Next(0,ballTypes.Count); 
        string ballType = ballTypes[index];
        GameObject newBall = null;
        if (ballType == "Small") {
            Debug.Log("SMALL");
            newBall = Instantiate(smallBallPrefab, spawnPoint.position, spawnPoint.rotation)
            .GameObject();
        } else {
            newBall = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation)
            .GameObject();
        }
        NetworkServer.Spawn(newBall);
        newBall.GetComponent<PongBall>().spawnID = id_ON_SERVER;
        newBall.GetComponent<PongBall>().ballType = ballType;
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
