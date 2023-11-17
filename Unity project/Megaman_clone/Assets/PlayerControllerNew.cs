using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControllerNew : MonoBehaviour
{
    Rigidbody2D rb;

    public float verticalVelocity;
    public Vector2 longJumpForce;
    public Vector2 shortJumpForce;
    Vector2 jumpForce;
    float moveDirection;

    public Vector2 gcheckDimensions;

    float jumpKeyPressTimer;
    public float longJumpKeyPressTime;

    bool jumpingUp = false;
    bool jump = false;
    bool groundCheckAllowed = true;
    bool grounded;
    public LayerMask solids;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        groundCheckAllowed = true;
    }

    void Update() {
        moveDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetAxisRaw("Jump") != 0 && !jumpingUp) {
            StartCoroutine(Jump());
            print("jumpingUp");
            jumpingUp = true;
        }
        if (grounded)
            jumpingUp = false;
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(verticalVelocity * moveDirection, rb.velocity.y);
        if (jump) {
            rb.AddForce(jumpForce, ForceMode2D.Impulse);
            jump = false;
        }
        if (!groundCheckAllowed) {
            grounded = false;
            return;
        }
        grounded = null != Physics2D.OverlapBox(transform.position, gcheckDimensions,
                                                    0, solids);
    }

    IEnumerator Jump() {
        StartCoroutine(JumpGroundCheck());
        while (Input.GetAxisRaw("Jump") != 0 && jumpKeyPressTimer < longJumpKeyPressTime) {
            jumpKeyPressTimer += Time.deltaTime;
            yield return null;
        }
        if (jumpKeyPressTimer > longJumpKeyPressTime){
            jumpKeyPressTimer = 0;
            jumpForce = longJumpForce;
            jump = true;
        } else {
            jumpKeyPressTimer = 0;
            jumpForce = shortJumpForce;
            jump = true;
        }
    }

    IEnumerator JumpGroundCheck() {
        float jumpWindowTimer = 0;
        var jumpWindow = 0.5f;
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
