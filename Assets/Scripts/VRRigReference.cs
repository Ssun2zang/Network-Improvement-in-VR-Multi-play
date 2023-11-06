using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRigReference : MonoBehaviour
{
    public static VRRigReference Singleton;

    public Transform headtarget;
    public Transform lefttarget;
    public Transform righttarget;
    private void Awake()
    {
        Singleton = this;
    }

}
