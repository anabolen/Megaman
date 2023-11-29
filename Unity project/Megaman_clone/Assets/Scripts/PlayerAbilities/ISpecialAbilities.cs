using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public interface ISpecialAbilities {

    //enum AbilityType { foxJumpAbility, gunAbility }

    //(int abilityID, string abilityName) DefineAbilityID();

    int AbilityID();

    (int ammoReturn, bool isFinite) AbilityAmmoReduction();

    string AbilityName();

    GameObject AbilityProjectile();

    AnimatorController AbilityPlayerAnimations();

    Sprite UIAbilitySprite();

}
