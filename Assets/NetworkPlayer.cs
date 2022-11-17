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
        public override void OnStartLocalPlayer()
        {
            //Camera.main.transform.SetParent(transform);
            //Camera.main.transform.localPosition = new Vector3(0, 0, 0);
            XROrigin rig = FindObjectOfType<XROrigin>();
            Debug.Log(rig);
            leftRig = rig.transform.Find("CameraOffset/LeftHand (Smooth locomotion)");
            //Debug.Log("okay");
            Debug.Log(leftRig);
        }

        // Update is called once per frame
        void Update()
        {
            if (!isLocalPlayer) { return; }

            // float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 110.0f;
            // float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * 4f;

            // transform.Rotate(0, moveX, 0);
            // transform.Translate(0, 0, moveZ);
            MapPosition(leftHand, leftRig);
            //MapPosition(rightHand, XRNode.RightHand);
            
        }
        void MapPosition(Transform target, Transform rigTransform)
        {
            //InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
            //InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Quaternion rotation);

            target.position = rigTransform.position;
            target.rotation = rigTransform.rotation;
            //Debug.Log(position);
            //target.rotation = rotation;
        }

        //void MapPosition(Transform target, XRNode node)
        //{
            //InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
            //InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Quaternion rotation);

            //target.position = position;
            //Debug.Log(position);
            //target.rotation = rotation;
        //}
    }
}
