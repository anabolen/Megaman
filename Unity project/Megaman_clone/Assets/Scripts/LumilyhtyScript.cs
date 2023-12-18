using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LumilyhtyScript : MonoBehaviour


{
    PolygonCollider2D polygonCollider;
    private Animator anim;

    private void OnTriggerEnter2D(Collider2D other)
    {

        {
            
            GetComponent<PolygonCollider2D>();   
            anim = GetComponent<Animator>();
            anim.Play("Lumilyhtyexplosion");
            
            
        }

    }
}