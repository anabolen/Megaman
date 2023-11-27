using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class FlyingEnemyMovementScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float maxRange;
    [SerializeField] float maxChaseTime;
    [SerializeField] float maxWaitTime;
    [SerializeField] float waitTime;
    [SerializeField] bool chaseTimeReset;
    [SerializeField] float chaseTime;
    Transform playerTransform;
    [SerializeField] bool chasing;
    Transform spriteTrans;

    void Awake()
    {
        playerTransform = GameObject.Find("PlayerSprite").GetComponent<Transform>();
        chaseTime = 0;
        waitTime = maxWaitTime;
        spriteTrans = GameObject.Find("FlyingEnemySprite").transform;
    }

    void Update()
    {
        transform.LookAt(playerTransform);
        if (Vector3.Distance(transform.position, playerTransform.position) < maxRange) {
            chasing = true;
        } else {
            chasing = false;
        }
        if (chasing == true && chaseTime < maxChaseTime) {
            transform.position += transform.forward * speed * Time.deltaTime;
            chaseTime += Time.deltaTime; 
            chaseTimeReset = false;
        }
        if (chaseTime >= maxChaseTime) {
            waitTime -= Time.deltaTime;
            if (waitTime < 0) { 
                waitTime = 0;
            }
        }
        if (waitTime == 0 && chaseTimeReset == false) {
            chaseTime = 0;
            waitTime = maxWaitTime;
            chaseTimeReset = true;
        }
        spriteTrans.position = transform.position;

    }
    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }
}
