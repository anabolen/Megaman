using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour {
    Collider2D triggerCollider;
    Collider2D collisionCollider;

    void Awake () {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders) {
            if (collider.isTrigger)
                triggerCollider = collider;
            else
                collisionCollider = collider;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer != 7)
            return;
        collisionCollider.enabled = false;
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer != 7)
            return;
        collisionCollider.enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer != 7 || Input.GetAxisRaw("Vertical") != -1)
            return;
        collisionCollider.enabled = false;
    }
}
