using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    bool started;
    bool dead;
    Vector2Int startRoomCoords;
    CameraMovement cam;
    [SerializeField] GameObject projectile;
    bool facingRight;
    bool notFired;
    public bool turning;
    Transform playerTrans;
    [SerializeField] float firerate;
    EnemyManager enemyManager;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    Animator animator;
    void RoomReset(Vector2Int roomCoords) {
        if (roomCoords != startRoomCoords) return;
        print(name + " resetting");
        started = false;
        enemyManager.enemyHp = enemyManager.enemyMaxHp;
    }

    void RoomStart(Vector2Int roomCoords) {
        if (roomCoords != startRoomCoords) return;
        print(name + " starting");
        started = true;
    }

    void Start() {
        var grid = FindObjectOfType<Grid>();
        cam = FindObjectOfType<CameraMovement>();
        cam.roomStart.AddListener(RoomStart);
        cam.roomReset.AddListener(RoomReset);
        startRoomCoords = (Vector2Int)grid.WorldToCell(transform.position);
        playerTrans = GameObject.Find("PlayerCharacter ").transform;
        enemyManager = GetComponent<EnemyManager>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponentInChildren<Animator>();
        notFired = true;
        turning = false;

    }


    void Update() {
        if (started) {
           if (transform.position.x > playerTrans.position.x) {
                facingRight = false;
                animator.Play("turretRotateLeft");
           } else {
                facingRight = true;
                animator.Play("turretRotateRight");
            }
           if (facingRight == true && notFired == true && turning == false) {
                ShootRight();
           } else if (facingRight == false && notFired == true && turning == false) {
                ShootLeft();
           }
        }
        if (enemyManager.enemyHp == 0) {
            animator.enabled = false;
            boxCollider.enabled = false;
            spriteRenderer.enabled = false;
            dead = true;
        } else {
            animator.enabled = true;
            boxCollider.enabled = true;
            spriteRenderer.enabled = true;
            dead = false;
        }

    }

    void ShootRight() {
        if (!dead) {
            Instantiate(projectile, transform.position, transform.rotation);
            notFired = false;
            Invoke("ResetFire", firerate);
        }
    }

    void ShootLeft() {
        if (!dead) {
            Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, 180));
            notFired = false;
            Invoke("ResetFire", firerate);
        }
    }

    void ResetFire() {
        notFired = true;
    }

}
