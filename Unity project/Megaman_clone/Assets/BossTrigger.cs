using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {
    bool bossSpawned;
    [SerializeField] GameObject boss;
    [SerializeField] Vector2 spawnPosition;
    [SerializeField] GameObject door;
    [SerializeField] PlayerManager playerManager;

    void Awake() {
        spawnPosition = GetComponentInChildren<Transform>().position;
    }

    void Update() {
        if (!bossSpawned)
            return;
        if (playerManager.playerHp == 0) { 
            boss.GetComponent<SnowmanBossAI>().BossReset();
            door.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer != 7 || bossSpawned)
            return;
        playerManager = collision.gameObject.GetComponent<PlayerManager>();
        bossSpawned = true;
        AudioFW.StopLoop("LevelMusic");
        AudioFW.PlayLoop("BossMusic");
        boss.SetActive(true);
        door.SetActive(true);
    }
}
