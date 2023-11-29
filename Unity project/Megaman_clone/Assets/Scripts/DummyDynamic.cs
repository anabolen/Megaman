using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDynamic : MonoBehaviour
{
    bool started;
    Vector2Int startRoomCoords;
    Vector2 startPos;
    CameraMovement cam;
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

    void Start()
    {
        startPos = transform.position;
        var grid = FindObjectOfType<Grid>();
        cam = FindObjectOfType<CameraMovement>();
        cam.roomStart.AddListener(RoomStart);
        cam.roomReset.AddListener(RoomReset);
        startRoomCoords = (Vector2Int)grid.WorldToCell(transform.position);

    }


    void Update()
    {
        if (started) {
            transform.position += Vector3.up * Time.deltaTime;
        }
    }
}
