using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerT;
    public bool followingP;
    void Start()
    {
        followingP = true;
        //playerT = GameObject.Find("PlayerCharacter").GetComponent<Transform>();
    }

    void Update()
    {
        //if (playerT.position.x > -3.6f) {
        //    followingP = false;
        //} else {
        //    followingP = true;
        //}

        if (followingP == true) {
            var cameraPos = transform.position;
            cameraPos.x = playerT.position.x;
            transform.position = cameraPos;
        }

    }

   


}
