using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {
    bool bossSpawned;
    [SerializeField] GameObject boss;
    [SerializeField] Vector2 spawnPosition;
    [SerializeField] GameObject door;

    void Awake() {
        spawnPosition = GetComponentInChildren<Transform>().position;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer != 7 || bossSpawned)
            return;
        bossSpawned = true;
        boss.SetActive(true);
        door.SetActive(true);
    }
}
