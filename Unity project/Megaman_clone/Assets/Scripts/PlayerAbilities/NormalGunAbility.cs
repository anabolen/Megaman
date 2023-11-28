using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using static UnityEngine.RuleTile.TilingRuleOutput;

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

        //foreach (GameObject p in projectiles) {
        //    if (p == null) {
        //        projectiles.Remove(p);
        //    }
        //}
        if (normalProjectiles.Count + 1 > maxprojectiles) {
            return null;
        }
        normalProjectiles.Add(projectile);
        return projectile;
    }

    public Sprite UIAbilitySprite()
    {
        var UISprite = Resources.Load<Sprite>("UISprites/GunAbilityUISprite");
        return UISprite;
    }
}
