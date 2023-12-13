using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotRocketAbility : SnowmanBossAbility
{
    GameObject carrotProjectile;
    public float direction;
    SnowmanBossAI bossAI;
    SnowmanAnimFunctions animFunctions;

    public override void AbilityBehaviour() {
        carrotProjectile = Resources.Load<GameObject>("Prefabs/BossPrefabs/IntercontinentalCarrot");
        bossAI = GameObject.Find("SnowmanBoss").GetComponent<SnowmanBossAI>();
        animFunctions = GameObject.Find("SnowmanBoss").GetComponentInChildren<SnowmanAnimFunctions>();
        LaunchCarrot();
    }

    public void LaunchCarrot() {
        var projectile = Instantiate(carrotProjectile);
        var offset 
            = new Vector2(animFunctions.projectileOffset.x * -bossAI.bossDirection, animFunctions.projectileOffset.y);
        projectile.transform.position = animFunctions.transform.position + (Vector3)offset;
        projectile.transform.rotation = Quaternion.Euler(0, 180 * Mathf.Clamp(bossAI.bossDirection, 0, 1), 0);
    }
}
