using UnityEngine;

public class SnowmanBossAI : MonoBehaviour {

    [Header("Boss ability settings")]
    [SerializeField] float[] phaseTransitionPercentages;
    public SnowmanBossAbility[] bossAbilities;

    [Header("Boss ability stats")]
    [SerializeField] float closeRangeDistance;
    [SerializeField] float carrotRocketCooldownDuration;
    [SerializeField] float ButtSlamCooldownDuration;
    public static int buttSlamDamage = 5;
    public static float splashKnockback = 20;

    [SerializeField] Transform playerTransform;
    Animator animator;
    SnowmanBossHealth healthScript;
    float maxHealth;
    string currentAbilityName;
    public float bossDirection;
    Vector2 distanceVectorFromPlayer; 
    string bossShootAnim = "BossShoot", bossButtSlamAnim = "BossButtSlam";


    float behaviourStartTime, behaviourCooldownDuration;

    void Start() {
        healthScript = GetComponent<SnowmanBossHealth>();
        animator = GetComponentInChildren<Animator>();
        maxHealth = healthScript.maxHealth;
        //playerTransform = GameObject.Find("PlayerCharacter ").GetComponent<Transform>();
    }

    void FixedUpdate() {

        distanceVectorFromPlayer = transform.position - playerTransform.position;
        bossDirection = new Vector2(distanceVectorFromPlayer.x, 0).normalized.x;

        while (animator.GetCurrentAnimatorStateInfo(0).IsName(currentAbilityName))
            return;

        transform.rotation = Quaternion.Euler(0, 180 * Mathf.Clamp(bossDirection, 0, 1), 0);

        if (behaviourStartTime + behaviourCooldownDuration > Time.time)
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

    void AbilityAnimation(string ability) {
        animator.Play(ability);
        currentAbilityName = ability;
    }

    void FirstPhaseBehaviour() {
        behaviourStartTime = Time.time;
        if (distanceVectorFromPlayer.magnitude < closeRangeDistance) {
            AbilityAnimation(bossButtSlamAnim);
            behaviourCooldownDuration = ButtSlamCooldownDuration;
        }
        else { 
            AbilityAnimation(bossShootAnim);
            behaviourCooldownDuration = carrotRocketCooldownDuration;
        }

    }

    void SecondPhaseBehaviour() {
    }

    void ThirdPhaseBehaviour() {

    }

    void FourthPhaseBehaviour() {

    }

}
