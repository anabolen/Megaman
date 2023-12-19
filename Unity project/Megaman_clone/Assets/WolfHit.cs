using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfHit : MonoBehaviour {
    [SerializeField] float wolfKnockback;
    [SerializeField] int wolfDamage;

    //void OnTriggerEnter2D(Collider2D collision) {
    //    if (collision.gameObject.layer != 7)
    //        return;
    //    var vectorToPlayer = collision.gameObject.GetComponent<Transform>().position - transform.position;
    //    float hitDirection = new Vector2(vectorToPlayer.x, 0).normalized.x;
    //    collision.gameObject.GetComponent<PlayerManager>().UpdatePlayerHp(-wolfDamage);
    //    collision.gameObject.GetComponent<PlayerController>().PlayerHitCheck(wolfKnockback, hitDirection);
    //}
}
