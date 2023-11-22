using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float projectileSpeed;
    PlayerShooting playerShootingScript;
    GameObject playerCharacter;
    float projectileDirection;
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

    void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }

}
