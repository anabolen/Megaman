using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class SuperGunAbility : ISpecialAbilities {

    public int AbilityID() {
        int abilityID = 1;
        return (abilityID);
    }

    public string AbilityName() {
        string abilityName = "Super gun";
        return abilityName;
    }

    public AnimatorController AbilityPlayerAnimations() {
        var playerAbilitySprites = Resources.Load<AnimatorController>("PlayerAnimations/SuperGunAbilityAnimations");
        return playerAbilitySprites;
    }

    public GameObject AbilityProjectile()
    {
        if (ammoAmount == 0)
            return null;
        //else
        var projectile = Resources.Load<GameObject>("PlayerProjectiles/SuperGunProjectile");
        return projectile;
    }

    public int ammoAmount = 20;
    private int ammoReduction = 5;

    public (int ammoReturn, bool isFinite) AbilityAmmoReduction()
    {
        int ammoReturn = ammoAmount - ammoReduction;
        Mathf.Clamp(ammoReturn, 0, 20);
        return (ammoReturn, true);
    }

    public Sprite UIAbilitySprite() {
        var UISprite = Resources.Load<Sprite>("UISprites/GunAbilityUISprite");
        return UISprite;
    }
}
