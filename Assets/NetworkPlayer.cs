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
        public Transform head;
        private Transform leftRig;
        private Transform rightRig;
        private Transform cameraRig;
        private GameObject gameManager;
        public NetworkConnectionToClient connection;
        public override void OnStartLocalPlayer()
        {
            connection = connectionToClient;
            gameManager = GameObject.Find("GameManager");
            int numOtherPlayer = GameObject.FindGameObjectsWithTag("Player").Length;
            numOtherPlayer = numOtherPlayer % gameManager.GetComponent<GameManager>().playerSpawnPos.Count;
            gameManager.GetComponent<GameManager>().networkPlayer = gameObject;
            gameManager.GetComponent<GameManager>().teamID = numOtherPlayer;
            XROrigin rig = FindObjectOfType<XROrigin>();
            rig.transform.rotation = gameManager.GetComponent<GameManager>().playerSpawnPos[numOtherPlayer].rotation;
            rig.transform.position = gameManager.GetComponent<GameManager>().playerSpawnPos[numOtherPlayer].position;
            cameraRig = rig.transform.Find("CameraOffset/Main Camera");
            leftRig = rig.transform.Find("CameraOffset/LeftHand (Smooth locomotion)");
            rightRig = rig.transform.Find("CameraOffset/RightHand (Teleport Locomotion)");
            head.gameObject.layer = LayerMask.NameToLayer("CameraIgnore");
            ChangeLayersRecursively(head, "CameraIgnore");
        }

        private void ChangeLayersRecursively(Transform trans , string name)
        {
            foreach (Transform child in trans)
            {
                child.gameObject.layer = LayerMask.NameToLayer(name);
                ChangeLayersRecursively(child, name);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!isLocalPlayer) { return; }
            MapPosition(head, cameraRig);
            MapPosition(leftHand, leftRig);
            MapPosition(rightHand, rightRig);
        }
        void MapPosition(Transform target, Transform rigTransform)
        {
            target.position = rigTransform.position;
            target.rotation = rigTransform.rotation;
        }
        [Command]
        public void CmdPickupItem(NetworkIdentity item)
        {
            item.RemoveClientAuthority();
            item.AssignClientAuthority(connectionToClient);
        }
    }
}
