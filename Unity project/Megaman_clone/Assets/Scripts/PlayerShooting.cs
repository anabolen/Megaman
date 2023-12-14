using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShooting : MonoBehaviour
{
    public List<GameObject> projectiles;
    //public List<GameObject> normalProjectiles;
    public float playerOrientation;
    public Vector3 defaultProjectileOffset;
    public Vector3 projectileOffset;
    PlayerInventory invScript;
    PlayerClimbing playerClimbing;
    PlayerManager playerManager;

    void Awake() 
    {
        invScript = GetComponent<PlayerInventory>();
        playerClimbing = GetComponent<PlayerClimbing>();
        playerManager = FindObjectOfType<PlayerManager>();
        projectileOffset = defaultProjectileOffset;
    }

    void Update() {

        if (Time.timeScale == 0)
            return;
        
        playerOrientation = Input.GetAxisRaw("Horizontal");
        
        if (playerOrientation != 0) {
            projectileOffset = new Vector3(defaultProjectileOffset.x * playerOrientation
                                            , defaultProjectileOffset.y, defaultProjectileOffset.z);
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            var projectileClass = invScript.specialAbilities[invScript.currentAbilityID];
            var projectile = projectileClass.AbilityProjectile();
            if (projectile != null) {
                projectileClass.AbilityAmmoIncrement(projectileClass.AmmoReductionPerShot());
                Physics2D.IgnoreLayerCollision(7, 14, FoxJumpAbility.ignorePlayerCollisions);
                Instantiate(projectile, transform.position + projectileOffset, transform.rotation);
                var ammo = projectileClass.AbilityAmmoIncrement(0).ammoReturn;
                playerManager.playerAmmo = ammo;
                playerManager.UpdatePlayerAmmo(ammo);
            }
        }
        
        //if (projectiles.Count < maxprojectiles && invScript.paused == false) {
        //    if (Input.GetKeyDown(KeyCode.F)) {
        //        var playerController = gameObject.GetComponent<PlayerController>();
        //        StartCoroutine(playerController.ShootingAnimations());
        //        Instantiate(projectile, transform.position + projectileOffset, transform.rotation);
        //    }
        //}
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + projectileOffset);
    }
}
