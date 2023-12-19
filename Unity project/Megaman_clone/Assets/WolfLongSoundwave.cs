using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class WolfLongSoundwave : MonoBehaviour {

    Rigidbody2D rb;
    [SerializeField] float velocity;
    [SerializeField] int soundwaveDamage;
    [SerializeField] float soundwaveKnockback;
    [SerializeField] float destructTime;
    [SerializeField] Transform playerTransform;
    Vector2 vectorToPlayer;
    [SerializeField] Transform spriteTransform;
    bool launched = false;
    bool vectorToPlayerSet = false;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        spriteTransform = GetComponentInChildren<Transform>();
        playerTransform = GameObject.Find("PlayerCharacter ").GetComponent<Transform>();
        Destroy(gameObject, destructTime);
    }

    private void FixedUpdate() {
        if (!vectorToPlayerSet) { 
            vectorToPlayer = -(transform.position - playerTransform.position).normalized;
            vectorToPlayerSet = true;
            return;
        }
        rb.velocity = vectorToPlayer * velocity;
        if (launched)
            return;
        var direction = new Vector2(rb.velocity.x, 0).normalized.x;      
        transform.rotation
            = Quaternion.Euler(0, 180 * Mathf.Clamp(-direction, 0, 1), new Vector2(0, rb.velocity.y).normalized.y * Vector2.Angle(Vector3.right * direction, rb.velocity));
        launched = true;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer != 7)
            return;
        var vectorToPlayer = collision.gameObject.GetComponent<Transform>().position - transform.position;
        float hitDirection = new Vector2(vectorToPlayer.x, 0).normalized.x;
        collision.gameObject.GetComponent<PlayerManager>().UpdatePlayerHp(-soundwaveDamage);
        collision.gameObject.GetComponent<PlayerController>().PlayerHitCheck(soundwaveKnockback, hitDirection);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, playerTransform.position);
    }
}
