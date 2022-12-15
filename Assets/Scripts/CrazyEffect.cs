using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyEffect : Effect
{
    EffectManager em;
    
    // Start is called before the first frame update
    void Start()
    {
        em = GameObject.Find("EffectManager").GetComponent<EffectManager>();
    }

    // Update is called once per frame
    public override void ApplyEffect() {
       em.SetView(em.visualEffects.crazy);
    }

    public override void RemoveEffect() {
       em.SetView(em.visualEffects.normal);
    }
}
