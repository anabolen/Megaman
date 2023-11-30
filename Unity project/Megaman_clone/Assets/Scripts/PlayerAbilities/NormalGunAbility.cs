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
        int abilityID = 2;
        return (abilityID);
    }

    public string AbilityName()
    {
        string abilityName = "Normal gun";
        return abilityName;
    }

    public AnimatorController AbilityPlayerAnimations()
    {
        var playerAbilitySprites = Resources.Load<AnimatorController>("PlayerAnimations/NormalAbilityAnimations");
        return playerAbilitySprites;
    }

    public static List<GameObject> normalProjectiles = new();
    public int maxprojectiles = 3;

    public GameObject AbilityProjectile()
    {
        var projectile = Resources.Load<GameObject>("PlayerProjectiles/NormalProjectile");

        if (normalProjectiles.Count + 1 > maxprojectiles) {
            Console.WriteLine("lol");
            return null;
        }
        normalProjectiles.Add(projectile);
        return projectile;
    }

    public (int ammoReturn, bool isFinite) AbilityAmmoReduction()
    {
        int ammoReturn = 0;
        return (ammoReturn, false);
    }

    public Sprite UIAbilitySprite()
    {
        var UISprite = Resources.Load<Sprite>("UISprites/GunAbilityUISprite");
        return UISprite;
    }
}
