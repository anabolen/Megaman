using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
     public float projectileSpeed;
    void Awake()
    {
        Destroy(gameObject,5f);
    }

    void Update()
    {
        transform.position += transform.right * projectileSpeed * Time.deltaTime;
    }
}
