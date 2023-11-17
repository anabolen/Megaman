using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform playerT;
    public bool followingP;
    public float cOffset;
    void Start()
    {
        followingP = true;
        playerT = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (playerT.position.x > -3.6f) {
            followingP = false;
        } else {
            followingP = true;
        }

        if (followingP == true) {
            var cameraPos = transform.position;
            cameraPos.x = playerT.position.x - cOffset;
            transform.position = cameraPos;
        }

    }

   


}
