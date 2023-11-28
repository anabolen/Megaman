using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperGunPlayerProjectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] int damageAmount;
    [SerializeField] float projectileLifeTime;
    PlayerShooting playerShootingScript;
    float projectileDirection;
    //damage type enum?

    void Awake()
    {
        playerShootingScript = FindObjectOfType<PlayerShooting>();
        var offsetVector = new Vector2(playerShootingScript.projectileOffset.x, 0).normalized;
        projectileDirection = offsetVector.x;
        Destroy(gameObject, projectileLifeTime);
    }

    void Update()
    {
        transform.position += transform.right * projectileSpeed * Time.deltaTime * projectileDirection;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == 8)
        {
            coll.gameObject.GetComponent<EnemyManager>().UpdateEnemyHp(-damageAmount);
        }
        Destroy(gameObject);
    }
}
