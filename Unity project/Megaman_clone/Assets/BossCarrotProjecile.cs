using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

public class BossCarrotProjecile : HomingProjectile {

    [Header("Carrot projectile settings")]
    [SerializeField] float initialLaunchForce;
    [SerializeField] float carrotProjectileThrottleForce; 
    [SerializeField] float timeBeforeTrottle;
    [SerializeField] float timeBeforeSplitting;
    float initializationTime;
    float bossDirection = 1;

    [Header("Small rocket settings")]
    [SerializeField] GameObject[] smallRockets;
    [SerializeField] float smallRocketLaunchForce;
    [SerializeField] float smallRocketThrottleForce;
    [SerializeField] float smallRocketTimeBeforeThrottle;
    [SerializeField] float smallRockerVelocityMulitplier;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb.AddForce(initialLaunchForce * bossDirection * Vector2.right, ForceMode2D.Impulse);
        //FindPlayerTransform();
        initializationTime = Time.time;
    }

    void FixedUpdate() {
        if (initializationTime + timeBeforeTrottle < Time.time) {
            HomingThrottle(rb, carrotProjectileThrottleForce, transform, spriteRenderer.transform);
        }
        if (initializationTime + timeBeforeSplitting < Time.time)
            SplitUp();

    }

    void SplitUp() {
        Vector2[] smallRocketLaunchDirections = {
            transform.TransformDirection(Vector2.up), 
            transform.TransformDirection(-1 * new Vector2(rb.velocity.x, 0).normalized), 
            transform.TransformDirection(Vector2.down),
        };
        var i = 0;
        foreach (var rocket in smallRockets) {
            var r = Instantiate(rocket);
            var script = r.GetComponent<HomingMissile>();
            script.LaunchProjectile(rb, smallRocketLaunchDirections[i], smallRocketLaunchForce, smallRocketTimeBeforeThrottle, smallRocketThrottleForce, smallRockerVelocityMulitplier);
            i++;
        }
        Destroy(gameObject);
    }

}
