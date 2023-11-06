//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR;
//using Photon.Pun;

//public class NetworkPlayer : MonoBehaviour
//{
//    public Transform headtarget;
//    public Transform lefttarget;
//    public Transform righttarget;

//    public PhotonView photonView;

//    void Awake()
//    {
//        if (!photonView.IsMine)
//        {
//            enabled = false;
//        }
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        photonView = GetComponent<PhotonView>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if(photonView.IsMine)
//        {
//            headtarget.position = VRRigReference.Singleton.headtarget.position;
//            headtarget.rotation = VRRigReference.Singleton.headtarget.rotation;

//            righttarget.position = VRRigReference.Singleton.righttarget.position;
//            righttarget.rotation = VRRigReference.Singleton.righttarget.rotation;

//            lefttarget.position = VRRigReference.Singleton.lefttarget.position;
//            lefttarget.rotation = VRRigReference.Singleton.lefttarget.rotation;
//        }
//    }
//}
