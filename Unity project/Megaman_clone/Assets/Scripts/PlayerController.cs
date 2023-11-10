using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float verticalSpeed;
    public Vector2 gcheckDimensions;
    public float gcheckDistance;
    public LayerMask solids;
    public float sJumpUpTime = 1f;
    public float lJumpTimeAddition = 1f;
    public bool hasDoubleJumped = false;
    public float jPressTime;
    public float keyPressTime = 0;
    float moveDirection;
    //float jumpTime;
    int verticalDirection = -1;
    public float jumpTimer;
    float jPressTimer;
    public bool jumpTriggerIsKeptDown;
    public bool jumpingUp;
    public bool fallingDown;
    public bool grounded;
    bool hasReleasedJumpDuringJump = true;
    public float longJumpTriggerTime = 0.1f;
    //Rigidbody2D rb;

    void Awake() {
        //rb = GetComponent<Rigidbody2D>();
        jumpTimer = 0;
        //jPressTimer = 0;
        jumpTriggerIsKeptDown = false;
    }

    private float GetNewHorizontalPosition(Vector3 previousPosition)
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
        var horzSpeed = moveDirection * moveSpeed * Time.deltaTime;
        return horzSpeed + previousPosition.x;
    }

    IEnumerator Jump()
    {
        hasReleasedJumpDuringJump = false;
        hasDoubleJumped = false;
        grounded = false;
        jumpingUp = true;
        verticalDirection = 1;
        jPressTimer += sJumpUpTime;
        while (jPressTimer > 0)
        {
            //Debug.Log(jPressTimer);
            jPressTimer -= Time.deltaTime;
            yield return null;
        }
        verticalDirection = -1;
        fallingDown = true;
        jumpingUp = false;
    }

    void Update()
    {
        float playerVerticalInput = Input.GetAxisRaw("Jump");

        if (playerVerticalInput != 0 && !jumpingUp && grounded && hasReleasedJumpDuringJump)
        {
            StartCoroutine(Jump());
        }

        if (!grounded)
        {
            float yOffset = verticalSpeed * verticalDirection;
            transform.position = new Vector2(transform.position.x, transform.position.y + yOffset);
        }

        if (playerVerticalInput != 0 && !hasDoubleJumped)
        {
            keyPressTime += Time.deltaTime;
        }

        if (playerVerticalInput == 0)
        {
            keyPressTime = 0;            
        }

        if (playerVerticalInput == 0 && grounded)
        {
            hasReleasedJumpDuringJump = true;
        }

        if (keyPressTime > longJumpTriggerTime)
        {
            jPressTimer += lJumpTimeAddition;
            hasDoubleJumped = true;
            keyPressTime = 0;
        }

        //GetNewHorizontalPosition();


        //if (jumpTimer > 0 && jumpingUp)
        //    jumpTimer -= Time.deltaTime;

        ////if (Input.GetAxisRaw("Jump") != 0) //-1 , 0 , 1
        ////    jumpTriggerIsKeptDown = true;

        //if (grounded && jumpTriggerIsKeptDown && !jumpingUp)
        //{
        //    if (Input.GetAxisRaw("Jump") != 0)
        //    {
        //        jPressTimer += Time.deltaTime;
        //    }
        //    if (jPressTimer > jPressTime)
        //    {
        //        jumpTimer = lJumpLength;
        //        jumpingUp = true;
        //        jumpTriggerIsKeptDown = false;
        //    }
        //    else if (Input.GetAxisRaw("Jump") == 0)
        //    {
        //        jumpTimer = sJumpUpTime;
        //        jumpingUp = true;
        //        jumpTriggerIsKeptDown = false;
        //    }
        //}
        ////else if (jumpTimer <= 0)
        ////    jumping = false;        

        //if (jumpingUp || !grounded)
        //{
        //    if (jumpingUp && jumpTimer > 0)
        //    {
        //        jPressTimer = 0;
        //        verticalDirection = 1;
        //    }
        //    else if (!grounded)
        //    {
        //        jumpingUp = false;
        //        verticalDirection = -1;
        //    }
        //    if (jumpingUp && Input.GetAxisRaw("Jump") != 0)
        //        return;
        //    else if (jumpingUp && jumpTimer < 0)
        //    {
        //        jumpingUp = false;
        //    }
        //}
        //else if (grounded)
        //    verticalDirection = 0;


        //var vertSpeed = verticalDirection * maxVertSpeed * Time.deltaTime;

        ////Move player
        //MovePlayer();

        ////transform.position = new Vector2(currentPosition.x + horzSpeed, currentPosition.y+vertSpeed);
    }

    private void MovePlayer()
    {
        var currentPosition = transform.position;
        float xPos = GetNewHorizontalPosition(currentPosition);
        float yPos = 0;
        transform.position = new Vector2(xPos, yPos);
    }

    private void FixedUpdate()
    { 

        if(jumpingUp) return;
        grounded = null != Physics2D.OverlapBox(transform.position, gcheckDimensions,
                            0, solids);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, gcheckDimensions);
    }
}
