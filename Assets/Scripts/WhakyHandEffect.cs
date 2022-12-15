using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit;

#if ENABLE_VR || (UNITY_GAMECORE && INPUT_SYSTEM_1_4_OR_NEWER)
using UnityEngine.InputSystem.XR;
#endif

public class WhakyHandEffect : Effect
{
    EffectManager em;
    
    GameObject[] audioSources;
    public InputActionProperty leftRotation;
    public InputActionProperty rightRotation;
    public ActionBasedController playerXRLeft;
    public ActionBasedController playerXRRight;

    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public override void ApplyEffect() {
        playerXRLeft.rotationAction = rightRotation;
        playerXRRight.rotationAction = leftRotation;
    }
    public override void RemoveEffect() {
        playerXRLeft.rotationAction = leftRotation;
        playerXRRight.rotationAction = rightRotation;
    }
}
