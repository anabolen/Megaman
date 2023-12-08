using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimations : MonoBehaviour
{
    CarrotRocketAbility RocketScript;
    SnowmanBossAI BossAI;
    Rigidbody2D rb;
    [SerializeField] float buttSlamJumpForce, buttSlamJumpGravityScale;
    [SerializeField] float buttSlamLandingForce;


    void Awake() {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    public void LaunchProjectile() {
        BossAI = transform.parent.GetComponent<SnowmanBossAI>();
        RocketScript = BossAI.bossAbilities[0] as CarrotRocketAbility;
        RocketScript.LaunchCarrot();
    }

    public void ButtSlamJump() {
        rb.gravityScale = buttSlamJumpGravityScale;
        rb.AddForce(buttSlamJumpForce * Vector2.up, ForceMode2D.Impulse);
    }

    public void ButtSlamLanding() {
        rb.gravityScale = 1;
        rb.AddForce(buttSlamLandingForce * Vector2.down, ForceMode2D.Impulse);
    }
}
