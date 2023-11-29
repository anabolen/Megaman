using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class RoomEvent : UnityEvent<Vector2Int> {
}

public class CameraMovement : MonoBehaviour
{
    public Vector2Int roomCoords;
    public RoomEvent roomStart;
    public RoomEvent roomReset;
    Vector2Int previousRoom;

    Grid grid;
    Transform playerT;
    public bool followingP;
    CameraRoomData roomData;
    void Start()
    {
        followingP = true;
        playerT = GameObject.Find("PlayerCharacter ").GetComponent<Transform>();
        grid = GameObject.Find("GridManager").GetComponent<Grid>();
        roomData = FindObjectOfType<CameraRoomData>();
        previousRoom = Vector2Int.one * 100;
    }

    void Update()
    {

        //if (followingP == true) {
        //    var cameraPos = transform.position;
        //    cameraPos.x = playerT.position.x;
        //    transform.position = cameraPos;
        //}
        
        var p = playerT.position;
        var gridCord = grid.WorldToCell(p);
        roomCoords = (Vector2Int)gridCord;
        if (roomCoords != previousRoom) {
            roomReset.Invoke(previousRoom);
            roomStart.Invoke(roomCoords);
        }
        previousRoom = roomCoords;
        bool inCameraRoom = false;
        int leftx = 0, rightx = 0, y = 0;
        foreach (var room in roomData.rooms) {
            if(roomCoords.y != room.y || roomCoords.x < room.leftx || roomCoords.x > room.rightx) {
                continue;
            }
            y = room.y;
            leftx = room.leftx;
            rightx = room.rightx;
            inCameraRoom = true;
            break;
        }
        if (inCameraRoom) {
            var leftRoomCenter = grid.GetCellCenterWorld(new Vector3Int(leftx, y, 0));
            var rightRoomCenter = grid.GetCellCenterWorld(new Vector3Int(rightx, y, 0));
            var newPos = new Vector3(Mathf.Clamp(p.x, leftRoomCenter.x, rightRoomCenter.x), rightRoomCenter.y, -10);
            transform.position = newPos;
        } else { 
            transform.position = grid.GetCellCenterWorld(gridCord) + Vector3.back * 10;
        }

    }




}
