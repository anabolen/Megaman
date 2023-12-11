using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnowmanBossInteractions : MonoBehaviour {

    SnowmanBossAI BossAI;
    float knockback;
    int damageAmount;
    BossAnimations animations;
    float hitTime;
    [SerializeField] float playerImmunityTime;

    void Start () {
        animations = GameObject.Find("BossSprite").GetComponent<BossAnimations>();
        BossAI = GetComponentInParent<SnowmanBossAI>();
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.layer == 7) {
            if (hitTime + playerImmunityTime > Time.time) {
                return;
            }
            hitTime = Time.time;
            if (animations.splashing) {
                knockback = SnowmanBossAI.splashKnockback;
                damageAmount = SnowmanBossAI.buttSlamDamage;
            }

            coll.gameObject.GetComponent<PlayerManager>().UpdatePlayerHp(-damageAmount);
            StartCoroutine(coll.gameObject.GetComponent<PlayerController>().PlayerHit(knockback, -BossAI.bossDirection));
        }
    }

}
