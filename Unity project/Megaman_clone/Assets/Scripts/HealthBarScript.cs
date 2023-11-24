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
        healthbar.maxValue = playerManager.playerMaxHp;
        UpdateHealthBar();
    }

    public void UpdateHealthBar() {
        healthbar.value = playerManager.playerHp;
    }
}
