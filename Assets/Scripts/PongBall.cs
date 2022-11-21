using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UI;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PongBall : MonoBehaviour
{
    public float Lifetime = 10.0f;
    
    // Start is called before the first frame update
    void Start() {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    public void UnFreeze() {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
    
    public void StartCountdown()
    {
        StartCoroutine(DestroyAfterLifetime());
    }



    IEnumerator DestroyAfterLifetime()
    {
        // May want to improve this implementation
        yield return new WaitForSeconds(Lifetime/2);
        if (GetComponent<XRGrabInteractable>().isSelected)
        {
            StartCoroutine(DestroyAfterLifetime());
            yield break;
        }
        
        yield return new WaitForSeconds(Lifetime/2);
        if (GetComponent<XRGrabInteractable>().isSelected)
        {
            StartCoroutine(DestroyAfterLifetime());
            yield break;
        }
        
        Destroy(gameObject);
    }
}
