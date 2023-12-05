using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class BossCarrotProjecile : HomingProjectile {

    [SerializeField] float initialLaunchForce;
    [SerializeField] float carrotProjectileThrottleForce;
    float initializationTime;
    float bossDirection = 1;
    [SerializeField] float timeBeforeTrottle;
    Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(initialLaunchForce * bossDirection * Vector2.right, ForceMode2D.Impulse);
        //FindPlayerTransform();
        initializationTime = Time.time;
    }

    void FixedUpdate() {
        var direction = new Vector2(rb.velocity.x, 0).normalized.x;
        transform.rotation 
            = Quaternion.Euler(0, 180 * direction, -rb.velocity.normalized.y * Vector2.Angle(Vector3.right * direction, rb.velocity.normalized));
        if (initializationTime+timeBeforeTrottle < Time.time) {
            //HomingThrottle(rb, carrotProjectileThrottleForce);
        }
    }

}
