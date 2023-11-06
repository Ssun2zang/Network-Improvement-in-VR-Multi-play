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
        objectRigidbody = GetComponent<Rigidbody>();
        cl = GetComponent<NetworkTransformClient>();
    }


    [ServerRpc(RequireOwnership = false)]
    public void SetTransformServerRpc(bool isTrue)
    {
        if (IsServer)
        {
            Debug.LogError("Server Transform");
            UpdateTransformClientRpc(isTrue);
        }
    }

    [ClientRpc]
    private void UpdateTransformClientRpc(bool isTrue)
    {
        cl.enabled = isTrue;
        Debug.LogError("Client Transform");
        objectRigidbody.isKinematic = true;
 
    }


    [ServerRpc(RequireOwnership = false)]
    public void SetVelocityAndPositionServerRpc(Vector3 linearVelocity, Vector3 position, Vector3 angularVelocity, Quaternion rotation)
    {
        if (IsServer)
        {
            Debug.LogError("Server");
            UpdateVelocityAndPositionOnClientsClientRpc(linearVelocity, position, angularVelocity, rotation);
        }
    }

    [ClientRpc]
    private void UpdateVelocityAndPositionOnClientsClientRpc(Vector3 linearVelocity, Vector3 position, Vector3 angularVelocity, Quaternion rotation)
    {
        objectRigidbody.isKinematic = false;
        objectRigidbody.velocity = linearVelocity;
        transform.position = position;
        transform.rotation = rotation;
        objectRigidbody.angularVelocity = angularVelocity;

        Debug.LogError("Client");
        Debug.Log(linearVelocity);
        Debug.Log(objectRigidbody.velocity);
        Debug.Log("----");
    }
}