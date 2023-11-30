using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class FoxJumpAbility : ISpecialAbilities { 

    public int AbilityID() {
        int abilityID = 0;
        return (abilityID);
    }

    public string AbilityName() {
        string abilityName = "Fox jump";
        return abilityName;
    }

    public AnimatorController AbilityPlayerAnimations() {
        var playerAbilitySprites = Resources.Load<AnimatorController>("PlayerAnimations/FoxJumpAbilityAnimations");
        return playerAbilitySprites;
    }

    public static bool foxJumpProjectileExists;
    public static bool ignorePlayerCollisions;

    public GameObject AbilityProjectile()
    {
        Console.WriteLine(ammoAmount);
        if (foxJumpProjectileExists == true || ammoAmount == 0) { 
            return null;
        }
        else {
            foxJumpProjectileExists = true;
            ignorePlayerCollisions = true;
            return Resources.Load<GameObject>("PlayerProjectiles/FoxJumpProjectile");
        }
    }

    public int ammoAmount = 20;
    private int ammoReduction = 5;

    public (int ammoReturn, bool isFinite) AbilityAmmoReduction()
    {
        ammoAmount -= ammoReduction;
        Mathf.Clamp(ammoAmount, 0, 20);
        return (ammoAmount, true);
    }


    public Sprite UIAbilitySprite() {
        var UISprite = Resources.Load<Sprite>("UISprites/FoxJumpUISprite");
        return UISprite;
    }
}

