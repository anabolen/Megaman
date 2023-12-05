using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    [SerializeField] float timer;
    float time;
    bool active;
    [SerializeField] bool activeStart;

    SpriteRenderer platformSprite;
    BoxCollider2D collider;
    bool started;
    Vector2Int startRoomCoords;
    CameraMovement cam;
    void Start()
    {
        time = 0;
        platformSprite = GetComponentInChildren<SpriteRenderer>();
        collider = GetComponentInChildren<BoxCollider2D>();
        var grid = FindObjectOfType<Grid>();
        cam = FindObjectOfType<CameraMovement>();
        cam.roomStart.AddListener(RoomStart);
        cam.roomReset.AddListener(RoomReset);
        startRoomCoords = (Vector2Int)grid.WorldToCell(transform.position);
        active = activeStart;
    }

    void Update()
    {
        if (active == true) {
            platformSprite.enabled = true;
            collider.enabled = true;
        } else {
            platformSprite.enabled = false;
            collider.enabled = false;
        }
        if (time >= timer) {
            if (active == true) {
                active = false;
            } else {
                active = true;
            }
            time = 0;
        }
        if (started == true) {
            time += Time.deltaTime;
        }
        
    }
    void RoomReset(Vector2Int roomCoords) {
        if (roomCoords != startRoomCoords) return;
        print(name + " resetting");
        started = false;
        active = activeStart;
        time = 0;
    }

    void RoomStart(Vector2Int roomCoords) {
        if (roomCoords != startRoomCoords) return;
        print(name + " starting");
        started = true;
    }
}
