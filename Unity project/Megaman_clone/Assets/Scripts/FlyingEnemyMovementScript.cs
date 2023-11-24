using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class FlyingEnemyMovementScript : MonoBehaviour
{
    public float speed;
    public float maxRange;
    public float maxChaseTime;
    public float maxWaitTime;
    [SerializeField] float waitTime;
    [SerializeField] Vector3 direction;
    [SerializeField] bool chaseTimeReset;
    [SerializeField] float chaseTime;
    Transform playerTransform;
    [SerializeField] bool chasing;

    void Awake()
    {
        playerTransform = GameObject.Find("PlayerCharacter ").GetComponent<Transform>();
        direction = playerTransform.position.normalized;
        chaseTime = 0;
        waitTime = maxWaitTime;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < maxRange) {
            chasing = true;
            direction = playerTransform.position.normalized;
        } else {
            chasing = false;
        }
        if (chasing == true && chaseTime < maxChaseTime) {
            transform.position += direction * speed * Time.deltaTime;
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

    }
    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }
}
