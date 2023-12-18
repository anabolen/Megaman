using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuanoBarrierMovement : MonoBehaviour {

    public float barrierSpeed;
    Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>(); 
    }

    public void LaunchBarrier(Vector2 direction) {
        rb.velocity = direction * barrierSpeed;
    }

}
