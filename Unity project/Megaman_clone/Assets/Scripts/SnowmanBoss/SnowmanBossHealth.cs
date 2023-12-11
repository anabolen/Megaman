using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanBossHealth : MonoBehaviour {
    public float health;
    public float maxHealth;

    void Awake() {
        health = maxHealth;
    }

}
