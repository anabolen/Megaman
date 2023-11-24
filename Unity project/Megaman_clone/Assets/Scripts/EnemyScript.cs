using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health;
    [SerializeField] int damageAmount;

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
        if (coll.gameObject.layer == 7) {
            coll.gameObject.GetComponent<PlayerManager>().UpdateHp(-damageAmount);
            print("damaged player");
        }
    }

}
