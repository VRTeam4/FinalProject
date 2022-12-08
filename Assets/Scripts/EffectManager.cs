using UnityEngine;
 using UnityEngine.Rendering;
 
 public class EffectManager : MonoBehaviour {
 
     private Volume globalVolume;
     
     [System.Serializable]
     public struct VisualEffects {
         public VolumeProfile normal;
         public VolumeProfile wide;
         public VolumeProfile crazy;
     }

     public VisualEffects visualEffects;
     
     // Use this for initialization
     void Start ()
     {
         globalVolume = gameObject.GetComponent<Volume>();
         globalVolume.profile = visualEffects.normal;

     }

     public void SetView(VolumeProfile effect) {
        globalVolume.profile = effect;
     }
     
     // Update is called once per frame
     void Update () {
         
     }
 }