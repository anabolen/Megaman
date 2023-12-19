using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damageAmount;
    [SerializeField] float direction;
    [SerializeField] float knockbackForce;

    void Awake() {
        AudioFW.Play("TurretShootAudio");
        Destroy(gameObject, 4f);
    }

    void Update() {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.layer == 7) {
            coll.gameObject.GetComponent<PlayerManager>().UpdatePlayerHp(-damageAmount);
            coll.gameObject.GetComponent<PlayerController>().PlayerHitCheck(knockbackForce, direction);
        }
        Destroy(gameObject);
    }
}
