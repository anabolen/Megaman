using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurreScript : MonoBehaviour
{

    private SpriteRenderer Renderer;
    Collider m_Collider;
    private Animator anim;
    bool died = false;

    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<Collider>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<EnemyManager>().enemyHp == 0 && !died)
        {
            anim.Play("KurreDeath");
            died = true;

        }
    }
}

//    public void kurreDeath()
//    {
//        Renderer.enabled = false;
//        m_Collider.enabled = false;
//    }

//}
