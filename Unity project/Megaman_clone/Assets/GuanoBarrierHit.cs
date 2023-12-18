using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuanoBarrierHit : MonoBehaviour {

    [SerializeField] int damage;
    [SerializeField] float timeBeforeResetAfterLaunch;
    bool justHit;
    public ISpecialAbilities guanoBarrierAbility;
    float timeOfLaunch;
    public Transform playerSpriteTransform;
    public PlayerShooting shootingScript;
    public bool launched;
    public bool reset = false;
    float timeOfReset;
    [SerializeField] float graceWindowAfterReset = 0.5f;
    Collider2D hitCollider;
    Collider2D projectileStopperCollider;

    private void Awake() {
        hitCollider = GetComponent<Collider2D>();
        projectileStopperCollider =GetComponentInChildren<Collider2D>();
    }

    void Update() {

        if (reset && Input.GetKey(KeyCode.F)) {
            return;
        } else if (reset) {
            reset = false;
            justHit = false;
        }

        if (!shootingScript.guanoBarrierLaunched) { 
            timeOfLaunch = Time.time;
            return;
        }

        if (timeOfLaunch + timeBeforeResetAfterLaunch < Time.time && hitCollider.enabled) { 
            ResetBarrier();
        }

    }
    void ResetBarrier() {
        transform.parent = playerSpriteTransform;
        transform.position = playerSpriteTransform.position;
        reset = true;
        shootingScript.guanoBarrierEnabled = false;
        timeOfReset = Time.time;
        shootingScript.guanoBarrierLaunched = false;
        guanoBarrierAbility.AbilityAmmoIncrement(guanoBarrierAbility.AmmoReductionPerShot());
        GetComponent<GuanoBarrierAnimation>().GuanoBarrierSpriteSwitch(false);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (justHit || !hitCollider.enabled)
            return;
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 13) {
            justHit = true;
            if (collision.TryGetComponent(out EnemyManager enemyHealth))
                enemyHealth.UpdateEnemyHp(-damage);
            if (collision.TryGetComponent(out BossHealth bossHealth))
                bossHealth.UpdateBossHp(-damage);
            ResetBarrier();
        }
        if (collision.gameObject.layer == 10) {
            justHit = true;
            Destroy(collision.gameObject);
            ResetBarrier();
        }
    }

}
