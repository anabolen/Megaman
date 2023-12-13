using UnityEngine;

public class SnowmanBossAI : MonoBehaviour {

    [Header("Boss ability settings")]
    [SerializeField] float[] phaseTransitionPercentages;
    public SnowmanBossAbility[] bossAbilities;

    [Header("Boss ability stats")]
    [SerializeField] float closeRangeDistance;
    [SerializeField] float carrotRocketCooldownDuration;
    [SerializeField] float buttSlamCooldownDuration;
    [SerializeField] float whirlCooldownDuration;
    public int buttSlamDamage = 5;
    public float splashKnockback = 20;
    public int whirlDamage = 5;
    public float whirlKnockback = 20;

    [SerializeField] Transform playerTransform;
    Animator animator;
    BossHealth healthScript;
    float maxHealth;
    string currentAbilityName;
    public float bossDirection;
    Vector2 distanceVectorFromPlayer; 
    string bossCarrotAnim = "BossShoot", bossButtSlamAnim = "BossButtSlam", bossWhirl = "BossWhirlWind";


    float behaviourStartTime, behaviourCooldownDuration;

    void Start() {
        healthScript = GetComponent<BossHealth>();
        animator = GetComponentInChildren<Animator>();
        maxHealth = healthScript.maxHealth;
        //playerTransform = GameObject.Find("PlayerCharacter ").GetComponent<Transform>();
    }

    void FixedUpdate() {

        if (currentAbilityName == bossWhirl)
            currentAbilityName = "BossWhirl";

        while (animator.GetCurrentAnimatorStateInfo(0).IsName(currentAbilityName))
            return;

        distanceVectorFromPlayer = transform.position - playerTransform.position;
        bossDirection = new Vector2(distanceVectorFromPlayer.x, 0).normalized.x;

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
            behaviourCooldownDuration = buttSlamCooldownDuration;
        }
        else { 
            AbilityAnimation(bossCarrotAnim);
            behaviourCooldownDuration = carrotRocketCooldownDuration;
        }

    }

    void SecondPhaseBehaviour() {
        behaviourStartTime = Time.time;
        if (distanceVectorFromPlayer.magnitude < closeRangeDistance) {
            AbilityAnimation(bossWhirl);
            behaviourCooldownDuration = whirlCooldownDuration;
        }
        else {
            AbilityAnimation(bossCarrotAnim);
            behaviourCooldownDuration = carrotRocketCooldownDuration;
        }
    }

    void ThirdPhaseBehaviour() {

    }

    void FourthPhaseBehaviour() {

    }

}
