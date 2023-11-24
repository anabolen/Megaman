using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EenmyProjectileScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damageAmount;
    [SerializeField] float direction;

    void Awake() {
        Destroy(gameObject, 6f);
    }

    void Update() {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.layer == 7) {
            coll.gameObject.GetComponent<PlayerManager>().UpdatePlayerHp(-damageAmount);
        }
        Destroy(gameObject);
    }
}
