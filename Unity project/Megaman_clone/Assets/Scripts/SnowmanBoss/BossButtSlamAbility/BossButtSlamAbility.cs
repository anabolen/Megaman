using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossButtSlamAbility : SnowmanBossAbility { //not used
    Rigidbody2D rb;
    [SerializeField] float jumpForce;
    public override void AbilityBehaviour() {
        rb = GameObject.Find("SnowmanBoss").GetComponent<Rigidbody2D>();
        //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
