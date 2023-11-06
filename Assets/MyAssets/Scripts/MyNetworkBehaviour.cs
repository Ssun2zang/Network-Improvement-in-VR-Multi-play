using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MyNetworkBehaviour : NetworkBehaviour
{
    //
    public Rigidbody objectRigidbody;
    private NetworkTransformClient cl;


    private void Start()
    {
        // Get the Rigidbody component attached to this object.
        objectRigidbody = GetComponent<Rigidbody>();
        cl = GetComponent<NetworkTransformClient>();
    }


    [ServerRpc]
    public void SetTransformServerRpc(bool isTrue)
    {
        if (IsServer)
        {
            // Update the networked object's linear velocity, position, and angular velocity on the server.
            cl.enabled = isTrue;
            Debug.LogError("Server Transform");

            // Replicate the changes to all clients.
            UpdateTransformClientRpc(isTrue);
        }
    }

    [ClientRpc]
    private void UpdateTransformClientRpc(bool isTrue)
    {
        // This method will be called on all clients to update the object's linear velocity, position, and angular velocity.
        // Ensure it's not called on the owner client to avoid double updates.
        if (!IsOwner)
        {
            cl.enabled = isTrue;
            Debug.LogError("Client Transform");
        }
    }


    [ServerRpc]
    public void SetVelocityAndPositionServerRpc(Vector3 linearVelocity, Vector3 position, Vector3 angularVelocity, Quaternion rotation)
    {
        if (IsServer)
        {
            // Update the networked object's linear velocity, position, and angular velocity on the server.
            objectRigidbody.velocity = linearVelocity;
            //transform.position = position;
            objectRigidbody.angularVelocity = angularVelocity;
            //transform.rotation = rotation;
            Debug.LogError("Server");
            Debug.Log(linearVelocity+",lv");
            Debug.Log(position);
            Debug.Log(angularVelocity);

            // Replicate the changes to all clients.
            UpdateVelocityAndPositionOnClientsClientRpc(linearVelocity, position, angularVelocity, rotation);
        }
    }

    [ClientRpc]
    private void UpdateVelocityAndPositionOnClientsClientRpc(Vector3 linearVelocity, Vector3 position, Vector3 angularVelocity, Quaternion rotation)
    {
        // This method will be called on all clients to update the object's linear velocity, position, and angular velocity.
        // Ensure it's not called on the owner client to avoid double updates.
        if (!IsOwner)
        {

            objectRigidbody.isKinematic = true;
            objectRigidbody.isKinematic = false;
            objectRigidbody.velocity = linearVelocity;
            // objectRigidbody.AddForce(linearVelocity, ForceMode.Impulse);
            // transform.position = position;
            // transform.rotation = rotation;
            objectRigidbody.angularVelocity = angularVelocity;
            //objectRigidbody.AddTorque(angularVelocity, ForceMode.Impulse);

            Debug.LogError("Client");
            Debug.Log(linearVelocity);
            Debug.Log(objectRigidbody.velocity);
            Debug.Log("----");
            //Debug.Log(position);
            //Debug.Log(angularVelocity);
        }
    }
}