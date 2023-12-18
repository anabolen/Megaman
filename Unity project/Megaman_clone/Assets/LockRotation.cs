using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LockRotation : MonoBehaviour {
    [Header("Locked axes")]
    //public bool xAxis;
    //public bool yAxis; 
    [SerializeField] bool zAxis = true;


    void Update() {
        if (zAxis) { 
        transform.rotation 
            = Quaternion.Euler(Vector3.zero);
        }
    }

    //float RotationStatus(bool axisStatus, float currentRotation) {
    //    if (axisStatus) {
    //        return 0;
    //    } else
    //        return currentRotation;
    //}

}
