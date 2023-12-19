using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSoundwave : MonoBehaviour {
    [SerializeField] float verticalDeacceleration;
    [SerializeField ]float smallVerticalSpeed;
    [SerializeField] float largeVerticalSpeed;
    float verticalSpeed;
    [SerializeField] float horizontalSpeed;
    [SerializeField] float projectileDestructTime;
    WolfAI wolfAI;
    float direction;
    bool horizontalShot;
    CapsuleCollider2D collider;

    void Awake() {
        wolfAI = FindObjectOfType<WolfAI>();
        collider = GetComponent<CapsuleCollider2D>();
        direction = wolfAI.wolfDirection;
        horizontalShot = wolfAI.linearHorizontalShot;
        Destroy(gameObject, projectileDestructTime);
        if (horizontalShot) {
            verticalSpeed = smallVerticalSpeed;
        } else {
            verticalSpeed = largeVerticalSpeed;
        }
    }

    void Update() {
        float y = transform.position.y;
        verticalSpeed = Mathf.Clamp(verticalSpeed - verticalDeacceleration * Time.deltaTime, 0, 50);
        y = transform.position.y - verticalSpeed * Time.deltaTime;
        float x = transform.position.x + direction * horizontalSpeed * Time.deltaTime;
        transform.position = new Vector3(x, y, 0);
    }

    [SerializeField] float projectileKnockback;
    [SerializeField] int projectileDamage;

    public void SwitchCollider(int side) {
        collider.offset = new Vector2(0.25f * side, 0);
        collider.enabled = true;
    }

    public void TurnOffCollider() {
        collider.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer != 7)
            return;
        var vectorToPlayer = collision.gameObject.GetComponent<Transform>().position - transform.position;
        float hitDirection = new Vector2(vectorToPlayer.x, 0).normalized.x;
        collision.gameObject.GetComponent<PlayerManager>().UpdatePlayerHp(-projectileDamage);
        collision.gameObject.GetComponent<PlayerController>().PlayerHitCheck(projectileKnockback, hitDirection);
    }
}
