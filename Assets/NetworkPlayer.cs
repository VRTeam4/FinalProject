using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Mirror;
using Unity.XR.CoreUtils;

namespace QuickStart
{
    public class NetworkPlayer : NetworkBehaviour
    {
        public Transform leftHand;
        public Transform rightHand;
        private Transform leftRig;
        private GameObject gameManager;
        public NetworkConnectionToClient connection;
        public override void OnStartLocalPlayer()
        {
            Debug.Log("TEST");
            connection = connectionToClient;
            gameManager = GameObject.Find("GameManager");
            gameManager.GetComponent<GameManager>().networkPlayer = gameObject;
            XROrigin rig = FindObjectOfType<XROrigin>();
            leftRig = rig.transform.Find("CameraOffset/LeftHand (Smooth locomotion)");
        }

        // Update is called once per frame
        void Update()
        {
            if (!isLocalPlayer) { return; }
            MapPosition(leftHand, leftRig);
            
        }
        void MapPosition(Transform target, Transform rigTransform)
        {
            target.position = rigTransform.position;
            target.rotation = rigTransform.rotation;
        }
        [Command]
        public void CmdPickupItem(NetworkIdentity item)
        {
            item.AssignClientAuthority(connectionToClient);
        }
    }
}
