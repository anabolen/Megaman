using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {
    Slider healthbar;
    PlayerManager playerManager;
    void Awake() {
        healthbar = GetComponentInChildren<Slider>();
        playerManager = FindObjectOfType<PlayerManager>();
        if (playerManager == null ) { Debug.LogError("..."); }
        healthbar.maxValue = playerManager.playerMaxHp;
        UpdateHealthBar();
    }

    public void UpdateHealthBar() {
        if (playerManager == null) { Debug.LogError("..."); }
        var hp = playerManager.playerHp;
        healthbar.value = hp;
    }
}
