using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform playerT;
    public bool followingP;
    void Start()
    {
        followingP = true;
        playerT = GameObject.Find("PlayerCharacter ").GetComponent<Transform>();
    }

    void Update()
    {

        if (followingP == true) {
            var cameraPos = transform.position;
            cameraPos.x = playerT.position.x;
            transform.position = cameraPos;
        }

    }

   


}
