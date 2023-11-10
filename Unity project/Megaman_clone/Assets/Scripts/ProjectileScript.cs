using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float projectileSpeed;
    GameObject playerobject;
    void Awake()
    {
        Destroy(gameObject,3f);
        playerobject = GameObject.Find("Player");
        PlayerShooting playerscript = playerobject.GetComponent<PlayerShooting>();
        playerscript.projectiles.Add(gameObject);
    }

    void Update()
    {
        transform.position += transform.right * projectileSpeed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }

}
