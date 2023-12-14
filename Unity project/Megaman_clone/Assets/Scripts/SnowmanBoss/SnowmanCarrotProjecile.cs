using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

public class SnowmanCarrotProjecile : HomingProjectile
{

    [Header("Carrot projectile settings")]
    [SerializeField] float initialLaunchForce;
    [SerializeField] float carrotProjectileThrottleForce;
    [SerializeField] float timeBeforeTrottle;
    public float timeBeforeSplitting;
    [SerializeField] int carrotDamage;
    [SerializeField] float carrotKnockback;
    [SerializeField] float destructionTime;

    float initializationTime;
    public float direction = 1;

    [Header("Small rocket settings")]
    [SerializeField] GameObject[] smallRockets;
    [SerializeField]
    float smallRocketLaunchForce = 5f,
                            smallRocketThrottleForce = 0.2f,
                            smallRocketTimeBeforeThrottle = 0.1f,
                            smallRockerVelocityMulitplier = 0.8f,
                            smallRocketDestructionTime;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    bool splitUpDisabled;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        var bossAI = GameObject.Find("SnowmanBoss").GetComponent<SnowmanBossAI>();
        direction = -bossAI.bossDirection;
        rb.AddForce(initialLaunchForce * direction * Vector2.right, ForceMode2D.Impulse);
        //FindPlayerTransform();
        initializationTime = Time.time;
        Destroy(gameObject, destructionTime);
        splitUpDisabled = CarrotRocketAbility.carrotSplitUpDisabled;
    }

    void FixedUpdate()
    {
        if (initializationTime + timeBeforeTrottle < Time.time)
        {
            HomingThrottle(rb, carrotProjectileThrottleForce, transform, spriteRenderer.transform);
        }
        if (initializationTime + timeBeforeSplitting < Time.time && !splitUpDisabled)
            SplitUp();
    }

    void SplitUp() {
        Vector2[] smallRocketLaunchDirections = {
            transform.TransformDirection(Vector2.up),
            transform.TransformDirection(-1 * new Vector2(rb.velocity.x, 0).normalized),
            transform.TransformDirection(Vector2.down),
        };
        var i = 0;
        foreach (var rocket in smallRockets)
        {
            var r = Instantiate(rocket);
            var script = r.GetComponent<HomingMissile>();
            script.LaunchProjectile(rb, smallRocketLaunchDirections[i], smallRocketLaunchForce, smallRocketTimeBeforeThrottle, smallRocketThrottleForce, smallRockerVelocityMulitplier, smallRocketDestructionTime);
            i++;
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.layer == 7) {
            coll.GetComponent<PlayerManager>().UpdatePlayerHp(-carrotDamage);
            float hitDirection = new Vector2(transform.TransformDirection(Vector2.right).normalized.x, 0).x;
            coll.gameObject.GetComponent<PlayerController>().PlayerHitCheck(carrotKnockback, -hitDirection);
        }
    }
}