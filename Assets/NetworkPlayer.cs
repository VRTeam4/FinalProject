using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Mirror;

namespace QuickStart
{
    public class NetworkPlayer : NetworkBehaviour
    {
        public Transform leftHand;
        public Transform rightHand;
        public override void OnStartLocalPlayer()
        {
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.localPosition = new Vector3(0, 0, 0);
        }

        // Update is called once per frame
        void Update()
        {
            //if (!isLocalPlayer) { return; }

            // float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 110.0f;
            // float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * 4f;

            // transform.Rotate(0, moveX, 0);
            // transform.Translate(0, 0, moveZ);
            MapPosition(leftHand, XRNode.LeftHand);
            MapPosition(rightHand, XRNode.RightHand);
            
        }

        void MapPosition(Transform target, XRNode node)
        {
            InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
            //InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Quaternion rotation);

            target.position = position;
            Debug.Log(position);
            //target.rotation = rotation;
        }
    }
}
