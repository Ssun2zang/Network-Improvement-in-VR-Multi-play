using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.Netcode;

public class MyGrabInteractable : XRGrabInteractable
{
    private Rigidbody rb;
    private Vector3 lastPosition;
    private Quaternion lastRotation;
    private Vector3 initialVelocity;
    private Vector3 initialAngularVelocity;
    private NetworkTransformClient cl;
    private MyNetworkBehaviour manipulator;
    private NetworkObject nob;




    private void Start()
    {
        // Get the NetworkedObjectManipulator component attached to the same object.
        manipulator = GetComponent<MyNetworkBehaviour>();
        rb = GetComponent<Rigidbody>();
        cl = GetComponent<NetworkTransformClient>();
        nob = GetComponent<NetworkObject>();
    }



    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        nob.ChangeOwnership(NetworkManager.Singleton.LocalClientId);
        base.OnSelectEntered(interactor);
        // 물체가 잡힐 때 실행되는 코드

        // NetworkTransformClient 컴포넌트를 활성화
        if (cl != null)
        {
            manipulator.SetTransformServerRpc(true);
            // lastPosition = rb.position;
            // lastRotation = rb.rotation;
        }
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        // Debug.LogWarning(base.GetAssit() + "assit");
        // 물체가 놓아질 때 실행되는 코드
        Vector3 finalPosition = rb.position;
        // Vector3 finalVelocity = (finalPosition - lastPosition) / Time.fixedDeltaTime;
        Vector3 finalVelocity = base.GetDetachVelocity();
        Debug.LogWarning(finalVelocity);

        Quaternion finalRotation = rb.rotation;
        
        // Quaternion deltaRotation = finalRotation * Quaternion.Inverse(lastRotation);
        // Vector3 finalAngularVelocity = deltaRotation.eulerAngles / Time.fixedDeltaTime;
        Vector3 finalAngularVelocity = base.GetDetachAngularVelocity();
        Debug.LogWarning(finalAngularVelocity);

        // NetworkTransformClient 컴포넌트를 비활성화
        if (cl != null)
        {
            manipulator.SetTransformServerRpc(false);
        }

        manipulator.SetVelocityAndPositionServerRpc(finalVelocity, finalPosition, finalAngularVelocity, finalRotation);

        nob.RemoveOwnership();

    }

    private void FixedUpdate()
    {
        // lastPosition = rb.position;
        // lastRotation = rb.rotation;
        //Debug.Log(rb.velocity);
    }
}