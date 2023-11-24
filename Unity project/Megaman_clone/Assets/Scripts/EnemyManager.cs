using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] int enemyHp;
    [SerializeField] int enemyMaxHp;
    [SerializeField] int damageAmount;

    private void Awake() {
        UpdateEnemyHp(enemyMaxHp);
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.layer == 6) {
            enemyHp--;
        }
        if (coll.gameObject.layer == 7) {
            coll.gameObject.GetComponent<PlayerManager>().UpdatePlayerHp(-damageAmount);
        }
    }

    public void UpdateEnemyHp(int hpChange) {
        enemyHp = Mathf.Clamp(enemyHp += hpChange, 0, enemyMaxHp);
        if (enemyHp == 0) {
            Destroy(gameObject);
        }
    }
}
