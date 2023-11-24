using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerProjectileScript : MonoBehaviour
{
    public float projectileSpeed;
    PlayerShooting playerShootingScript;
    float projectileDirection;
    [SerializeField] int damageAmount;
    //damage type enum?

    void Awake()
    {
        playerShootingScript = FindObjectOfType<PlayerShooting>();
        playerShootingScript.projectiles.Add(gameObject);
        var offsetVector = new Vector2(playerShootingScript.projectileOffset.x, 0).normalized;
        projectileDirection = offsetVector.x;
        Destroy(gameObject, 3f);
    }

    void Update() {
        transform.position += transform.right * projectileSpeed * Time.deltaTime * projectileDirection;
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.layer == 8) {
            coll.gameObject.GetComponent<EnemyManager>().UpdateEnemyHp(-damageAmount);
        }
        Destroy(gameObject);
    }
}
