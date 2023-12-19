using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurreScript : MonoBehaviour
{

    private SpriteRenderer Renderer;
    Collider2D m_Collider;
    private Animator anim;
    public bool died = false;
    int previousHp;

    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        previousHp = GetComponent<EnemyManager>().enemyHp;    
    }

    // Update is called once per frame
    void Update() 
    {
        if (GetComponent<EnemyManager>().enemyHp < previousHp) {            
            previousHp = GetComponent<EnemyManager>().enemyHp;
            AudioFW.Play("SquirrelDamaged");

        }
        if (GetComponent<EnemyManager>().enemyHp == 0 && !died)
        {

            anim.Play("KurreDeath");
            died = true;
            AudioFW.Play("Kurredeath");
        }
    }
    
    public void kurreDeath()
    {
        Renderer.enabled = false;
        m_Collider.enabled = false;
        anim.enabled = false;
    }

}