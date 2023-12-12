using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HomingProjectile : MonoBehaviour {

    public static Transform playerTransform;
    public static float previousTimeOfCollision;
    public static List<GameObject> projectiles = new();

    public void FindPlayerTransform() {
        playerTransform = GameObject.Find("PlayerCharacter ").GetComponent<Transform>();
    }

    public void HomingThrottle(Rigidbody2D rb, float throttleForce, Transform projectileTf, Transform spriteTf) {
        rb.AddForce(-(rb.position - (Vector2)playerTransform.position).normalized * throttleForce, ForceMode2D.Impulse);
        var direction = new Vector2(rb.velocity.x, 0).normalized.x;
        projectileTf.rotation
            = Quaternion.Euler(0, 180, -direction * rb.velocity.normalized.y * Vector2.Angle(Vector3.right * direction, rb.velocity.normalized));
        spriteTf.localRotation
            = Quaternion.Euler(0, 180 * Mathf.Clamp(-direction, 0, 1), -18.5f);
    }
}
