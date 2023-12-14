using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class TykkiScript : MonoBehaviour
{
    public GameObject tykkiprojectile;
    EnemyManager enemyManager;
    SpriteRenderer spriteRenderer;
    PolygonCollider2D polygonCollider;
    Animator animator;
    bool started;
    Vector2Int startRoomCoords;
    Vector2 startPos;
    CameraMovement cam;
    void Start() {
        enemyManager = GetComponent<EnemyManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();
        var grid = FindObjectOfType<Grid>();
        cam = FindObjectOfType<CameraMovement>();
        cam.roomStart.AddListener(RoomStart);
        cam.roomReset.AddListener(RoomReset);
        startRoomCoords = (Vector2Int)grid.WorldToCell(transform.position);
    }

    void Update() {
        if (!started) {
            animator.enabled = false;
            return;
        }
        if (enemyManager.enemyHp == 0) {
            spriteRenderer.enabled = false;
            polygonCollider.enabled = false;
            animator.enabled = false;

        } else {
            spriteRenderer.enabled = true;
            polygonCollider.enabled = true;
            animator.enabled = true;
        }
    }

    void RoomReset(Vector2Int roomCoords) {
        if (roomCoords != startRoomCoords) return;
        print(name + " resetting");
        enemyManager.enemyHp = enemyManager.enemyMaxHp;
        started = false;
    }

    void RoomStart(Vector2Int roomCoords) {
        if (roomCoords != startRoomCoords) return;
        print(name + " starting");
        started = true;
    }

    public void LaunchProjectile() {      
        Instantiate(tykkiprojectile, transform.position, transform.rotation);
    }
}
