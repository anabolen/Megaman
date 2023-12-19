using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int enemyHp;
    public int enemyMaxHp;
    [SerializeField] int damageAmount;
    [SerializeField] float knockbackForce;

    private void Awake() {
        UpdateEnemyHp(enemyMaxHp);
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.layer == 6) {
            UpdateEnemyHp(-1);
        }
        if (coll.gameObject.layer == 7) {
            var vectorToPlayer = coll.gameObject.GetComponent<Transform>().position - transform.position;
            float hitDirection = new Vector2(vectorToPlayer.x, 0).normalized.x;
            coll.gameObject.GetComponent<PlayerManager>().UpdatePlayerHp(-damageAmount);
            coll.gameObject.GetComponent<PlayerController>().PlayerHitCheck(knockbackForce, hitDirection);
        }
    }

    public void UpdateEnemyHp(int hpChange) {
        enemyHp = Mathf.Clamp(enemyHp += hpChange, 0, enemyMaxHp);
    }
}
