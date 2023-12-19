using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SquirrelScript : MonoBehaviour
{
    public float normalspeed = 1;
    public float angryspeed = 4;
    private Animator anim;
    bool transformed = false;
    public GameObject kurre;
    public KurreScript kurreScript;

    // Start is called before the first frame update
    void Start()
    {
            kurre.GetComponent<PatrolScript>().SetSpeed(normalspeed, 0f);
            anim = kurre.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == 7 && !transformed && kurreScript.died == false) {
            
            anim.Play("KurreTransform");
            kurre.GetComponent<PatrolScript>().SetSpeed(angryspeed, 0.5f);
            transformed = true;
        }
        
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            print("print2");

            kurre.GetComponent<PatrolScript>().SetSpeed(normalspeed, 0f);
            anim.Play("KurreWalk");
            transformed = false;
        }
    }
}


