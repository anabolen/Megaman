using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxJumpPlayerProjectile : MonoBehaviour
{
    [SerializeField] float projectileForce;
    [SerializeField] float projectileLifeTime;
    float awakeTime;
    Rigidbody2D rb;
    PlayerShooting playerShootingScript;
    GameObject player;
    public float launchForce;
    float projectileDirection;
    [SerializeField] float ignoreCollisionsTime;
    //damage type enum?

    void Awake()
    {
        player = GameObject.Find("PlayerCharacter ");
        rb = GetComponent<Rigidbody2D>();
        playerShootingScript = FindObjectOfType<PlayerShooting>();
        var offsetVector = new Vector2(playerShootingScript.projectileOffset.x, 0).normalized;
        projectileDirection = offsetVector.x;

        rb.AddForce(projectileForce * new Vector2(projectileDirection, 1).normalized, ForceMode2D.Impulse);
        awakeTime = Time.time;
    }

    void LaunchPlayer() {
        var pRb = player.GetComponent<Rigidbody2D>();
        pRb.AddForce(Vector2.up * launchForce, ForceMode2D.Impulse);
    }

    void Update()
    {
        if (awakeTime + projectileLifeTime < Time.time)
        {
            FoxJumpAbility.foxJumpProjectileExists = false;
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        FoxJumpAbility.ignorePlayerCollisions = false;
        Physics2D.IgnoreLayerCollision(7, 9, FoxJumpAbility.ignorePlayerCollisions);
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        if(collision.gameObject.layer == 7) { 
            LaunchPlayer();
        }
    }

}
