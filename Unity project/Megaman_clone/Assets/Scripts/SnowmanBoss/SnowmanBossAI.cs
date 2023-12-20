using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SnowmanBossAI : MonoBehaviour {

    [Header("Boss ability settings")]
    [SerializeField] float[] phaseTransitionPercentages;
    public SnowmanBossAbility[] bossAbilities;
    float closeRangeDistance;
    [SerializeField] float firstPhaseCloseRangeDistance;

    [Header("Second phase settings")]
    [SerializeField] float secondPhaseCloseRangeDistance;
    [SerializeField] int whirlAttackMaxInt = 2;
    int whirlAttackInt;

    [Header("Third phase settings")]
    [SerializeField] int buttSlamMaxInt = 3;
    int buttSlamInt;
    [SerializeField] int slamWhirlComboMaxInt = 2;
    int slamWhirlComboInt;
    int shootingSequenceInt;
    [SerializeField] float thirdPhaseStartCooldownDuration;
    bool thirdPhaseStarted = false;

    [Header("Fourth phase settings")]
    [SerializeField] int megaShootMaxInt = 1;
    int megaShootInt;
    [SerializeField] int quickWhirlMaxInt = 2;
    int quickWhirlInt;

    [Header("Cooldowns")]
    [SerializeField] float carrotRocketCooldownDuration;
    [SerializeField] float buttSlamCooldownDuration;
    [SerializeField] float whirlCooldownDuration;
    [SerializeField] float secondPhaseShootCooldownDuration = 1.2f;
    [SerializeField] float sidewaysButtSlamCooldownDuration = 0.5f;
    [SerializeField] float whirlButtSlamTransitionCooldownDuration = 3;
    [SerializeField] float quickWhirlCooldownDuration = 0.5f;

    [Header("Damage and knockback")]
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
    string bossShootAnim = "BossShoot", 
        bossButtSlamAnim = "BossButtSlam", 
        bossWhirl = "BossWhirlWind", 
        bossRapidShootAnim = "BossRapidShoot",
        bossSidewaysButtSlamAnim = "BossSidewaysButtSlam",
        bossMegaShootAnim = "BossMegaShoot",
        bossQuickWhirl = "BossQuickWhirl";

    public bool transitioned = false;

    Rigidbody2D rb;

    float behaviourStartTime, behaviourCooldownDuration;

    void Awake() {
        AudioFW.StopLoop("LevelMusic");
        AudioFW.PlayLoop("BossMusic");
        healthScript = GetComponent<BossHealth>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.Find("PlayerCharacter ").GetComponent<Transform>();
        maxHealth = healthScript.maxHealth;
        //playerTransform = GameObject.Find("PlayerCharacter ").GetComponent<Transform>();
    }

    void FixedUpdate() {
        if (currentAbilityName == bossWhirl || currentAbilityName == bossQuickWhirl)
            currentAbilityName = "BossWhirl";

        while (animator.GetCurrentAnimatorStateInfo(0).IsName(currentAbilityName))
            return;

        if (currentAbilityName == "BossWhirl") {
            rb.velocity = Vector2.zero;
        }

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
        else if (healthPercentage > 0)
            FourthPhaseBehaviour();
        else
            BossDeath();
        
    }

    public void BossDeath() {
        animator.Play("BossDeath");
        AudioFW.StopLoop("BossMusic");
    }

    void AbilityAnimation(string ability) {
        animator.Play(ability);
        currentAbilityName = ability;
    }

    void FirstPhaseBehaviour() {
        behaviourStartTime = Time.time;
        closeRangeDistance = firstPhaseCloseRangeDistance;
        if (distanceVectorFromPlayer.magnitude < closeRangeDistance) {
            AbilityAnimation(bossButtSlamAnim);
            behaviourCooldownDuration = buttSlamCooldownDuration;
        }
        else { 
            AbilityAnimation(bossShootAnim);
            behaviourCooldownDuration = carrotRocketCooldownDuration;
        }

    }

    void SecondPhaseShoot() {
        AbilityAnimation(bossRapidShootAnim);
        behaviourCooldownDuration = secondPhaseShootCooldownDuration;
        transitioned = true;
    }

    void SecondPhaseBehaviour() {
        if (!transitioned) {
            SecondPhaseShoot();
            behaviourStartTime = Time.time;
            closeRangeDistance = secondPhaseCloseRangeDistance;
            return;
        }
        behaviourStartTime = Time.time;
        if (whirlAttackInt < whirlAttackMaxInt) {
            AbilityAnimation(bossWhirl);
            behaviourCooldownDuration = whirlCooldownDuration;
            whirlAttackInt++;
        } else {
            SecondPhaseShoot();
            whirlAttackInt = 0;
        }
    }

    void ThirdPhaseBehaviour() {

        //if (!thirdPhaseStarted) {
        //    thirdPhaseStarted = true;
        //    behaviourCooldownDuration = thirdPhaseStartCooldownDuration;
        //    behaviourStartTime = Time.time;
        //    //Play transition animation
        //    return;
        //}

        if (shootingSequenceInt == 0 || shootingSequenceInt == 2) {
            AbilityAnimation(bossShootAnim);
            shootingSequenceInt++;
            return;
        } else if (shootingSequenceInt == 1) {
            AbilityAnimation(bossRapidShootAnim);
            shootingSequenceInt++;
            return;
        }

        if (slamWhirlComboInt < slamWhirlComboMaxInt) {
            if (buttSlamInt < buttSlamMaxInt) {
                AbilityAnimation(bossSidewaysButtSlamAnim);
                behaviourStartTime = Time.time;
                behaviourCooldownDuration = sidewaysButtSlamCooldownDuration;
                buttSlamInt++;
            } else {
                AbilityAnimation(bossWhirl);
                behaviourCooldownDuration = whirlButtSlamTransitionCooldownDuration;
                buttSlamInt = 0;
                slamWhirlComboInt++;
            }
        } else {
            slamWhirlComboInt = 0;
            shootingSequenceInt = 0;
        }
    }

    void FourthPhaseBehaviour() {

        if (megaShootMaxInt > megaShootInt) {
            AbilityAnimation(bossMegaShootAnim);
            megaShootInt++;
            return;
        } 
        if (quickWhirlMaxInt > quickWhirlInt) {
            AbilityAnimation(bossQuickWhirl);
            behaviourCooldownDuration = quickWhirlCooldownDuration;
            behaviourStartTime = Time.time;
            quickWhirlInt++;
            return;
        } else if (quickWhirlMaxInt + 1 > quickWhirlInt) {
            megaShootInt = 0;
            quickWhirlInt++;
            return;
        } else if (quickWhirlMaxInt + 2 > quickWhirlInt) {
            behaviourStartTime = Time.time;
            behaviourCooldownDuration = sidewaysButtSlamCooldownDuration;
            quickWhirlInt++;
            return;
        }

        if (buttSlamInt < buttSlamMaxInt) {
            AbilityAnimation(bossSidewaysButtSlamAnim);
            behaviourStartTime = Time.time;
            behaviourCooldownDuration = sidewaysButtSlamCooldownDuration;
            buttSlamInt++;
        } else {
            megaShootInt = 0;
            quickWhirlInt = 0;
            buttSlamInt = 0;
        }
    }

}
