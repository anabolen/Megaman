using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyDynamic : MonoBehaviour
{
    bool started;
    Vector2Int startRoomCoords;
    Vector2 startPos;
    CameraMovement cam;
    FlyingEnemyMovementScript movementScript;
    EnemyManager manager;
    CircleCollider2D collider;
    SpriteRenderer spriteRenderer;
    void RoomReset(Vector2Int roomCoords) {
        if (roomCoords != startRoomCoords) return;
        print(name + " resetting");
        transform.position = startPos;
        movementScript.gameObject.transform.position = startPos;
        manager.enemyHp = manager.enemyMaxHp;
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
        movementScript = GetComponentInChildren<FlyingEnemyMovementScript>();
        manager = GetComponentInChildren<EnemyManager>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        collider = GetComponentInChildren<CircleCollider2D>();

    }


    void Update() {
        if (started) {
            movementScript.enabled = true;
        } else {
            movementScript.enabled= false;
        }
        if (manager.enemyHp == 0) {
            spriteRenderer.enabled = false;
            collider.enabled = false;
        } else {
            spriteRenderer.enabled = true;
            collider.enabled = true;
        }
    }
}
