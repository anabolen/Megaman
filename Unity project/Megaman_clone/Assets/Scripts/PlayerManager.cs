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
    public int playerAmmo;
    public int playerMaxAmmo;
    public int lives;
    public bool playingDeathAnimation;
    PlayerController controller;
    StatusBarScript healthBarScript;
    PlayerClimbing climbingScript;
    public bool justClimbed;
    bool canStartClimbing = false;
    Transform ladderTransform;

    [SerializeField] float initialImmunityDuration;
    public static float timeOfHit;
    
    void Awake() {
        lives = 3;
        playerAmmo = 0;
        controller = GetComponent<PlayerController>();
        healthBarScript = FindObjectOfType<StatusBarScript>();
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
        if (collision.gameObject.layer == 12) { 
            climbingScript = null;
            ladderTransform = null;
            canStartClimbing = false;
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.layer == 9) {
            //PickUpCollision
            var pickupScript = coll.gameObject.GetComponent<PickUpScript>();
            if (pickupScript.pickupType == PickUpScript.PickUpType.BigHp || 
                pickupScript.pickupType == PickUpScript.PickUpType.SmallHp) {
                UpdatePlayerHp(pickupScript.pickUpHpAmount);
            }
            else if (pickupScript.pickupType == PickUpScript.PickUpType.BigAmmo ||
                     pickupScript.pickupType == PickUpScript.PickUpType.SmallAmmo) {
                playerAmmo += pickupScript.pickUpAmmoAmount;
                PlayerInventory inventory = GetComponent<PlayerInventory>();
                inventory.specialAbilities
                    [inventory.currentAbilityID].AbilityAmmoIncrement(pickupScript.pickUpAmmoAmount);
                UpdatePlayerAmmo(inventory.specialAbilities
                    [inventory.currentAbilityID].AbilityAmmoIncrement(0).ammoReturn);
            } else if (pickupScript.pickupType == PickUpScript.PickUpType.ExtraLife) {
                lives++;
            }
        }
    }

    public void UpdatePlayerHp(int hpChange) {

        if (hpChange < 0 && controller.takingDamage || initialImmunityDuration + timeOfHit > Time.time) {
            return;
        } else if (hpChange < 0) {
            timeOfHit = Time.time;
        }

        playerHp = Mathf.Clamp(playerHp += hpChange, 0, playerMaxHp);
        if (healthBarScript != null) { 
            healthBarScript.UpdateHealthBar();
        }
        if (playerHp == 0 && !playingDeathAnimation) {
            StartCoroutine(controller.PlayerDeath());
            playingDeathAnimation = true;
        }
    }

    public void UpdatePlayerAmmo(int ammoChange) {
        playerAmmo = ammoChange;
        if (healthBarScript != null) {
            healthBarScript.UpdateHealthBar();
        }
    }

    void HealthAndAmmoClamp() {
        playerAmmo = Mathf.Clamp(playerAmmo, 0, playerMaxAmmo);
        //Clamps hp and ammo between 0 and max so they never go above max or below 0
    }
}
