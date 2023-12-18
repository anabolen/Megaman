using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpecialAbilities {

    //enum AbilityType { foxJumpAbility, gunAbility }

    //(int abilityID, string abilityName) DefineAbilityID();

    int AbilityID();

    (int ammoReturn, int maxAmmo, bool isFinite) AbilityAmmoIncrement(int increment);

    int AmmoReductionPerShot();

    string AbilityName();

    GameObject AbilityProjectile();

    public enum AbilityShootingBehaviour { PressToShoot, HoldToShoot, Chargeable }

    AbilityShootingBehaviour ShootingBehaviour();

    //AnimatorController AbilityPlayerAnimations();

    Sprite UIAbilitySprite();

}
