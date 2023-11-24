using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tykki : MonoBehaviour
{
    public void Start()
    {
        // existing components on the GameObject
        AnimationClip clip;
        Animator anim;

        // new event created
        AnimationEvent evt;
        evt = new AnimationEvent();


        evt.intParameter = 12345;
        evt.time = 1.3f;


        anim = GetComponent<Animator>();
        clip = anim.runtimeAnimatorController.animationClips[0];
        clip.AddEvent(evt);
    }
}
  
