using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanBossAI : MonoBehaviour {

    [SerializeField] SnowmanBossAbility[] bossAbilities;
    [SerializeField] float[] phaseTransitionPercentages;
    SnowmanBossHealth healthScript;
    float maxHealth;

    [SerializeField] float CarrotRocketCooldownDuration;

    float BehaviourStartTime, BehaviourCooldownDuration;

    void Awake() {
        healthScript = GetComponent<SnowmanBossHealth>();
        maxHealth = healthScript.maxHealth;
    }

    void FixedUpdate() {
        if (BehaviourStartTime + BehaviourCooldownDuration > Time.time)
            return;
        var healthPercentage = healthScript.health / maxHealth;
        if (healthPercentage > phaseTransitionPercentages[0])
            FirstPhaseBehaviour();
        else if (healthPercentage > phaseTransitionPercentages[1])
            SecondPhaseBehaviour();
        else if (healthPercentage > phaseTransitionPercentages[2])
            ThirdPhaseBehaviour();
        else
            FourthPhaseBehaviour();
    }

    void FirstPhaseBehaviour() {
        BehaviourStartTime = Time.time;
        BehaviourCooldownDuration = CarrotRocketCooldownDuration;
        bossAbilities[0].AbilityBehaviour();
        var carrot = (bossAbilities[0] as CarrotRocketAbility);
        carrot.LaunchCarrot();
    }

    void SecondPhaseBehaviour() {

    }

    void ThirdPhaseBehaviour() {

    }

    void FourthPhaseBehaviour() {

    }

}
