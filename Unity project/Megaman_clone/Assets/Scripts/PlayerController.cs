using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float maxVertSpeed;
    public Vector2 gcheckDimensions;
    public float gcheckDistance;
    public LayerMask solids;
    public float sJumpLength;
    public float lJumpLength;
    public float jPressTime;

    float moveDirection;
    public bool jumpTrigger;
    float jumpTime;
    int vertDirection;
    public float jumpTimer;
    public bool jumping;
    float jPressTimer;
    public bool grounded;
    Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        jumpTimer = 0;
        jPressTimer = 0;
        jumpTrigger = false;
    }

    void Update() {
        moveDirection = Input.GetAxisRaw("Horizontal");

        if (jumpTimer > 0 && jumping)
            jumpTimer -= Time.deltaTime;

        if (Input.GetAxisRaw("Jump") != 0)
            jumpTrigger = true;

        if (grounded && jumpTrigger && !jumping) {
            if (Input.GetAxisRaw("Jump") != 0) {
                jPressTimer += Time.deltaTime;
            }
            if (jPressTimer > jPressTime) {
                jumpTimer = lJumpLength;
                jumping = true;
                jumpTrigger = false;                     
            } else if (Input.GetAxisRaw("Jump") == 0) {
                jumpTimer = sJumpLength;
                jumping = true;
                jumpTrigger = false;               
            }
        } 
        //else if (jumpTimer <= 0)
        //    jumping = false;        

        if (jumping || !grounded) {
            if (jumping && jumpTimer > 0) {
                jPressTimer = 0;
                vertDirection = 1;
            } else if (!grounded) {
                jumping = false;
                vertDirection = -1;
            }
            if (jumping && Input.GetAxisRaw("Jump") != 0)
                return; else if (jumping && jumpTimer < 0) {
                jumping = false; 
            }       
        } else if (grounded)
            vertDirection = 0;

        var horzSpeed = moveDirection * moveSpeed * Time.deltaTime;
        var vertSpeed = vertDirection * maxVertSpeed * Time.deltaTime;

        var tf = transform.position;
        transform.position = new Vector2(tf.x + horzSpeed, tf.y+vertSpeed);
    }

    private void FixedUpdate() {
        grounded = null != Physics2D.OverlapBox(transform.position, gcheckDimensions,
                            0, solids);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, gcheckDimensions);
    }
}
