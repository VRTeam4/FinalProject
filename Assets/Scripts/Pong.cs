using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Mirror;

public class Pong : MiniGame
{
    //      10
    //     9 8 
    //    7 6 5 
    //   4 3 2 1
    public List<Transform> spawnPositions;
    public GameObject soloCupPrefab;
    public int cupsRemaining = 0;

    public List<BallSpawn> ballSpawn1;
    public List<BallSpawn> ballSpawn2;

    
    // Start is called before the first frame update
    public void StartPong()
    {
        foreach (var spawn in ballSpawn1)
        {
            spawn.SpawnBall();
        }
        foreach (var spawn in ballSpawn2)
        {
            spawn.SpawnBall();
        }
        foreach (var pos in spawnPositions)
        {
            SpawnCup(pos.position);
            cupsRemaining += 1;
            started = true;
        }
    }

    // public override void OnStartClient()
    // {
    //     if (isServer)
    //     {
    //         StartPong();
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PointScored()
    {
        cupsRemaining -= 1;
        if (cupsRemaining == 0)
        {
            GameOver();
        }
    }
    
    [Command(requiresAuthority = false)]
    void SpawnCup(Vector3 position)
    {
        GameObject newCup = Instantiate(soloCupPrefab, position, soloCupPrefab.transform.rotation).GameObject();
        NetworkServer.Spawn(newCup);
    }
}
