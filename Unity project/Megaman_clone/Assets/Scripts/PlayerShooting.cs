using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ISpecialAbilities;

public class PlayerShooting : MonoBehaviour
{

    public List<GameObject> projectiles;
    public GameObject guanoBarrier;
    GuanoBarrierHit guanoBarrierHit;
    public bool guanoBarrierEnabled;
    public bool guanoBarrierLaunched;
    [SerializeField] Vector2[] guanoBarrierLaunchDirections;
    //public List<GameObject> normalProjectiles;
    public float playerOrientation;
    public Vector3 defaultProjectileOffset;
    public Vector3 projectileOffset;
    PlayerInventory invScript;
    PlayerClimbing playerClimbing;
    PlayerManager playerManager;
    Transform spriteTransform;

    void Awake() 
    {
        invScript = GetComponent<PlayerInventory>();
        playerClimbing = GetComponent<PlayerClimbing>();
        playerManager = FindObjectOfType<PlayerManager>();
        spriteTransform = GetComponentInChildren<SpriteRenderer>().GetComponent<Transform>();   
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

        var projectileClass = invScript.specialAbilities[invScript.currentAbilityID];

        if (projectileClass.ShootingBehaviour() == AbilityShootingBehaviour.PressToShoot)
            PressToShootCheck(projectileClass);
        else if (projectileClass.ShootingBehaviour() == AbilityShootingBehaviour.HoldToShoot)
            HoldToShootCheck(projectileClass);


        //if (projectiles.Count < maxprojectiles && invScript.paused == false) {
        //    if (Input.GetKeyDown(KeyCode.F)) {
        //        var playerController = gameObject.GetComponent<PlayerController>();
        //        StartCoroutine(playerController.ShootingAnimations());
        //        Instantiate(projectile, transform.position + projectileOffset, transform.rotation);
        //    }
        //}
    }

    void HoldToShootCheck(ISpecialAbilities projectileClass) {

        Vector2 launchDirection;

        if (Input.GetAxisRaw("Vertical") > 0)
            launchDirection = guanoBarrierLaunchDirections[0];
        else if (projectileOffset.normalized.x < 0)
            launchDirection = guanoBarrierLaunchDirections[2];
        else
            launchDirection = guanoBarrierLaunchDirections[1];

        //print(launchDirection);
        bool barrierReset = false;
        if (guanoBarrierHit != null)
            barrierReset = guanoBarrierHit.reset;
        var barrierAmmo = projectileClass.AbilityAmmoIncrement(0).ammoReturn;
        if (guanoBarrierLaunched || barrierReset || barrierAmmo == 0)
            return;

        if (Input.GetKey(KeyCode.F)) {
            if (guanoBarrier != null && !guanoBarrierEnabled) {
                guanoBarrierEnabled = true;
                guanoBarrier.GetComponent<GuanoBarrierAnimation>().GuanoBarrierSpriteSwitch(guanoBarrierEnabled);
            } else if (!guanoBarrierEnabled) { 
                guanoBarrier = Instantiate(projectileClass.AbilityProjectile(), spriteTransform);
                guanoBarrierHit = guanoBarrier.GetComponent<GuanoBarrierHit>();
                guanoBarrierHit.guanoBarrierAbility = projectileClass;
                guanoBarrierHit.playerSpriteTransform = spriteTransform;
                guanoBarrierHit.shootingScript = this;
            }
        } else if (guanoBarrierEnabled) {
            guanoBarrier.transform.parent = null;
            guanoBarrier.GetComponent<GuanoBarrierMovement>().LaunchBarrier(launchDirection);
            guanoBarrierLaunched = true;
        }
    }

    void PressToShootCheck(ISpecialAbilities projectileClass) {
        if (Input.GetKeyDown(KeyCode.F)) {
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
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + projectileOffset);
    }
}
