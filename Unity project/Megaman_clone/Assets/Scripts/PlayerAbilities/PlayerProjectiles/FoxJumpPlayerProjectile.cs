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
    float projectileDirection;
    //damage type enum?

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerShootingScript = FindObjectOfType<PlayerShooting>();
        var offsetVector = new Vector2(playerShootingScript.projectileOffset.x, 0).normalized;
        projectileDirection = offsetVector.x;

        rb.AddForce(Vector2.right * projectileDirection);
        awakeTime = Time.time;
    }

    void Update()
    {
        if (awakeTime + projectileLifeTime < Time.time)
        {
            FoxJumpAbility.foxJumpProjectileExists = false;
            Destroy(gameObject);
        }
    }

}
