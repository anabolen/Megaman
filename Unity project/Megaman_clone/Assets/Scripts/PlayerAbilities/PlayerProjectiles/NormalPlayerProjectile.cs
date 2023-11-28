using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class NormalPlayerProjectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] int damageAmount;
    PlayerShooting playerShootingScript;
    float projectileDirection;
    [SerializeField] float projectileLifeTime;
    float awakeTime;
    //damage type enum?

    void Awake()
    {
        playerShootingScript = FindObjectOfType<PlayerShooting>();
        var offsetVector = new Vector2(playerShootingScript.projectileOffset.x, 0).normalized;
        projectileDirection = offsetVector.x;
        awakeTime = Time.time;
    }

    void Update() {
        transform.position += transform.right * projectileSpeed * Time.deltaTime * projectileDirection;
        if (awakeTime + projectileLifeTime < Time.time) {
            DestroyProjectile();
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.layer == 8) {
            coll.gameObject.GetComponent<EnemyManager>().UpdateEnemyHp(-damageAmount);
        }
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        NormalGunAbility.normalProjectiles.RemoveAt(0);
        Destroy(gameObject);
    }

}
