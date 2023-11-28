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

    public GameObject AbilityProjectile()
    {
        if (foxJumpProjectileExists == true)
            return null;
        else
            foxJumpProjectileExists = true;
            return Resources.Load<GameObject>("PlayerProjectiles/FoxJumpProjectile");
    }

    public Sprite UIAbilitySprite() {
        var UISprite = Resources.Load<Sprite>("UISprites/FoxJumpUISprite");
        return UISprite;
    }
}

