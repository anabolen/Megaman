using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FoxJumpAbility : ISpecialAbilities { 

    public int AbilityID() {
        return (1);
    }

    public string AbilityName() {
        return "Fox jump";
    }

    //public AnimatorController AbilityPlayerAnimations() {
    //    return Resources.Load<AnimatorController>("PlayerAnimations/FoxJumpAbilityAnimations");
    //}

    public static bool foxJumpProjectileExists;
    public static bool ignorePlayerCollisions;

    public int AmmoReductionPerShot() {
        return -5;
    }

    public int ammoAmount = 20;

    public (int ammoReturn, bool isFinite) AbilityAmmoIncrement(int increment)
    {
        ammoAmount += increment;
        Mathf.Clamp(ammoAmount, 0, 20);
        return (ammoAmount, true);
    }

    public GameObject AbilityProjectile()
    {
        if (foxJumpProjectileExists == true || ammoAmount == 0) { 
            return null;
        }
        else {
            foxJumpProjectileExists = true;
            ignorePlayerCollisions = true;
            return Resources.Load<GameObject>("PlayerProjectiles/FoxJumpProjectile");
        }
    }


    public Sprite UIAbilitySprite() {
        return Resources.Load<Sprite>("UISprites/FoxJumpUISprite"); 
    }
}

