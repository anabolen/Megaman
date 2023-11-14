using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float maxHorizontalOffset;
    public float maxVerticalOffset;
    public float verticalOffset;
    float currentVerticalOffset;
    public float fallAccelerationDuration;
    float verticalAccelerator = 0;

    //groundCheck
    public LayerMask solids;
    public Vector2 gcheckDimensions;
    public float gcheckDistance;

    bool hasLongJumped = false;
    bool hasReleasedJumpDuringJump = true;

    //jump times
    public float shortJumpTime;
    public float longJumpTimeAddition;
    float jumpTime;
    float currentJumpDuration;

    //jump key press times
    float jumpKeyPressTimer;
    public float longJumpTriggerTime;
   
    //movement directions/speeds
    float moveDirection;
    public int verticalDirection = -1;
    float xOffset;
    public float yOffset;

    bool jumpingUp;
    public bool grounded;
    public bool offSetting = false;

    Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    Vector3 GetNewPosition(Vector3 previousPosition)
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
        xOffset = moveDirection * maxHorizontalOffset * Time.deltaTime;
        if (!grounded)
            yOffset = verticalOffset * Time.deltaTime;
        else
            yOffset = 0;
        return new Vector3(xOffset + previousPosition.x, yOffset + previousPosition.y, previousPosition.z);
    }

    IEnumerator Jump()
    {
        hasReleasedJumpDuringJump = false;
        hasLongJumped = false;
        grounded = false;
        jumpingUp = true;
        verticalDirection = 1;
        jumpTime += shortJumpTime;
        while (jumpTime > 0)
        {
            //Debug.Log(jPressTimer);
            jumpTime -= Time.deltaTime;
            verticalAccelerator += Time.deltaTime;
            yield return null;
        }
        verticalDirection = -1;
        jumpingUp = false;
    }

    IEnumerator ChangeVerticalSpeed(float speed, int currentDirection) {
        verticalAccelerator = 0;
        offSetting = true;
        while (verticalDirection == currentDirection) {
            verticalOffset = speed;
        yield return null;
        }
        verticalAccelerator = 0;
        offSetting = false;
    }

    void Update()
    {
        float playerVerticalInput = Input.GetAxisRaw("Jump");

        if (playerVerticalInput != 0 && !jumpingUp && grounded && hasReleasedJumpDuringJump)
        {
            StartCoroutine(Jump());
        }
        //counts the time jump key is being pressed
        if (playerVerticalInput != 0 && !hasLongJumped)
        {
            jumpKeyPressTimer += Time.deltaTime;
        }

        if (playerVerticalInput == 0)
        {
            jumpKeyPressTimer = 0;            
        }

        if (playerVerticalInput == 0 && grounded)
        {
            hasReleasedJumpDuringJump = true;
        }

        if (jumpKeyPressTimer > longJumpTriggerTime)
        {
            jumpTime += longJumpTimeAddition;
            currentJumpDuration = jumpTime + longJumpTimeAddition;
            hasLongJumped = true;
            jumpKeyPressTimer = 0;
        } else
            currentJumpDuration = shortJumpTime;
        

        var fallOffset = Mathf.Sin((verticalAccelerator / Mathf.PI/2)
                            / fallAccelerationDuration+Mathf.PI) * maxVerticalOffset;

        var jumpOffset = -Mathf.Sin((verticalAccelerator / Mathf.PI / 2)
                            / currentJumpDuration+Mathf.PI) * maxVerticalOffset + 8;
        print(jumpOffset);

        if (verticalDirection == 1) { 
            rb.gravityScale = 0;
            verticalOffset = jumpOffset;
        }
        else if (verticalDirection == -1) { 
            rb.gravityScale = 1.5f;
            verticalOffset = 0;
        }

        if (verticalDirection != 0 && !offSetting)
        {
            StartCoroutine(ChangeVerticalSpeed(verticalOffset, verticalDirection));
        }

        transform.position = GetNewPosition(transform.position);

        //var x = 
        //if (true)
        //{
        //    8;
        //} else
        //{
        //    10;
        //}

        //x = true ? 8 : 10;

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
    }

    void FixedUpdate() { 
        if(jumpingUp) return;
        grounded = null != Physics2D.OverlapBox(transform.position, gcheckDimensions,
                            0, solids);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, gcheckDimensions);
    }
}
