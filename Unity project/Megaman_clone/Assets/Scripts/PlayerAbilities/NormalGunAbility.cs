using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System;

public class NormalGunAbility : ISpecialAbilities
{

    public int AbilityID()
    {
        return (0);
    }

    public string AbilityName()
    {
        return "Normal gun";
    }

    public AnimatorController AbilityPlayerAnimations()
    {
        return Resources.Load<AnimatorController>("PlayerAnimations/NormalAbilityAnimations"); ;
    }

    public static List<GameObject> normalProjectiles = new();
    public int maxprojectiles = 3;

    public GameObject AbilityProjectile()
    {
        var projectile = Resources.Load<GameObject>("PlayerProjectiles/NormalProjectile");

        if (normalProjectiles.Count + 1 > maxprojectiles) {
            return null;
        }
        normalProjectiles.Add(projectile);
        return projectile;
    }

    public int AmmoReductionPerShot()
    {
        return 0;
    }

    public (int ammoReturn, bool isFinite) AbilityAmmoIncrement(int increment)
    {
        return (0, false);
    }

    public Sprite UIAbilitySprite()
    {
        return Resources.Load<Sprite>("UISprites/GunAbilityUISprite"); ;
    }
}
