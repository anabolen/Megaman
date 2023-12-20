using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WolfDeath : MonoBehaviour {
    EnemyManager enemyManager;
    [SerializeField] GameObject wall;

    private void Awake() {
        enemyManager = GetComponentInChildren<EnemyManager>();
    }

    private void Update() {
        if (enemyManager.enemyHp == 0){
            wall.SetActive(false);
            Destroy(gameObject);
        }
    }
}
