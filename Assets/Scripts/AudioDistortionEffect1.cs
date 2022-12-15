using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDistortionEffect1 : Effect
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
        audioSource.GetComponent<AudioDistortionFilter>().distortionLevel = 0.6f;
       }
    }

    public override void RemoveEffect()
    {
        foreach (GameObject audioSource in audioSources) {
        audioSource.GetComponent<AudioDistortionFilter>().distortionLevel = 0f;
       }
    }
}
