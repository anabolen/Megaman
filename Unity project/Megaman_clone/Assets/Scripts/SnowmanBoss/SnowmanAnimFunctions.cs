using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SnowmanAnimFunctions : MonoBehaviour {
    CarrotRocketAbility carrotRocketAbility;
    SnowmanBossAI bossAI;
    Collider2D slamDamageCollider;
    Collider2D whirlDamageCollider;
    Collider2D defaultCollider;
    Rigidbody2D rb;
    Animator animator;

    [Header("Shooting settings")]
    public bool shooting;
    public Vector2 projectileOffset = new Vector2(0.98f, 0.22f);

    [Header("Butt slam settings")]
    [SerializeField] float buttSlamJumpForce = 10, buttSlamJumpGravityScale = 5;
    [SerializeField] float buttSlamLandingForce = 20;
    [SerializeField] float sidewaysButtSlamJumpForce = 10;
    [SerializeField] float sidewaysButtSlamLandingForce = 30;

    [Header("Slam settings")]
    [SerializeField] float normalSlamDuration = 0.2f;
    [SerializeField] float normalSlamShockWaveSpeed = 50;
    [SerializeField] float sidewaysSlamDuration = 0.1f;
    [SerializeField] float buttSlamJumpForce_xAxisComponent = 5;
    float currentSlamDuration;
    float timeOfSplash;
    public bool slamming = false;
    public bool whirling = false;
    float slamDamageArea_x;
    enum SlamType { Normal, Sideways }
    SlamType currentSlamType;
    enum WhirlType { Normal, Quick }
    WhirlType currentWhirlType;

    [Header("Whirl settings")]
    [SerializeField] float whirlAccelerationForce = 15;
    [SerializeField] float whirlDeaccelerationForce = 15;
    [SerializeField] float whirlAccelerationTime = 1;
    [SerializeField] float whirlDeaccelerationTime = 0.2f;
    [SerializeField] float quickWhirlAccelerationForce = 20;
    [SerializeField] float quickWhirlAccelerationTime = 1;


    bool stopWhirling;
    float whirlStartTime;
    float whirlStopTime;

    [Header("Second phase shooting additional settings")]
    [SerializeField] Vector2[] additionalLaunchDirections;
    int directionIndex;
    bool superShotStarted = false;

    void Awake() {
        rb = GetComponentInParent<Rigidbody2D>();
        whirlStartTime = -10;
        whirlStopTime = -10;
        animator = GetComponent<Animator>();
        bossAI = GetComponentInParent<SnowmanBossAI>();
        slamDamageCollider = GameObject.Find("SplashDamageCollider").GetComponent<Collider2D>();
        whirlDamageCollider = GameObject.Find("WhirlDamageCollider").GetComponent<Collider2D>();
        defaultCollider = GameObject.Find("BossHitboxes").GetComponent<Collider2D>();
        whirlDamageCollider.enabled = false;
        slamDamageCollider.enabled = false;
    }

    private void FixedUpdate() {
        SlamUpdate();
        if (whirling)
            WhirlUpdate();
    }

    void WhirlUpdate() {

        if (currentWhirlType == WhirlType.Quick) {
            QuickWhirlUpdate();
            return;
        }

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

    void QuickWhirlUpdate() {
        rb.AddRelativeForce(quickWhirlAccelerationForce * Time.fixedDeltaTime * new Vector2(-bossAI.bossDirection, 0), ForceMode2D.Impulse);
        if (whirlStartTime + quickWhirlAccelerationTime < Time.time) {
            rb.velocity = Vector2.zero;
            whirling = false;
            ReturnToIdleAnimation();
        }
    }

    void SlamUpdate() {

        if (!slamming) {
            slamDamageArea_x = 0;
            slamDamageCollider.transform.localScale = Vector2.one;
            slamDamageCollider.enabled = false;
            return;
        }

        if (currentSlamType == SlamType.Normal) { 
            slamDamageArea_x += Time.fixedDeltaTime * normalSlamShockWaveSpeed;
            slamDamageCollider.transform.localScale = new Vector2(slamDamageArea_x, 1);
            currentSlamDuration = normalSlamDuration;
        } else {
            slamDamageArea_x += Time.fixedDeltaTime * normalSlamShockWaveSpeed;
            slamDamageCollider.transform.localScale = new Vector2(slamDamageArea_x, 1);
            currentSlamDuration = sidewaysSlamDuration;
        }
        print(currentSlamType);

        if (timeOfSplash + currentSlamDuration < Time.time)
            slamming = false;
    }

    public void Whirl() {
        defaultCollider.enabled = false;
        whirlDamageCollider.enabled = true;
        whirling = true;
        stopWhirling = false;
        currentWhirlType = WhirlType.Normal;
        whirlStartTime = Time.time;
    }

    public void QuickWhirl() {
        defaultCollider.enabled = false;
        whirlDamageCollider.enabled = true;
        whirling = true;
        stopWhirling = false;
        currentWhirlType = WhirlType.Quick;
        whirlStartTime = Time.time;
    }

    public void Shoot() {
        carrotRocketAbility = bossAI.bossAbilities[0] as CarrotRocketAbility;
        CarrotRocketAbility.carrotSplitUpDisabled = false;
        CarrotRocketAbility.airStrike = false;
        shooting = true;
        carrotRocketAbility.LaunchDirection(additionalLaunchDirections[1]);
        carrotRocketAbility.AbilityBehaviour();
    }

    public void SecondPhaseShoot() {
        if (!superShotStarted) {
            superShotStarted = true;
            directionIndex = 0;
        }
        carrotRocketAbility = bossAI.bossAbilities[0] as CarrotRocketAbility;
        CarrotRocketAbility.carrotSplitUpDisabled = true;
        CarrotRocketAbility.airStrike = false;
        shooting = true;
        carrotRocketAbility.LaunchDirection(additionalLaunchDirections[directionIndex]);
        carrotRocketAbility.AbilityBehaviour();
        directionIndex++;
        if (directionIndex > 2)
            superShotStarted = false;
    }

    public void AirStrike() {

        CarrotRocketAbility.airStrike = true;
        CarrotRocketAbility.carrotSplitUpDisabled = false;
        carrotRocketAbility.LaunchDirection(additionalLaunchDirections[3]);
        carrotRocketAbility.AbilityBehaviour();
    }

    public void Slam() {
        rb.velocity = Vector2.zero;
        slamming = true;
        slamDamageCollider.enabled = true;
        timeOfSplash = Time.time;
    }

    public void ReturnToIdleAnimation() {
        animator.Play("BossIdle");
    }

    public void ButtSlamJump() {
        rb.gravityScale = buttSlamJumpGravityScale;
        rb.AddForce(buttSlamJumpForce * Vector2.up, ForceMode2D.Impulse);
        currentSlamType = SlamType.Normal;
    }

    public void SidewaysButtSlamJump() {
        rb.gravityScale = buttSlamJumpGravityScale;
        rb.AddForce(new Vector2(buttSlamJumpForce_xAxisComponent * -bossAI.bossDirection, sidewaysButtSlamJumpForce), ForceMode2D.Impulse);
        currentSlamType = SlamType.Sideways;
    }

    public void ButtSlamLanding() {
        rb.gravityScale = 1;
        rb.AddForce(buttSlamLandingForce * Vector2.down, ForceMode2D.Impulse);
    }

    public void SidewaysButtSlamLanding() {
        rb.gravityScale = 1;
        rb.AddForce(sidewaysButtSlamLandingForce * Vector2.down, ForceMode2D.Impulse);
    }

    public void TurnToBossDirection() { //not necessary
        transform.position = new Vector3(transform.position.x * bossAI.bossDirection, transform.position.y, transform.position.z);;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)projectileOffset);
        
    }

}
