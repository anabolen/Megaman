using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HomingProjectile : MonoBehaviour {

    public static Transform playerTransform;

    public void FindPlayerTransform() {
        playerTransform = GameObject.Find("PlayerCharacter ").GetComponent<Transform>();
    }

    public void HomingThrottle(Rigidbody2D rb, float throttleForce) {
        rb.AddForce(-(rb.position - (Vector2)playerTransform.position).normalized * throttleForce, ForceMode2D.Impulse);
    }

}
