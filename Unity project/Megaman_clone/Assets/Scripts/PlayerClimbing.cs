using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.UIElements;

public class PlayerClimbing : MonoBehaviour
{
    public bool climbing = false;
    Rigidbody2D rb;
    [SerializeField] float ladderClimbSpeed;
    Transform ladderTransform;
    PlayerController playerController;
    PlayerManager playerManager;
    PlayerShooting playerShooting;
    [SerializeField] float climbingSpriteSwitchTime;
    float climbingSpriteSwitchTimer;

    enum ClimbingDirection { ClimbingLeft, ClimbingRight }
    ClimbingDirection currentClimbingDirection;
    bool startedClimbing;

    Animator animator;
    

    void Awake() {
        //climbingRight = (Texture2D)Resources.Load("PlayerAnimations/ClimbingSpriteStates/ClimbingRight");
        //climbingLeft = (Texture2D)Resources.Load("PlayerAnimations/ClimbingSpriteStates/ClimbingLeft");
    }

    void Start() {
    }


    void Update() {
        if (!climbing) {
            startedClimbing = false;
            return;
        }
        if (!startedClimbing) {
            if (playerShooting.playerOrientation == -1)
                currentClimbingDirection = ClimbingDirection.ClimbingLeft;
            else
                currentClimbingDirection = ClimbingDirection.ClimbingRight;
            startedClimbing = true;
        }

        rb.gravityScale = 0;

        if (climbingSpriteSwitchTime < climbingSpriteSwitchTimer) {
            climbingSpriteSwitchTimer = 0;
            SpriteStateSwitch();
        }
        if (Input.GetAxisRaw("Vertical") != 0)
            climbingSpriteSwitchTimer += Time.deltaTime;
        else
            climbingSpriteSwitchTimer = 0;

        animator.Play(currentClimbingDirection.ToString());
        
        if (Input.GetAxisRaw("Jump") != 0) {
            rb.gravityScale = playerController.defaultGravityScale;
            climbing = false;
            playerController.enabled = false;
            playerManager.justClimbed = true;
            playerController.enabled = true;
        }
    }

    void FixedUpdate() {
        if (!climbing) {
            startedClimbing = false;
            return;
        }
        rb.MovePosition(new Vector2(ladderTransform.position.x, rb.position.y + Input.GetAxisRaw("Vertical") * ladderClimbSpeed * Time.fixedDeltaTime));
    }

    void SpriteStateSwitch() {
        if (currentClimbingDirection == ClimbingDirection.ClimbingRight) {
            currentClimbingDirection = ClimbingDirection.ClimbingLeft;
        } else if (currentClimbingDirection == ClimbingDirection.ClimbingLeft) {
            currentClimbingDirection = ClimbingDirection.ClimbingRight;
        }
    }

    public void StartClimbing(Transform tf) {
        climbing = true;
        rb = GetComponent<Rigidbody2D>();
        playerManager = GetComponent<PlayerManager>();
        playerController = GetComponent<PlayerController>();
        playerShooting = GetComponent<PlayerShooting>();
        animator = GetComponentInChildren<Animator>();
        ladderTransform = tf;
    }
}
