using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject networkPlayer;
    public Pong pong;
    public List<Transform> playerSpawnPos;
    public List<Effect> effects;
    public int teamID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddEffect() {
        System.Random rnd = new System.Random();
        int num_effects = effects.Count;
        int index = rnd.Next(0,num_effects);
        Effect toApply = effects[index];
        toApply.ApplyEffect();
        while (effects.Contains(toApply)) {
            effects.Remove(toApply);
        }
    }
}
