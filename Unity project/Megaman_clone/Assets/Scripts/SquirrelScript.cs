using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SquirrelScript : MonoBehaviour
{
    
    private Animator anim;
    bool transformed = false;

    public int duration = 2;

    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.Find("Kurre").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == 7 && !transformed) {
            anim.Play("KurreTransform");
            GameObject.Find("Kurre").GetComponent<PatrolScript>().speed *= 2;
            transformed = true;
        }
        
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.Play("kurrewalk");
    }
    void Update()
    {



            

    }


}


