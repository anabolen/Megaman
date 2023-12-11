using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BossAnimations : MonoBehaviour
{
    CarrotRocketAbility RocketScript;
    SnowmanBossAI BossAI;
    Collider2D splashDamageCollider;
    Rigidbody2D rb;
    Animator animator;

    [Header("Shooting settings")]
    public bool shooting;

    [Header("Butt slam settings")]
    [SerializeField] float buttSlamJumpForce, buttSlamJumpGravityScale;
    [SerializeField] float buttSlamLandingForce;

    [Header("Splash settings")]
    [SerializeField] float splashDuration;
    [SerializeField] float splashShockWaveSpeed;
    float timeOfSplash;
    public bool splashing = false;
    float splashDamageArea_x;


    void Awake() {
        rb = GetComponentInParent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        BossAI = GetComponentInParent<SnowmanBossAI>();
        splashDamageCollider = GameObject.Find("SplashDamageCollider").GetComponent<Collider2D>();
    }

    private void FixedUpdate() {
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

    public void Shoot() {
        RocketScript = BossAI.bossAbilities[0] as CarrotRocketAbility;
        shooting = true;
        RocketScript.AbilityBehaviour();
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
        transform.position = new Vector3(transform.position.x * BossAI.bossDirection, transform.position.y, transform.position.z);;
    }

}
