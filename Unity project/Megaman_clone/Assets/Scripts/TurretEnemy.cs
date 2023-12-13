using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    bool started;
    Vector2Int startRoomCoords;
    Vector2 startPos;
    CameraMovement cam;
    [SerializeField] GameObject projectile;
    bool facingRight;
    bool notFired;
    Transform playerTrans;
    [SerializeField] float firerate;
    void RoomReset(Vector2Int roomCoords) {
        if (roomCoords != startRoomCoords) return;
        print(name + " resetting");
        transform.position = startPos;
        started = false;
    }

    void RoomStart(Vector2Int roomCoords) {
        if (roomCoords != startRoomCoords) return;
        print(name + " starting");
        started = true;
    }

    void Start() {
        startPos = transform.position;
        var grid = FindObjectOfType<Grid>();
        cam = FindObjectOfType<CameraMovement>();
        cam.roomStart.AddListener(RoomStart);
        cam.roomReset.AddListener(RoomReset);
        startRoomCoords = (Vector2Int)grid.WorldToCell(transform.position);
        playerTrans = GameObject.Find("PlayerCharacter ").transform;
        notFired = true;

    }


    void Update() {
        if (started) {
           if (transform.position.x > playerTrans.position.x) {
                facingRight = false;
           } else {
                facingRight = true;
           }
           if (facingRight == true && notFired == true) {
                ShootRight();
           } else if (facingRight == false && notFired == true) {
                ShootLeft();
           }
        }
    }

    void ShootRight() {
        Instantiate(projectile, transform.position, transform.rotation);
        notFired = false;
        Invoke("ResetFire", firerate);
    }

    void ShootLeft() {
        Instantiate(projectile, transform.position, Quaternion.Euler( 0, 0, 180));
        notFired = false;
        Invoke("ResetFire", firerate);
    }

    void ResetFire() {
        notFired = true;
    }

}
