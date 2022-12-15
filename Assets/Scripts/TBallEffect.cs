using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBallEffect : Effect
{
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    public override void ApplyEffect() {
        Debug.Log("T BALL");
        gameManager.small = false;
        gameManager.tBall = true;
    }

    public override void RemoveEffect()
    {
        gameManager.tBall = false;
    }
}
