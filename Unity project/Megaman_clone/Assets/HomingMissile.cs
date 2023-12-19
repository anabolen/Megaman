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
    [SerializeField] int rocketDamage;
    [SerializeField] float knockback;

    void FixedUpdate() {
        if (initializationTime + timeBeforeThrottle < Time.time && launched) {
            rb.velocity = rb.velocity + -(rb.position - (Vector2)playerTransform.position).normalized * velocityMultiplier;
            HomingThrottle(rb, throttleForce, transform, spriteRenderer.transform);
        }
    }

    public void LaunchProjectile(Rigidbody2D sourceRb, Vector2 launchDirection, float launchForce, float timeBeforeTh, float thForce, float vMultiplier, float destructionTime) {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.position = sourceRb.position;
        //rb.MovePosition(sourceRb.position);
        rb.velocity = sourceRb.velocity;
        timeBeforeThrottle = timeBeforeTh;
        throttleForce = thForce;
        launched = true;
        velocityMultiplier = vMultiplier;
        initializationTime = Time.time;
        rb.AddForce(launchForce * launchDirection, ForceMode2D.Impulse);
        Destroy(gameObject, destructionTime);
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.layer == 7) {
            coll.gameObject.GetComponent<PlayerManager>().UpdatePlayerHp(-rocketDamage);
            float hitDirection = new Vector2(transform.TransformDirection(Vector2.right).normalized.x, 0).x;
            coll.gameObject.GetComponent<PlayerController>().PlayerHitCheck(knockback, hitDirection);
        }
    }
}

