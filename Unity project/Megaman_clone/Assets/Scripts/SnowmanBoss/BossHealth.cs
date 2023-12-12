using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour {
    public int health;
    public int maxHealth;

    void Awake() {
        health = maxHealth;
    }

    public void UpdateBossHp(int hpChange) {
        health += hpChange;
    }

}
