using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    public float maxHorizontalVelocity;
    public float minHorziontalVelocityMultiplier;
    public float jumpVelocity;
    public float moveDirection;
    public float defaultGravityScale;

    public float jumpWindow;
    public float horizontalAccelerationTime;
    public float initialHorizontalOffset;
    bool changingHorizontalDirection;

    public Vector2 gcheckDimensions;

    float jumpKeyPressTimer;
    public float maxJumpTime;
    public float minJumpTime;

    bool jumpingUp = false;
    bool groundCheckAllowed = true;
    bool grounded;
    public LayerMask solids;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        groundCheckAllowed = true;
    }

    void Update() {

        if (Input.GetAxisRaw("Horizontal") != 0 && !changingHorizontalDirection) {
            StartCoroutine(HorizontalOffsetChange());
            changingHorizontalDirection = true;
        }

        if (Input.GetAxisRaw("Jump") != 0 && !jumpingUp) {
            StartCoroutine(Jump());
            print("jumpingUp");
            jumpingUp = true;
        }

        if (grounded && Input.GetAxisRaw("Jump") == 0) 
            jumpingUp = false;
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(maxHorizontalVelocity * moveDirection, rb.velocity.y);
        if (!groundCheckAllowed) {
            grounded = false;
            return;
        }
        grounded = null != Physics2D.OverlapBox(transform.position, gcheckDimensions,
                                                    0, solids);
    }

    IEnumerator HorizontalOffsetChange() {
        float horizontalAccelerationTimer = 0;
        moveDirection = Input.GetAxisRaw("Horizontal") * minHorziontalVelocityMultiplier;
        rb.position = new Vector2(rb.position.x+initialHorizontalOffset*moveDirection/minHorziontalVelocityMultiplier
                                  , rb.position.y);
        while (horizontalAccelerationTimer < horizontalAccelerationTime && Input.GetAxisRaw("Horizontal")
               == moveDirection / minHorziontalVelocityMultiplier) {
            horizontalAccelerationTimer += Time.deltaTime;
            yield return null;
        }
        moveDirection /= minHorziontalVelocityMultiplier;
        while (Input.GetAxisRaw("Horizontal") == moveDirection) {
            yield return null;
        }
        moveDirection = 0;
        changingHorizontalDirection = false;
    }

    IEnumerator Jump() {
        StartCoroutine(JumpGroundCheck());
        rb.gravityScale = 0;
        while (Input.GetAxisRaw("Jump") != 0 && jumpKeyPressTimer < maxJumpTime) {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            jumpKeyPressTimer += Time.deltaTime;
            yield return null;
        }
        while (jumpKeyPressTimer < minJumpTime) {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            jumpKeyPressTimer += Time.deltaTime;
            yield return null;
        }
        rb.gravityScale = defaultGravityScale;
        jumpKeyPressTimer = 0;
    }

    IEnumerator JumpGroundCheck() {
        float jumpWindowTimer = 0;
        groundCheckAllowed = false;
        while (jumpWindowTimer < jumpWindow) {
            jumpWindowTimer += Time.deltaTime;
            yield return null;
        }
        groundCheckAllowed = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, gcheckDimensions);
    }
}
