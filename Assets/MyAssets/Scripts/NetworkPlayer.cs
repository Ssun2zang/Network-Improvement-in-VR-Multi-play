using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkPlayer : NetworkBehaviour
{
    public Transform root;
    public Transform head;
    public Transform rightHand;
    public Transform leftHand;
    public Transform Avatar;

    public Renderer[] meshToDisable;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            foreach (var item in meshToDisable)
            {
                item.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        root.position = VRRigReferences.SingleTon.root.position;
        root.rotation = VRRigReferences.SingleTon.root.rotation;

        head.position = VRRigReferences.SingleTon.head.position;
        head.rotation = VRRigReferences.SingleTon.head.rotation;

        rightHand.position = VRRigReferences.SingleTon.rightHand.position;
        rightHand.rotation = VRRigReferences.SingleTon.rightHand.rotation;

        leftHand.position = VRRigReferences.SingleTon.leftHand.position;
        leftHand.rotation = VRRigReferences.SingleTon.leftHand.rotation;

    }
}
