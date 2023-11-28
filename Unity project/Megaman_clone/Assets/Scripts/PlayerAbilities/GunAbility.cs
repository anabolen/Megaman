using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class GunAbility : ISpecialAbilities {

    public int AbilityID() {
        int abilityID = 1;
        return (abilityID);
    }

    public string AbilityName() {
        string abilityName = "Super gun";
        return abilityName;
    }

    public AnimatorController AbilityPlayerAnimations() {
        var playerAbilitySprites = Resources.Load<AnimatorController>("PlayerAnimations/GunAbilityAnimations");
        return playerAbilitySprites;
    }

    public void AbilitySequence() {
        //Launches super gun projectile(s)
    }

    public Sprite UIAbilitySprite() {
        var UISprite = Resources.Load<Sprite>("UISprites/GunAbilityUISprite");
        return UISprite;
    }
}
