using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyDestroy : MonoBehaviour
{
    FlyingEnemyMovementScript flyingEnemy;
    GameObject flyingEnemySprite;
    void Awake()
    {
        flyingEnemy = GetComponentInChildren<FlyingEnemyMovementScript>();
        flyingEnemySprite = GameObject.Find("FlyingEnemySprite");
    }
    void Update()
    {
        if (flyingEnemy == null) {
            Destroy(flyingEnemySprite);
            Destroy(gameObject);
        }
    }
}
