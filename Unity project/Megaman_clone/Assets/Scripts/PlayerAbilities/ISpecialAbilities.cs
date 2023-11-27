using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public interface ISpecialAbilities {

    //enum AbilityType { foxJumpAbility, gunAbility }

    //(int abilityID, string abilityName) DefineAbilityID();

    int AbilityID();

    string AbilityName();

    AnimatorController AbilitySprites();

    void AbilitySequence();

}
