using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
        AudioFW.Play("PerusaseAudio");
    }

    void Update() {
        transform.position += transform.right * projectileSpeed * Time.deltaTime * projectileDirection;
        if (awakeTime + projectileLifeTime < Time.time) {
            DestroyProjectile();
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        //could've an abstract class for enemy and boss health to inherit from
        if (coll.gameObject.layer == 8) {
            if (coll.gameObject.TryGetComponent(out EnemyManager manager))
                manager.UpdateEnemyHp(-damageAmount);
        }
        if (coll.gameObject.layer == 13) {
            if (coll.gameObject.TryGetComponent(out BossHealth bossHealth))
                bossHealth.UpdateBossHp(-damageAmount);
        }
        DestroyProjectile();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //could've an abstract class for enemy and boss health to inherit from
        if (coll.gameObject.layer == 8) {
            if (coll.gameObject.TryGetComponent(out EnemyManager manager))
                manager.UpdateEnemyHp(-damageAmount);
        }
        if (coll.gameObject.layer == 13) {
            if (coll.gameObject.TryGetComponent(out BossHealth bossHealth))
                bossHealth.UpdateBossHp(-damageAmount);
        }
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        if (NormalGunAbility.normalProjectiles.Any())
            NormalGunAbility.normalProjectiles.RemoveAt(0);
        Destroy(gameObject);
    }

}
