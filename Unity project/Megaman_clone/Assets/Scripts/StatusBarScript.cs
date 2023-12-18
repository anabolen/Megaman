using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarScript : MonoBehaviour {
    Slider healthbar;
    Slider ammoBar;
    PlayerManager playerManager;
    PlayerShooting playerShooting;
    void Awake() {
        healthbar = GameObject.Find("Slider").GetComponent<Slider>();
        ammoBar = GameObject.Find("AmmoSlider").GetComponent<Slider>();    
        playerManager = FindObjectOfType<PlayerManager>();
        playerShooting = playerManager.GetComponent<PlayerShooting>();
        //if (playerManager == null ) { Debug.LogError("..."); }
        healthbar.maxValue = playerManager.playerMaxHp;
        UpdateStatusBar();
    }

    public void UpdateStatusBar() {
        //if (playerManager == null) { Debug.LogError("..."); }

        //var ammo = playerManager.playerAmmo;
        var ammo = playerShooting.currentAbilityAmmo;
        print(ammo);
        var hp = playerManager.playerHp;
        healthbar.value = hp;
        ammoBar.maxValue = playerShooting.currentAbilityMaxAmmo;
        ammoBar.value = ammo;
    }
}
