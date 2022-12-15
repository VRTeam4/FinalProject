using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBallEffect : Effect
{
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    public override void ApplyEffect() {
        gameManager.ballTypes.Add("Small");
    }

    public override void RemoveEffect()
    {
        gameManager.ballTypes = new List<string>();
        gameManager.ballTypes.Add("Normal");
    }
}
