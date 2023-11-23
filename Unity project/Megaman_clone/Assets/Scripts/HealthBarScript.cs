using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    Slider healthbar;
    PlayerManager playerManager;
    void Awake()
    {
        healthbar = GetComponentInChildren<Slider>();
        playerManager = FindObjectOfType<PlayerManager>();
        healthbar.maxValue = playerManager.maxHp;
        UpdateHealthBar();
    }

    public void UpdateHealthBar() {
        healthbar.value = playerManager.hp;
    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.B)) {
            playerManager.hp -= 5;
            UpdateHealthBar();
        }   
    }

}
