using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossButtSlamAbility : SnowmanBossAbility {
    Rigidbody2D rb;
    float jumpForce = 5;
    public override void AbilityBehaviour() {
        rb = GameObject.Find("SnowmanBoss").GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    }

    public override Animation AbilityAnimation() {
        return null;
    }
}
