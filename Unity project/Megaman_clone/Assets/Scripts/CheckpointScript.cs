using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    BoxCollider2D boxCollider;
    bool used;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        used = false;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        boxCollider.enabled = false;
        if (used == false) {
            collision.GetComponent<PlayerController>().checkpoint += 1;
            used = true;
        }
    }
}
