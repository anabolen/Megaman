using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BearEnemy : MonoBehaviour
{
    Transform playerTrans;
    [SerializeField] float speed;
    [SerializeField] float maxRange;
    [SerializeField] float speedModifier;
    [SerializeField] float runTime;
    [SerializeField] float time;
    [SerializeField] bool facingRight;
    float startSpeed;
    bool started;
    public bool dying;
    [SerializeField] bool running;
    Vector2Int startRoomCoords;
    Vector2 startPos;
    CameraMovement cam;
    EnemyManager enemyManager;
    SpriteRenderer spriteRend;
    BoxCollider2D boxCollider;
    Animator animator;
    void Start()
    {
        playerTrans = GameObject.Find("PlayerCharacter ").GetComponent<Transform>();
        enemyManager = GetComponent<EnemyManager>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        startPos = transform.position;
        var grid = FindObjectOfType<Grid>();
        cam = FindObjectOfType<CameraMovement>();
        cam.roomStart.AddListener(RoomStart);
        cam.roomReset.AddListener(RoomReset);
        startRoomCoords = (Vector2Int)grid.WorldToCell(transform.position);
        startSpeed = speed;
        running = false;
    }

    void Update()
    {
        if (started && running == false) {
            if (transform.position.x < playerTrans.position.x) {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            } else if (transform.position.x > playerTrans.position.x) {
                transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            }
        }
        if (started && running == false && dying == false) {
            if (transform.position.x < playerTrans.position.x) {
                spriteRend.flipX = true;
                facingRight = true;
            } else if (transform.position.x > playerTrans.position.x) {
                spriteRend.flipX = false;
                facingRight = false;
            }
        }
        if (enemyManager.enemyHp == 0) {
            boxCollider.enabled = false;
            dying = true;
            animator.Play("PolarBearDeathAnim");
        } else if (dying == false){
            boxCollider.enabled = true;
            spriteRend.enabled = true;
        }
        if (Vector3.Distance(transform.position, playerTrans.position) < maxRange && running == false) {
            BearRun();
        }
        if (running == true && dying == false) {
            time += Time.deltaTime;
            animator.Play("PolarBearRunning");
            if (facingRight == true) {
                transform.position += Vector3.right * speed * speedModifier * Time.deltaTime;
            } else if (facingRight == false) {
                transform.position += Vector3.left * speed * speedModifier * Time.deltaTime;
            }
        } else if (dying == false) {
            animator.Play("PolarBearWalking");
        }
        if (time >= runTime) {
            running = false;
            time = 0;
        }
    }

    void RoomReset(Vector2Int roomCoords) {
        if (roomCoords != startRoomCoords) return;
        print(name + " resetting");
        transform.position = startPos;
        enemyManager.enemyHp = enemyManager.enemyMaxHp;
        started = false;
        speed = startSpeed;
        running = false;
        dying = false;
    }

    void RoomStart(Vector2Int roomCoords) {
        if (roomCoords != startRoomCoords) return;
        print(name + " starting");
        started = true;
    }

    void BearRun() {
        running = true;
        print("Running");
    }
    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }
}
