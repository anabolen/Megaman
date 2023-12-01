using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

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
    Dictionary<Enum, Enum> correspondingShootingAnimations = new();
    //enum ShootingAnimationStates { StandingShooting, RunningShooting, AirborneShooting }
    public enum PlayerAnimatorStates { Left, Right, Airborne, Idle, Step, Running, Hit, 
                                       StandingShooting, RunningShooting, AirborneShooting }
    PlayerAnimatorStates playerSpriteDirection;
    public PlayerAnimatorStates playerAnimation;

    bool stepping;

    [SerializeField] float shootingAnimationTime;
    bool newShot;

    [SerializeField] float deathTime;
    [SerializeField] float spawnTime;
    float scriptPausedTime;
    public bool scriptPaused;

    bool takingDamage;
    [SerializeField] float hitTime;
    [SerializeField] float immunityTime;
    [SerializeField] float immunitySpriteToggleTime;
    [SerializeField] float hitForce;
    [SerializeField] float freezeBugTime;
    float freezeBugTimer;


    [SerializeField] Vector2 groundCheckDimensions;

    [SerializeField] Vector2 rightCheckDimensions;
    [SerializeField] Vector2 leftCheckDimensions;
    float sidecheckMidPosition;

    bool grounded;
    bool rightHit;
    bool leftHit;
    [SerializeField] Vector2 hitDirectionVector;
    float xhitDirection;

    float jumpKeyPressTimer;
    [SerializeField] float maxJumpTime;
    [SerializeField] float minJumpTime;

    bool jumpingUp = false;
    bool jumpAllowed = true;

    [SerializeField] LayerMask solids;
    [SerializeField] LayerMask enemies;
    [SerializeField] LayerMask enemyProjectiles;

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
        checkpoint = -1; //why?
        stepping = false;

        BoxCollider2D box = GetComponent<BoxCollider2D>();
        sidecheckMidPosition = box.size.y/2;

        playerHorizontalOrientation.Add(PlayerAnimatorStates.Left, -180);
        playerHorizontalOrientation.Add(PlayerAnimatorStates.Right, 0);
        playerSpriteDirection = PlayerAnimatorStates.Right;

        correspondingShootingAnimations.Add(PlayerAnimatorStates.Idle, PlayerAnimatorStates.StandingShooting);
        correspondingShootingAnimations.Add(PlayerAnimatorStates.Step, PlayerAnimatorStates.StandingShooting);
        correspondingShootingAnimations.Add(PlayerAnimatorStates.Running, PlayerAnimatorStates.RunningShooting);
        correspondingShootingAnimations.Add(PlayerAnimatorStates.Airborne, PlayerAnimatorStates.AirborneShooting);

        playerAnimation = PlayerAnimatorStates.Idle;
    }

    void Update() {

        if (scriptPaused || Time.timeScale == 0) {
            if(freezeBugTime < freezeBugTimer)
                scriptPaused = false;
            freezeBugTimer += Time.deltaTime;
            return;
        }
        freezeBugTimer = 0;

        if (Input.GetAxisRaw("Horizontal") != 0 && !changingHorizontalDirection) {
            StartCoroutine(HorizontalOffsetChange());
            changingHorizontalDirection = true;
        }

        if (Input.GetAxisRaw("Jump") != 0 && !jumpingUp) {
            StartCoroutine(Jump());
            jumpingUp = true;
        }

        CheckPlayerSpriteState(movementMultiplier);
        float rotation = playerHorizontalOrientation.GetValueOrDefault(playerSpriteDirection);
        spriteTransform.rotation = Quaternion.Euler(0, rotation, 0);

        if (grounded) {
            if (Input.GetAxisRaw("Jump") == 0 && jumpAllowed)
                jumpingUp = false;
            if (Input.GetAxisRaw("Horizontal") != 0 && !stepping) { 
                playerAnimation = PlayerAnimatorStates.Running;
                return;
            }
            if(!stepping)
                playerAnimation = PlayerAnimatorStates.Idle;
        } else {
            playerAnimation = PlayerAnimatorStates.Airborne;
        }
    }

    void FixedUpdate()
    {
        Physics2D.IgnoreLayerCollision(7, 8, takingDamage);

        rightHit = null != Physics2D.OverlapBox(new Vector2(transform.position.x + 0.2f, transform.position.y + sidecheckMidPosition),
                             groundCheckDimensions, 0, enemies);
        leftHit = null != Physics2D.OverlapBox(new Vector2(transform.position.x - 0.2f, transform.position.y + sidecheckMidPosition),
                             groundCheckDimensions, 0, enemies);

        if (scriptPaused) {
            return;
        }
        if (grounded) {
            verticalVelocityMultiplier = 0;
        } else
            verticalVelocityMultiplier = 1;

        rb.velocity = new Vector2(maxHorizontalVelocity * movementMultiplier, rb.velocity.y);

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
        if (rightHit)
            xhitDirection = -1;
        else
            xhitDirection = 1;

        if (playerManager.playerHp != 0 && takingDamage == false) { 
            scriptPaused = true;
            takingDamage = true;
            rb.AddForce(hitForce * new Vector2(hitDirectionVector.x * xhitDirection, hitDirectionVector.y).normalized, ForceMode2D.Impulse);
            //playerAnimation = PlayerAnimatorStates.Hit;
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
        if (checkpoints.Any())
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
            playerSpriteDirection = PlayerAnimatorStates.Left;
        else if (orientation > 0)
            playerSpriteDirection = PlayerAnimatorStates.Right;
    }

    IEnumerator HorizontalOffsetChange() {
        float horizontalAccelerationTimer = 0;
        movementMultiplier = Input.GetAxisRaw("Horizontal") * minHorziontalVelocityMultiplier;
        rb.MovePosition(new Vector2(rb.position.x + initialHorizontalOffset * movementMultiplier / minHorziontalVelocityMultiplier
                        , rb.position.y));
        stepping = true;
        playerAnimation = PlayerAnimatorStates.Step;
        while (horizontalAccelerationTimer < horizontalAccelerationTime && Input.GetAxisRaw("Horizontal")
               == movementMultiplier / minHorziontalVelocityMultiplier) {
            horizontalAccelerationTimer += Time.deltaTime;
            yield return null;
        }
        stepping = false;
        movementMultiplier /= minHorziontalVelocityMultiplier;
        playerAnimation = PlayerAnimatorStates.Running;
        while (Input.GetAxisRaw("Horizontal") == movementMultiplier) {
            yield return null;
        }
        movementMultiplier = 0;
        changingHorizontalDirection = false;
    }

    IEnumerator Jump() {
        StartCoroutine(JumpGroundCheck());
        rb.gravityScale = 0;
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
            playerAnimation = (PlayerAnimatorStates)correspondingShootingAnimations.GetValueOrDefault(playerAnimation);
            yield return null;
        }
    }

    void OnDrawGizmos() {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + 0.2f, transform.position.y + box.size.y/2), groundCheckDimensions);
        Gizmos.DrawWireCube(new Vector2(transform.position.x - 0.2f, transform.position.y + box.size.y / 2), rightCheckDimensions);
        Gizmos.DrawWireCube(transform.position, leftCheckDimensions);
    }
}
