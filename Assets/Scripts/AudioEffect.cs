using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : Effect
{
    EffectManager em;
    
    GameObject[] audioSources;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSources = GameObject.FindGameObjectsWithTag("AudioSource");
    }

    // Update is called once per frame
    public override void ApplyEffect() {
       foreach (GameObject audioSource in audioSources) {
        System.Random rnd = new System.Random();
        int index = rnd.Next(0,2);        
        audioSource.GetComponent<AudioSource>().pitch = (1.5f - index) * audioSource.GetComponent<AudioSource>().pitch;
        audioSource.GetComponent<AudioSource>().volume = 1.2f * audioSource.GetComponent<AudioSource>().volume;
       }
    }
}
