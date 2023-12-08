using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotRocketAbility : SnowmanBossAbility
{
    GameObject carrotProjectile;
    public float direction;

    public override void AbilityBehaviour() {
        carrotProjectile = Resources.Load<GameObject>("Prefabs/BossPrefabs/IntercontinentalCarrot");
    }

    public override Animation AbilityAnimation() {
        return null;
    }

    public void LaunchCarrot() {
        var projectile = Instantiate(carrotProjectile);
        projectile.GetComponent<BossCarrotProjecile>().bossDirection = direction;
    }
}
