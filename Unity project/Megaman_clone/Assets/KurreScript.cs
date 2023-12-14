using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurreScript : MonoBehaviour
{

    private SpriteRenderer Renderer;
    Collider m_Collider;
    private Animator anim;

    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<Collider>();
        anim = GameObject.Find("Kurre").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Kurre").GetComponent<EnemyManager>().enemyHp == 0)
        {
            anim.Play("kurreDeath");

        }
    }

    public void kurreDeath()
    {
        Renderer.enabled = false;
        m_Collider.enabled = false;
    }

}
