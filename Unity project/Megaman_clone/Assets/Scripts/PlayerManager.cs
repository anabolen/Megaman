using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerManager : MonoBehaviour
{
    public int playerHp;
    public int playerMaxHp;
    [SerializeField] static int playerAmmo;
    public int playerMaxAmmo;
    public int lives;
    public bool playingDeathAnimation;
    PlayerController controller;
    HealthBarScript healthBarScript;
    PlayerClimbing climbingScript;
    public bool justClimbed;
    bool canStartClimbing = false;
    Transform ladderTransform;
    
    void Awake() {
        lives = 3;
        playerAmmo = 0;
        controller = GetComponent<PlayerController>();
        healthBarScript = FindObjectOfType<HealthBarScript>();
        climbingScript = GetComponent<PlayerClimbing>();

        HomingProjectile.playerTransform = transform;
    }

    private void Start() {
        UpdatePlayerHp(playerMaxHp);
        climbingScript = null;
    }

    void Update() {
        if (climbingScript == null) {
            justClimbed = false;
        }
        if (canStartClimbing && Input.GetAxisRaw("Vertical") != 0) {
            climbingScript.StartClimbing(ladderTransform);
            controller.enabled = false;
        }
    }

    void OnTriggerStay2D(Collider2D coll) {
        if (coll.gameObject.layer == 12 && !justClimbed) {
            climbingScript = GetComponent<PlayerClimbing>();
            ladderTransform = coll.transform;
            canStartClimbing = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D collision) {
        climbingScript = null;
        ladderTransform = null;
        canStartClimbing = false;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.layer == 8 || coll.gameObject.layer == 10) {
            //Enemy collision
        }
        if (coll.gameObject.layer == 9) {
            //PickUpCollision
            var pickupScript = coll.gameObject.GetComponent<PickUpScript>();
            print(pickupScript.pickupType);
            if (pickupScript.pickupType == PickUpScript.PickUpType.BigHp || 
                pickupScript.pickupType == PickUpScript.PickUpType.SmallHp) {
                UpdatePlayerHp(pickupScript.pickUpHpAmount);
            }
            else if (pickupScript.pickupType == PickUpScript.PickUpType.BigAmmo ||
                     pickupScript.pickupType == PickUpScript.PickUpType.SmallAmmo) {
                playerAmmo += pickupScript.pickUpAmmoAmount;
            } else if (pickupScript.pickupType == PickUpScript.PickUpType.ExtraLife) {
                lives++;
            }
        }
    }

    public void UpdatePlayerHp(int hpChange) {
        playerHp = Mathf.Clamp(playerHp += hpChange, 0, playerMaxHp);
        if (healthBarScript != null) { 
            healthBarScript.UpdateHealthBar();
        }
        if (playerHp == 0 && !playingDeathAnimation) {
            StartCoroutine(controller.PlayerDeath());
            playingDeathAnimation = true;
        }
    }

    void HealthAndAmmoClamp() {
        playerAmmo = Mathf.Clamp(playerAmmo, 0, playerMaxAmmo);
        //Clamps hp and ammo between 0 and max so they never go above max or below 0
    }
}
