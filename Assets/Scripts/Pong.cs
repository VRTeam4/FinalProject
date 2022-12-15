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
    public GameManager gameManager;
    public GameObject soloCupPrefab;
    public int cupsRemaining;

    public List<BallSpawn> ballSpawn;

    public GameObject startButton;
    public GameObject startButtonTwo;

    public int teamZeroScore;
    public int teamOneScore;

    
    // Start is called before the first frame update
    public void StartPong()
    {
        cupsRemaining = 0;
        teamOneScore = 0;
        teamZeroScore = 0;
        TellServerRemoveButton();
        int id = 0;
        foreach (var spawn in ballSpawn)
        {
            spawn.setId(id);
            spawn.SpawnBall(false);
            id += 1;
        }

        foreach (var pos in spawnPositions)
        {
            int TeamID = 0;
            if (cupsRemaining >= 10) {
                TeamID = 1;
            }
            SpawnCup(pos.position, TeamID);
            cupsRemaining += 1;
            started = true;
        }
    }

    [Command (requiresAuthority=false)]
    public void TellServerRemoveButton()
    {
        RemoveButton();
    }

    [Command (requiresAuthority=false)]
    public void ApplyEffect(int TeamID) {
        ApplyEffectHelper(TeamID);
    }

    [ClientRpc]
    public void ApplyEffectHelper(int TeamID) {
        if (gameManager.teamID != TeamID) {
             gameManager.AddEffect();
        }
    }

    [ClientRpc]
    public void RemoveButton() {
        startButton.SetActive(false);
        startButtonTwo.SetActive(false);
    }

    [ClientRpc]
    public void EnableButton() {
        startButton.SetActive(true);
        startButtonTwo.SetActive(true);
    }

    [Command(requiresAuthority = false)]
    public void GameOver()
    {
        gameManager.RemoveActiveEffects();
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
        EnableButton();
        Debug.Log("The game is over!");
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

    public void PointScored(int TeamID)
    {
        ApplyEffect(TeamID);
        if (TeamID == 0) {
            teamZeroScore += 1;
        } else if (TeamID == 1) {
            teamOneScore += 1;
        }
        if (teamOneScore == 10 || teamZeroScore == 10)
        {
            GameOver();
        }
    }
    
    [Command(requiresAuthority = false)]
    void SpawnCup(Vector3 position, int TeamID)
    {
        GameObject newCup = Instantiate(soloCupPrefab, position, soloCupPrefab.transform.rotation).GameObject();
        NetworkServer.Spawn(newCup);
        newCup.GetComponent<SoloCup>().SetTeamID(TeamID);
    }
}
