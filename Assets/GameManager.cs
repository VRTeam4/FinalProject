using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public GameObject networkPlayer;
    public Pong pong;
    public List<Transform> playerSpawnPos;
    public List<Effect> effects;
    public int teamID;
    public List<string> ballTypes;
    public GameObject ballPrefab;
    private List<Effect> curEffects;


    // Start is called before the first frame update
    void Start()
    {
        curEffects = new List<Effect>(effects);
        ballTypes = new List<string>();
        ballTypes.Add("Normal");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddEffect() {
        Debug.Log("ADDING EFFECT");
        if (curEffects.Count > 0) {
        System.Random rnd = new System.Random();
        int num_effects = curEffects.Count;
        int index = rnd.Next(0,num_effects);
            Effect toApply = curEffects[index];
            toApply.ApplyEffect();
            while (curEffects.Contains(toApply)) {
                curEffects.Remove(toApply);
            }
        } else {
            Debug.Log("NO GOOD");
        }
    }

    [ClientRpc]
    public void RemoveActiveEffects() {
        foreach (Effect effect in effects) {
        effect.RemoveEffect();
       }
       curEffects = new List<Effect>(effects);
       Debug.Log(curEffects.Count);
    }
}
