using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MiniGame : NetworkBehaviour
{
    public float score = 0;
    public bool started = false;
    public bool ended = false;

    [Command(requiresAuthority = false)]
    public void GameOver()
    {
        GameObject[] balls =  GameObject.FindGameObjectsWithTag("PongBall");
        GameObject[] cups = GameObject.FindGameObjectsWithTag("PongCup");
        foreach (var ball in balls)
        {
            NetworkServer.Destroy(ball);
        }
        foreach (var cup in cups)
        {
            NetworkServer.Destroy(cup);
        }
        ended = true;
        Debug.Log("The game is over!");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
