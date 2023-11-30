using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Elevatorscript : MonoBehaviour
{
    public float speed;
    public int startingPoint;
    public Transform[] points;
    private int i;
    void Start()
    {
        transform.position = points[startingPoint].position;
    }


    void Update()
    {// Checking the distance of the platform and the point
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }// moving the platform to the point position with the index
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);

    }
    private void OnCollisionEnter2D(Collision2D collision)

    {
        collision.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}