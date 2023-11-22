using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;


    public float defaultGravityScale;
    public float jumpVelocity;
    public float jumpWindow;

    public float maxHorizontalVelocity;
    public float minHorziontalVelocityMultiplier;
    public float moveDirection;
    public float horizontalAccelerationTime;
    public float initialHorizontalOffset;
    bool changingHorizontalDirection;

    Transform spriteTransform;
    public GameObject sprite;

    Dictionary<Enum, float> playerHorizontalOrientation = new();

    enum PlayerSpriteStates { Left, Right, Airborne, Idle, Step, Running }
    PlayerSpriteStates playerSpriteDirection;
    PlayerSpriteStates playerAnimation;

    Dictionary<Enum, Enum> correspondingShootingAnimations = new();
    enum ShootingAnimationStates { StandingShooting, RunningShooting, AirborneShooting }
    public float shootingAnimationTime;
    public bool newShot;


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
        spriteTransform = sprite.transform;
        groundCheckAllowed = true;

        playerHorizontalOrientation.Add(PlayerSpriteStates.Left, -180);
        playerHorizontalOrientation.Add(PlayerSpriteStates.Right, 0);
        playerSpriteDirection = PlayerSpriteStates.Right;

        correspondingShootingAnimations.Add(PlayerSpriteStates.Idle, ShootingAnimationStates.StandingShooting);
        correspondingShootingAnimations.Add(PlayerSpriteStates.Step, ShootingAnimationStates.StandingShooting);
        correspondingShootingAnimations.Add(PlayerSpriteStates.Running, ShootingAnimationStates.RunningShooting);
        correspondingShootingAnimations.Add(PlayerSpriteStates.Airborne, ShootingAnimationStates.AirborneShooting);

    }

    void Update() {

        if (Input.GetAxisRaw("Horizontal") != 0 && !changingHorizontalDirection) {
            StartCoroutine(HorizontalOffsetChange());
            changingHorizontalDirection = true;
        }

        if (Input.GetAxisRaw("Jump") != 0 && !jumpingUp) {
            StartCoroutine(Jump());
            jumpingUp = true;
        }

        if (grounded) {
            if (Input.GetAxisRaw("Jump") == 0)
                jumpingUp = false;
            if (Input.GetAxisRaw("Horizontal") == 0)
                playerAnimation = PlayerSpriteStates.Idle;
        }

        CheckPlayerSpriteState(moveDirection);
        float rotation = playerHorizontalOrientation.GetValueOrDefault(playerSpriteDirection);
        spriteTransform.rotation = Quaternion.Euler(0, rotation, 0); 
    }

    void CheckPlayerSpriteState(float orientation) { 
        if (orientation < 0)
            playerSpriteDirection = PlayerSpriteStates.Left;
        else if (orientation > 0)
            playerSpriteDirection = PlayerSpriteStates.Right;
    }

    IEnumerator HorizontalOffsetChange() {
        float horizontalAccelerationTimer = 0;
        moveDirection = Input.GetAxisRaw("Horizontal") * minHorziontalVelocityMultiplier;
        rb.position = new Vector2(rb.position.x+initialHorizontalOffset*moveDirection/minHorziontalVelocityMultiplier
                                  , rb.position.y);
        playerAnimation = PlayerSpriteStates.Step;
        while (horizontalAccelerationTimer < horizontalAccelerationTime && Input.GetAxisRaw("Horizontal")
               == moveDirection / minHorziontalVelocityMultiplier) {
            horizontalAccelerationTimer += Time.deltaTime;
            yield return null;
        }
        moveDirection /= minHorziontalVelocityMultiplier;
        playerAnimation = PlayerSpriteStates.Running;
        while (Input.GetAxisRaw("Horizontal") == moveDirection) {
            yield return null;
        }
        moveDirection = 0;
        changingHorizontalDirection = false;
    }

    IEnumerator Jump() {
        StartCoroutine(JumpGroundCheck());
        rb.gravityScale = 0;
        playerAnimation = PlayerSpriteStates.Airborne;
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

    public IEnumerator ShootingAnimations() {
        float shootingAnimationTimer = 0;
        newShot = false;
        while (shootingAnimationTime > shootingAnimationTimer && !newShot) { 
            shootingAnimationTimer += Time.deltaTime;
            correspondingShootingAnimations.GetValueOrDefault(playerAnimation);
            yield return null;
        }
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, gcheckDimensions);
    }
}
