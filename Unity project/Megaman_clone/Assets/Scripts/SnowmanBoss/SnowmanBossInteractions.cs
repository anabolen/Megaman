using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnowmanBossInteractions : MonoBehaviour {

    SnowmanBossAI bossAI;
    float knockback;
    int damageAmount;
    SnowmanAnimFunctions animations;
    float hitTime;
    [SerializeField] float playerImmunityTime;

    void Start () {
        animations = GameObject.Find("BossSprite").GetComponent<SnowmanAnimFunctions>();
        bossAI = GetComponentInParent<SnowmanBossAI>();
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.layer == 7) {
            if (hitTime + playerImmunityTime > Time.time) {
                return;
            }
            hitTime = Time.time;
            if (animations.splashing) {
                knockback = bossAI.splashKnockback;
                damageAmount = bossAI.buttSlamDamage;
            }
            if (animations.whirling) {
                knockback = bossAI.whirlKnockback;
                damageAmount = bossAI.whirlDamage;
            }

            coll.gameObject.GetComponent<PlayerManager>().UpdatePlayerHp(-damageAmount);
            StartCoroutine(coll.gameObject.GetComponent<PlayerController>().PlayerHit(knockback, -bossAI.bossDirection));
        }
    }

}
