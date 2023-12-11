using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarScript : MonoBehaviour {
    Slider healthbar;
    Slider ammoBar;
    PlayerManager playerManager;
    void Awake() {
        healthbar = GameObject.Find("Slider").GetComponent<Slider>();
        ammoBar = GameObject.Find("AmmoSlider").GetComponent<Slider>();    
        playerManager = FindObjectOfType<PlayerManager>();
        //if (playerManager == null ) { Debug.LogError("..."); }
        healthbar.maxValue = playerManager.playerMaxHp;
        ammoBar.maxValue = playerManager.playerMaxAmmo;
        UpdateHealthBar();
    }

    public void UpdateHealthBar() {
        //if (playerManager == null) { Debug.LogError("..."); }
        var ammo = playerManager.playerAmmo;
        var hp = playerManager.playerHp;
        healthbar.value = hp;
        ammoBar.value = ammo;
    }
}
