using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float projectileSpeed;
    GameObject playerCharacter;
    void Awake()
    {
        var playerShootingScript = FindObjectOfType<PlayerShooting>();
        playerShootingScript.projectiles.Add(gameObject);
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.position += transform.right * projectileSpeed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }

}
