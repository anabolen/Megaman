using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PlayerController controller;

    private void Awake() {
        controller = GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();

    }
    void Update() {
        animator.Play(controller.playerAnimation.ToString());
    }
}
