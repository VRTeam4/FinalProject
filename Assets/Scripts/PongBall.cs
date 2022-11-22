using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UI;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Mirror;
using QuickStart;

public class PongBall : NetworkBehaviour
{
    public float Lifetime = 10.0f;
    private GameObject gameManager;
    // Start is called before the first frame update
    void Start() {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    public void UnFreeze() {
        gameManager = GameObject.Find("GameManager");
        //Debug.Log(gameManager.GetComponent<GameManager>().networkPlayer.GetComponent<NetworkPlayer>());
        //gameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(gameManager.GetComponent<GameManager>().networkPlayer.GetComponent<QuickStart.NetworkPlayer>().connection);
        gameManager.GetComponent<GameManager>().networkPlayer.GetComponent<QuickStart.NetworkPlayer>().CmdPickupItem(gameObject.GetComponent<NetworkIdentity>());
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
