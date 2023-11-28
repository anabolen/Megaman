using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D col;

    [SerializeField] float defaultGravityScale;
    [SerializeField] float shortJumpGravityScale;
    [SerializeField] float jumpVelocity;
    [SerializeField] float jumpWindow;

    [SerializeField] float maxHorizontalVelocity;
    [SerializeField] float minHorziontalVelocityMultiplier;
    [SerializeField] float movementMultiplier;
    [SerializeField] float horizontalAccelerationTime;
    [SerializeField] float initialHorizontalOffset;
    bool changingHorizontalDirection;

    float verticalVelocityMultiplier;

    Transform spriteTransform;
    SpriteRenderer spriteRenderer;
    public GameObject sprite;
    PlayerManager playerManager;

    Dictionary<Enum, float> playerHorizontalOrientation = new();
    enum PlayerSpriteStates { Left, Right, Airborne, Idle, Step, Running, Hit }
    PlayerSpriteStates playerSpriteDirection;
    PlayerSpriteStates playerAnimation;

    [SerializeField] float deathTime;
    [SerializeField] float spawnTime;
    float scriptPausedTime;
    bool scriptPaused;

    bool takingDamage;
    [SerializeField] float hitTime;
    [SerializeField] float immunityTime;
    [SerializeField] float immunitySpriteToggleTime;
    [SerializeField] Vector2 hitDirection;
    [SerializeField] float hitForce;

    Dictionary<Enum, Enum> correspondingShootingAnimations = new();
    enum ShootingAnimationStates { StandingShooting, RunningShooting, AirborneShooting }
    [SerializeField] float shootingAnimationTime;
    bool newShot;

    [SerializeField] Vector2 groundCheckDimensions;

    float jumpKeyPressTimer;
    [SerializeField] float maxJumpTime;
    [SerializeField] float minJumpTime;

    bool jumpingUp = false;
    bool jumpAllowed = true;
    bool grounded;

    [SerializeField] LayerMask solids;

    public int checkpoint;
    public GameObject[] checkpoints;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        playerManager = GetComponent<PlayerManager>();
        spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        spriteTransform = sprite.transform;
        jumpAllowed = true;
        StartCoroutine(PlayerSpawn());
        checkpoint = -1;




        playerHorizontalOrientation.Add(PlayerSpriteStates.Left, -180);
        playerHorizontalOrientation.Add(PlayerSpriteStates.Right, 0);
        playerSpriteDirection = PlayerSpriteStates.Right;

        correspondingShootingAnimations.Add(PlayerSpriteStates.Idle, ShootingAnimationStates.StandingShooting);
        correspondingShootingAnimations.Add(PlayerSpriteStates.Step, ShootingAnimationStates.StandingShooting);
        correspondingShootingAnimations.Add(PlayerSpriteStates.Running, ShootingAnimationStates.RunningShooting);
        correspondingShootingAnimations.Add(PlayerSpriteStates.Airborne, ShootingAnimationStates.AirborneShooting);
    }

    void Update() {

        if (scriptPaused) {
            return;
        }

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

        CheckPlayerSpriteState(movementMultiplier);
        float rotation = playerHorizontalOrientation.GetValueOrDefault(playerSpriteDirection);
        spriteTransform.rotation = Quaternion.Euler(0, rotation, 0); 
    }

    void FixedUpdate()
    {
        Physics2D.IgnoreLayerCollision(7, 8, takingDamage);

        if (scriptPaused) {
            return;
        }
        if (grounded) {
            verticalVelocityMultiplier = 0;
        } else
            verticalVelocityMultiplier = 1;

        rb.velocity = new Vector2(maxHorizontalVelocity * movementMultiplier, rb.velocity.y * verticalVelocityMultiplier);

        if (!jumpAllowed)
        {
            grounded = false;
            return;
        }
        grounded = null != Physics2D.OverlapBox(transform.position, groundCheckDimensions,
                                                    0, solids);

    }

    public IEnumerator PlayerHit()
    {
        if (playerManager.playerHp != 0 && takingDamage == false) { 
            scriptPaused = true;
            takingDamage = true;
            rb.AddForce(hitDirection.normalized * hitForce, ForceMode2D.Impulse);
            playerAnimation = PlayerSpriteStates.Hit;
            yield return new WaitForSeconds(hitTime);
            scriptPaused = false;
            float immunityStartTime = Time.time;
            while (immunityStartTime + immunityTime > Time.time) {
                if (spriteRenderer.enabled) { 
                    spriteRenderer.enabled = false;
                } else
                    spriteRenderer.enabled = true;
                yield return new WaitForSeconds(immunitySpriteToggleTime);
            }
            spriteRenderer.enabled = true;
            takingDamage = false;
        }
    }

    public IEnumerator PlayerDeath() {
        //Play death animation
        playerManager.lives--;
        col.enabled = false;
        spriteRenderer.enabled = false;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        scriptPaused = true;
        scriptPausedTime = deathTime;
        yield return new WaitForSeconds(deathTime);
        yield return StartCoroutine(QuitTransitionAnimations());
        if (playerManager.lives > 0) {
            StartCoroutine(PlayerSpawn());
        }
    }

    public IEnumerator PlayerSpawn() {
        transform.position = checkpoints[checkpoint].transform.position;
        //Play spawn animation
        col.enabled = false;
        spriteRenderer.enabled = false;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        scriptPaused = true;
        scriptPausedTime = spawnTime;
        //health bar fill animation
        yield return new WaitForSeconds(spawnTime);
        playerManager.UpdatePlayerHp(playerManager.playerMaxHp);
        yield return StartCoroutine(QuitTransitionAnimations());
    }

    IEnumerator QuitTransitionAnimations() {
        col.enabled = true;
        spriteRenderer.enabled = true;
        scriptPaused = false;
        rb.gravityScale = defaultGravityScale;
        yield return null;
    }


    void CheckPlayerSpriteState(float orientation) { 
        if (orientation < 0)
            playerSpriteDirection = PlayerSpriteStates.Left;
        else if (orientation > 0)
            playerSpriteDirection = PlayerSpriteStates.Right;
    }

    IEnumerator HorizontalOffsetChange() {
        float horizontalAccelerationTimer = 0;
        movementMultiplier = Input.GetAxisRaw("Horizontal") * minHorziontalVelocityMultiplier;
        rb.MovePosition(new Vector2(rb.position.x + initialHorizontalOffset * movementMultiplier / minHorziontalVelocityMultiplier
                        , rb.position.y)); 
        playerAnimation = PlayerSpriteStates.Step;
        while (horizontalAccelerationTimer < horizontalAccelerationTime && Input.GetAxisRaw("Horizontal")
               == movementMultiplier / minHorziontalVelocityMultiplier) {
            horizontalAccelerationTimer += Time.deltaTime;
            yield return null;
        }
        movementMultiplier /= minHorziontalVelocityMultiplier;
        playerAnimation = PlayerSpriteStates.Running;
        while (Input.GetAxisRaw("Horizontal") == movementMultiplier) {
            yield return null;
        }
        movementMultiplier = 0;
        changingHorizontalDirection = false;
    }

    IEnumerator Jump() {
        StartCoroutine(JumpGroundCheck());
        rb.gravityScale = 0;
        playerAnimation = PlayerSpriteStates.Airborne;
        while (Input.GetAxisRaw("Jump") != 0 && jumpKeyPressTimer < maxJumpTime) {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            jumpKeyPressTimer += Time.deltaTime;
            if (jumpKeyPressTimer > minJumpTime)
                rb.gravityScale = defaultGravityScale;
            yield return null;
        }
        while (jumpKeyPressTimer < minJumpTime) {
            //rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            rb.gravityScale = shortJumpGravityScale;
            jumpKeyPressTimer += Time.deltaTime;
            yield return null;
        }
        rb.gravityScale = defaultGravityScale;
        jumpKeyPressTimer = 0;
    }

    IEnumerator JumpGroundCheck() {
        jumpAllowed = false;
        float jumpWindowTimer = 0;
        while (jumpWindowTimer < jumpWindow) {
            jumpWindowTimer += Time.deltaTime;
            yield return null;
        }
        jumpAllowed = true;
    }

    public IEnumerator ShootingAnimations() {
        newShot = true;
        yield return null;
        float shootingAnimationTimer = 0;
        newShot = false;
        while (shootingAnimationTime > shootingAnimationTimer && !newShot) { 
            shootingAnimationTimer += Time.deltaTime;
            correspondingShootingAnimations.GetValueOrDefault(playerAnimation);
            yield return null;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, groundCheckDimensions);
    }
}
