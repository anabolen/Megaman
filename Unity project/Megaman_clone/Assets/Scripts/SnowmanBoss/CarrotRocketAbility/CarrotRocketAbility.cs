using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotRocketAbility : SnowmanBossAbility
{
    GameObject carrotProjectile;
    public float direction;

    public override void AbilityBehaviour() {
        carrotProjectile = Resources.Load<GameObject>("Prefabs/BossPrefabs/IntercontinentalCarrot");
        LaunchCarrot();
    }

    public void LaunchCarrot() {
        var projectile = Instantiate(carrotProjectile);
        projectile.GetComponent<BossCarrotProjecile>().bossDirection = direction;
        var bossPosition = GameObject.Find("SnowmanBoss").GetComponent<Transform>().position;
        projectile.transform.position = new Vector2(bossPosition.x, bossPosition.y + 3);
    }
}
