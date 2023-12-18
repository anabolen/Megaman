using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ISpecialAbilities;
using static UnityEngine.MonoBehaviour;

public class GuanoBarrierAbility : ISpecialAbilities {

    public int AbilityID() {
        return (2);
    }

    public string AbilityName() {
        return "Guano barrier";
    }

    public AbilityShootingBehaviour ShootingBehaviour() {
        return AbilityShootingBehaviour.HoldToShoot;
    }

    //public AnimatorController AbilityPlayerAnimations() {
    //    return Resources.Load<AnimatorController>("PlayerAnimations/SuperGunAbilityAnimations"); ;
    //}

    public GameObject AbilityProjectile() {
        if (ammoAmount == 0)
            return null;
        return Resources.Load<GameObject>("PlayerProjectiles/GuanoBarrier");
    }

    public int AmmoReductionPerShot() {
        return -5;
    }

    public int ammoAmount = 20;

    public (int ammoReturn, bool isFinite) AbilityAmmoIncrement(int increment) {
        ammoAmount += increment;
        ammoAmount = Mathf.Clamp(ammoAmount, 0, 20);
        return (ammoAmount, true);
    }

    public Sprite UIAbilitySprite() {
        return Resources.Load<Sprite>("UISprites/SuperGunUISprite"); ;
    }
}
