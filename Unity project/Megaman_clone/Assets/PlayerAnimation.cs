using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PlayerController controller;
    PlayerClimbing playerClimbing;

    private void Awake() {
        controller = GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();
        playerClimbing = GetComponentInParent<PlayerClimbing>();

    }
    void Update() {
        if (!playerClimbing.climbing)
            animator.Play(controller.playerAnimation.ToString());
    }
}
