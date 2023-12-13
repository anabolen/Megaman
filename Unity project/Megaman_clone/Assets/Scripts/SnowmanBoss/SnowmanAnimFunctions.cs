using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SnowmanAnimFunctions : MonoBehaviour {
    CarrotRocketAbility carrotRocketAbility;
    SnowmanBossAI bossAI;
    Collider2D splashDamageCollider;
    Collider2D whirlDamageCollider;
    Collider2D defaultCollider;
    Rigidbody2D rb;
    Animator animator;

    [Header("Shooting settings")]
    public bool shooting;
    public Vector2 projectileOffset;

    [Header("Butt slam settings")]
    [SerializeField] float buttSlamJumpForce, buttSlamJumpGravityScale;
    [SerializeField] float buttSlamLandingForce;

    [Header("Splash settings")]
    [SerializeField] float splashDuration;
    [SerializeField] float splashShockWaveSpeed;
    float timeOfSplash;
    public bool splashing = false;
    public bool whirling = false;
    float splashDamageArea_x;

    [Header("Whirl settings")]
    [SerializeField] float whirlAccelerationForce;
    [SerializeField] float whirlDeaccelerationForce;
    [SerializeField] float whirlAccelerationTime;
    [SerializeField] float whirlDeaccelerationTime;
    bool stopWhirling;
    float whirlStartTime;
    float whirlStopTime;


    void Awake() {
        rb = GetComponentInParent<Rigidbody2D>();
        whirlStartTime = -10;
        whirlStopTime = -10;
        animator = GetComponent<Animator>();
        bossAI = GetComponentInParent<SnowmanBossAI>();
        splashDamageCollider = GameObject.Find("SplashDamageCollider").GetComponent<Collider2D>();
        whirlDamageCollider = GameObject.Find("WhirlDamageCollider").GetComponent<Collider2D>();
        defaultCollider = GameObject.Find("BossHitboxes").GetComponent<Collider2D>();
        whirlDamageCollider.enabled = false;
        splashDamageCollider.enabled = false;

    }

    private void FixedUpdate() {
        SplashUpdate();
        if (whirling)
            WhirlUpdate();
    }

    void WhirlUpdate() {

        if (whirlStopTime + whirlDeaccelerationTime > Time.time && stopWhirling) { 
            rb.AddRelativeForce(new Vector2(bossAI.bossDirection, 0) * whirlDeaccelerationForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        else if (whirlStopTime + whirlDeaccelerationTime < Time.time && stopWhirling) { 
            rb.velocity = Vector2.zero;
            whirling = false;
            whirlDamageCollider.enabled = false;
            defaultCollider.enabled = true;
            ReturnToIdleAnimation();
        }

        if (stopWhirling)
            return;

        rb.AddRelativeForce(new Vector2(-bossAI.bossDirection, 0) * whirlAccelerationForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
        if (whirlStartTime + whirlAccelerationTime < Time.time) {
            stopWhirling = true;
            whirlDeaccelerationTime = Time.time;
        }
    }

    void SplashUpdate() {

        if (!splashing) {
            splashDamageArea_x = 0;
            splashDamageCollider.transform.localScale = Vector2.one;
            splashDamageCollider.enabled = false;
            return;
        }

        splashDamageArea_x += Time.fixedDeltaTime * splashShockWaveSpeed;
        splashDamageCollider.transform.localScale = new Vector2(splashDamageArea_x, 1);

        if (timeOfSplash + splashDuration < Time.time)
            splashing = false;
    }

    public void Whirl() {
        defaultCollider.enabled = false;
        whirlDamageCollider.enabled = true;
        whirling = true;
        stopWhirling = false;
        whirlStartTime = Time.time;
    }

    public void Shoot() {
        carrotRocketAbility = bossAI.bossAbilities[0] as CarrotRocketAbility;
        shooting = true;
        carrotRocketAbility.AbilityBehaviour();
    }

    public void Splash() {
        splashing = true;
        splashDamageCollider.enabled = true;
        timeOfSplash = Time.time;
    }

    public void ReturnToIdleAnimation() {
        animator.Play("BossIdle");
    }

    public void ButtSlamJump() {
        rb.gravityScale = buttSlamJumpGravityScale;
        rb.AddForce(buttSlamJumpForce * Vector2.up, ForceMode2D.Impulse);
    }

    public void ButtSlamLanding() {
        rb.gravityScale = 1;
        rb.AddForce(buttSlamLandingForce * Vector2.down, ForceMode2D.Impulse);
    }

    public void TurnToBossDirection() { //not necessary
        transform.position = new Vector3(transform.position.x * bossAI.bossDirection, transform.position.y, transform.position.z);;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)projectileOffset);
        
    }

}
