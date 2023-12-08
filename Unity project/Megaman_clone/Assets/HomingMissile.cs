using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : HomingProjectile
{
    float throttleForce;
    float timeBeforeThrottle;
    float initializationTime;
    bool launched = false;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    float velocityMultiplier;

    void FixedUpdate() {
        if (initializationTime + timeBeforeThrottle < Time.time && launched) {
            rb.velocity = rb.velocity + -(rb.position - (Vector2)playerTransform.position).normalized * velocityMultiplier;
            HomingThrottle(rb, throttleForce, transform, spriteRenderer.transform);
        }
    }

    public void LaunchProjectile(Rigidbody2D sourceRb, Vector2 launchDirection, float launchForce, float timeBeforeTh, float thForce, float vMultiplier, float destructionTime) {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.MovePosition(sourceRb.position);
        rb.velocity = sourceRb.velocity;
        timeBeforeThrottle = timeBeforeTh;
        throttleForce = thForce;
        launched = true;
        velocityMultiplier = vMultiplier;
        initializationTime = Time.time;
        rb.AddForce(launchForce * launchDirection, ForceMode2D.Impulse);
        Destroy(gameObject, destructionTime);
    }
}

