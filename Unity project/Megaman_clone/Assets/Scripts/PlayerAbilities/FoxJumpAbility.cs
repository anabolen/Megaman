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

    public void AbilitySequence() { 
        //Launches fox object on front of the player
    }

}

