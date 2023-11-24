using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EenmyProjectileScript : MonoBehaviour
{
    public float speed;
    public int damageAmount;

    void Awake()
    {
        Destroy(gameObject, 6f);
    }


    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.layer == 7) {
            coll.gameObject.GetComponent<PlayerManager>().UpdateHp(-damageAmount);
        }
        Destroy(gameObject);
    }
}
