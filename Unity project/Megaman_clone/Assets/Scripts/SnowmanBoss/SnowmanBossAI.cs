using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanBossAI : MonoBehaviour {

    public SnowmanBossAbility[] bossAbilities;
    [SerializeField] float[] phaseTransitionPercentages;
    Animator animator;
    SnowmanBossHealth healthScript;
    float maxHealth;

    [SerializeField] float CarrotRocketCooldownDuration;

    float BehaviourStartTime, BehaviourCooldownDuration;

    void Awake() {
        healthScript = GetComponent<SnowmanBossHealth>();
        animator = GetComponentInChildren<Animator>();
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
        //bossAbilities[0].AbilityBehaviour();
        //var carrot = (bossAbilities[0] as CarrotRocketAbility);
        //carrot.LaunchCarrot();
        StartCoroutine(AbilityAnimation("BossButtSlam", "BossIdle"));
    }

    IEnumerator AbilityAnimation(string ability, string idle)  {
        animator.Play(ability);
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(ability))
            yield return null;
        animator.Play(idle);
    }

    void SecondPhaseBehaviour() {
        StartCoroutine(AbilityAnimation("BossShoot", "BossIdle"));
    }

    void ThirdPhaseBehaviour() {

    }

    void FourthPhaseBehaviour() {

    }

}
