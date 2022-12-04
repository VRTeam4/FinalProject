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

    public List<BallSpawn> ballSpawn;

    public GameObject startButton;

    
    // Start is called before the first frame update
    public void StartPong()
    {
        RemoveButton();
        int id = 0;
        foreach (var spawn in ballSpawn)
        {
            spawn.setId(id);
            spawn.SpawnBall();
            id += 1;
        }

        foreach (var pos in spawnPositions)
        {
            SpawnCup(pos.position);
            cupsRemaining += 1;
            started = true;
        }
    }

    [ClientRpc]
    public void RemoveButton() {
        startButton.SetActive(false);
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
