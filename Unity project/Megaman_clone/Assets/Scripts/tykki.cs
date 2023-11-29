using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    public GameObject tykkiprojectile;
   

    //void Awake()
    //{

    //    InvokeRepeating("LaunchProjectile", 2.0f, 1f);
    //}
    
    void LaunchProjectile()
    {
        
        Instantiate(tykkiprojectile, transform.position, transform.rotation);
    }
}
