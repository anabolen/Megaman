using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Elevatorscript : MonoBehaviour
{
    public float speed;
    public int startingPoint;
    public Transform[] points;
    private int i;
    bool started;
    Vector2Int startRoomCoords;
    Vector2 startPos;
    CameraMovement cam;
    void Start()
    {
        transform.position = points[startingPoint].position;
        startPos = transform.position;
        var grid = FindObjectOfType<Grid>();
        cam = FindObjectOfType<CameraMovement>();
        cam.roomStart.AddListener(RoomStart);
        cam.roomReset.AddListener(RoomReset);
        startRoomCoords = (Vector2Int)grid.WorldToCell(transform.position);
    }


    void Update() {
        // Checking the distance of the platform and the point
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }// moving the platform to the point position with the index
        if (started == true) {
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        collision.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision) {
        collision.transform.SetParent(null);
    }
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
}