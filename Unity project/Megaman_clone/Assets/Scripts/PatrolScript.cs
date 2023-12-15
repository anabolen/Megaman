using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PatrolScript : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    private Animator kurrewalk;
    public float speed;
    private float nextspeed;        
    public void SetSpeed(float newspeed, float delay = 5f)
    {
        nextspeed = newspeed;
        speed = 0;
        Invoke("Invokedspeed",delay);
    }
    public void Invokedspeed()
    {
        speed = nextspeed;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        kurrewalk = GetComponent<Animator>();
        currentPoint = pointB.transform;
        kurrewalk.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)

        {
            flip();
            currentPoint = pointA.transform; 
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)

        {
            flip();
            currentPoint = pointB.transform; 
        }
    }
    private void flip()
    {
        Vector3 localscale = transform.localScale;
        localscale.x *= -1;
        transform.localScale = localscale;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
