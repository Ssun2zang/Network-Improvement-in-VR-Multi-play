using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MyNetworkBehaviour : NetworkBehaviour
{
    //
    public Rigidbody objectRigidbody;
    private NetworkTransformClient cl;
    private NetworkObject nob;


    private void Start()
    {
        // Get the Rigidbody component attached to this object.
        objectRigidbody = GetComponent<Rigidbody>();
        cl = GetComponent<NetworkTransformClient>();
        nob = GetComponent<NetworkObject>();
    }


    [ServerRpc(RequireOwnership = false)]
    public void changeDiscOwnershipServerRpc(ulong id)
    {
        if (IsServer)
        {
            nob.ChangeOwnership(id);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void resetDiscOwnershipServerRpc()
    {
        if (IsServer)
        {
            nob.RemoveOwnership();
        }
    }

    [ServerRpc]
    public void SetTransformServerRpc(bool isTrue, Vector3 position, Quaternion rotation)
    {
        if (IsOwner)
        {
            objectRigidbody.isKinematic = true;
            // Update the networked object's linear velocity, position, and angular velocity on the server.
            cl.enabled = isTrue;
            Debug.LogError("Server Transform");
            objectRigidbody.position = position;
            objectRigidbody.rotation = rotation;


            // Replicate the changes to all clients.
            UpdateTransformClientRpc(isTrue, position, rotation);

        }
    }

    [ClientRpc]
    private void UpdateTransformClientRpc(bool isTrue, Vector3 position, Quaternion rotation)
    {
        // This method will be called on all clients to update the object's linear velocity, position, and angular velocity.
        // Ensure it's not called on the owner client to avoid double updates.
        if (!IsOwner)
        {
            objectRigidbody.isKinematic = true;
            cl.enabled = isTrue;
            Debug.LogError("Client Transform");
            objectRigidbody.position = position;
            objectRigidbody.rotation = rotation;

        }
    }


    [ServerRpc]
    public void SetVelocityAndPositionServerRpc(Vector3 linearVelocity, Vector3 position, Vector3 angularVelocity, Quaternion rotation)
    {
        if (IsServer)
        {
            objectRigidbody.isKinematic = false;
            // Update the networked object's linear velocity, position, and angular velocity on the server.
            objectRigidbody.velocity = linearVelocity;
            objectRigidbody.position = position;
            objectRigidbody.angularVelocity = angularVelocity;
            objectRigidbody.rotation = rotation;
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
            objectRigidbody.isKinematic = false;
            objectRigidbody.velocity = linearVelocity;
            // objectRigidbody.AddForce(linearVelocity, ForceMode.Impulse);
            objectRigidbody.position = position;
            objectRigidbody.rotation = rotation;
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