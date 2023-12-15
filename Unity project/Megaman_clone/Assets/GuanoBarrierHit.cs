using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuanoBarrierHit : MonoBehaviour {

    [SerializeField] int damage;
    [SerializeField] float timeBeforeResetAfterLaunch;
    float timeOfLaunch;
    public Transform playerSpriteTransform;
    public PlayerShooting shootingScript;
    public bool launched;
    bool reset = false;

    private void Update() {

        if (reset && Input.GetKey(KeyCode.F)) {
            return;
        } else if (reset) { 
            reset = false;
            shootingScript.guanoBarrierLaunched = false;
        }

        if (!shootingScript.guanoBarrierLaunched) { 
            timeOfLaunch = Time.time;
            return;
        }

        if (timeOfLaunch + timeBeforeResetAfterLaunch < Time.time) { 
            ResetBarrier();
        }

    }
    void ResetBarrier() {
        transform.parent = playerSpriteTransform;
        transform.position = playerSpriteTransform.position;
        reset = true;
        shootingScript.guanoBarrierEnabled = false;
        GetComponent<GuanoBarrierAnimation>().GuanoBarrierSpriteSwitch(false);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 13) {
            if (collision.TryGetComponent(out EnemyManager enemyHealth))
                enemyHealth.UpdateEnemyHp(-damage);
            if (collision.TryGetComponent(out BossHealth bossHealth))
                bossHealth.UpdateBossHp(-damage);
            ResetBarrier();
        }
        if (collision.gameObject.layer == 10) {
            Destroy(collision.gameObject);
            ResetBarrier();
        }
    }

}
