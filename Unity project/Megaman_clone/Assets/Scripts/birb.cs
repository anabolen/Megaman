using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birb : MonoBehaviour
    
    
{

    public Vector3 direction;
    public float speed;

    void Start()
    {
        //direction = new Vector3(1, 1, 0);
    }

    void Update()
    {
        Vector3 velocity = direction.normalized * speed;
        transform.Translate(Time.deltaTime * velocity, Space.World);
    }
}