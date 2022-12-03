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
    public BallSpawn spawnPoint;

    [SyncVar]
    public bool grabbed;

    [SyncVar]
    public int spawnID;

    // Start is called before the first frame update
    void Start() {
        gameManager = GameObject.Find("GameManager");
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    //[Command(requiresAuthority = false)]
    public void BallGrabbed() {
        if (!grabbed) {
            grabbed = true;
            clientUnfreeze();
            spawnPoint.BallRemoved();
        }
    }

    [Command(requiresAuthority = false)]
    public void clientUnfreeze() {
        Unfreeze();
    }

    [ClientRpc]
    public void Unfreeze() {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    [Command(requiresAuthority = false)]
    public void destroy() {
        //Destroy(gameObject);
        NetworkServer.Destroy(gameObject);
    }

    public void localGrab() {
        spawnPoint = gameManager.GetComponent<GameManager>().pong.ballSpawn[spawnID];
        gameManager.GetComponent<GameManager>().networkPlayer.GetComponent<QuickStart.NetworkPlayer>().CmdPickupItem(gameObject.GetComponent<NetworkIdentity>());
        BallGrabbed();
    }
    
    [Command(requiresAuthority = false)]
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
