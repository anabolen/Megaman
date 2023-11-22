using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health;

    void Update()
    {
        if (health <= 0) {
            Destroy(gameObject);
        }   
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.layer == 6) {
            health--;
        }
    }
}
