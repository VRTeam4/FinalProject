using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UI;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Mirror;
using QuickStart;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Mirror;

public class PongBall : NetworkBehaviour
{
    public float Lifetime = 10.0f;
    private GameObject gameManager;
    public BallSpawn spawnPoint;

    [SyncVar]
    public bool grabbed;

    [SyncVar]
    public int spawnID;

    string ballType;


    // Start is called before the first frame update
    void Start() {
        gameManager = GameObject.Find("GameManager");
        List<string> ballTypes = gameManager.GetComponent<GameManager>().ballTypes;
        System.Random rnd = new System.Random();
        int index = rnd.Next(0,ballTypes.Count); 
        ballType = ballTypes[index];

        if (spawnID != -999) {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        
    }

    [Command(requiresAuthority = false)]
    public void SpawnBall(Vector3 velocity)
    {
        GameObject newBall = Instantiate(gameManager.GetComponent<GameManager>().ballPrefab, transform.position, transform.rotation).GameObject();        
        newBall.GetComponent<PongBall>().spawnID = -999;
        NetworkServer.Spawn(newBall);
        newBall.GetComponent<PongBall>().Unfreeze();
        newBall.GetComponent<PongBall>().setVelocity(velocity);
    }

    public void BallReleased() {
        if (ballType == "Split") {
            StartCoroutine(SpawnAfterDelay());
        }
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

    
    [ClientRpc]
    public void setVelocity(Vector3 velocity) {
        GetComponent<Rigidbody>().velocity = velocity;
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

    IEnumerator SpawnAfterDelay()
    {
        // May want to improve this implementation
        yield return new WaitForSeconds(0.1f);
    
        Debug.Log(GetComponent<Rigidbody>().velocity);
        SpawnBall(GetComponent<Rigidbody>().velocity);    
    }
}
