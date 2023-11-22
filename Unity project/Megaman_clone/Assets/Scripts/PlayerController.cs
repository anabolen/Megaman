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
    float jumpOffset;

    bool goingRight = true;

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
        if (moveDirection == 1)
            goingRight = true;
        else if (moveDirection == -1)
            goingRight = false;
        xOffset = moveDirection * maxHorizontalOffset * Time.deltaTime;
        if (!grounded || jumpingUp) { 
            yOffset = verticalOffset * Time.deltaTime;
            print("falling");
        }
        else { 
            yOffset = 0;
            offSetting = false;
        }
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

    IEnumerator ChangeVerticalOffset(int currentDirection) {
        
        offSetting = true;
        verticalAccelerator = 0.01f;
        while (verticalDirection == currentDirection) {

        yield return null;
        }
        verticalAccelerator = 0.01f;
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
        

        if (verticalDirection != 0 && !offSetting) {
            offSetting = true;
            verticalAccelerator = 0.01f;
        }

        var fallOffset = Mathf.Sin(Mathf.PI / 2 / Mathf.Clamp(verticalAccelerator, 0.01f, fallAccelerationDuration) / fallAccelerationDuration 
                                   + Mathf.PI) * maxVerticalOffset;

        if (jumpingUp) { 
            jumpOffset = -Mathf.Sin(Mathf.PI / 2 / Mathf.Clamp(verticalAccelerator, 0.01f, currentJumpDuration) / currentJumpDuration 
                                        + Mathf.PI) * maxVerticalOffset + 8;
        }

        if (verticalDirection == 1) {       
            verticalOffset = jumpOffset;
        }
        else if (verticalDirection == -1) {
            verticalOffset = fallOffset;
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
