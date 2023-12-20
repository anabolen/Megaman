using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PlayerController controller;
    PlayerClimbing playerClimbing;
    PlayerManager manager;

    private void Awake() {
        controller = GetComponentInParent<PlayerController>();
        manager = GetComponentInParent<PlayerManager>();
        animator = GetComponent<Animator>();
        playerClimbing = GetComponentInParent<PlayerClimbing>();

    }
    void Update() {
        if (controller.dying) {
            animator.Play(PlayerController.PlayerAnimatorStates.Death.ToString());
            return;
        }
        if (controller.spawning) {
            animator.Play(PlayerController.PlayerAnimatorStates.Spawn.ToString());
            return;
        }
        if (!playerClimbing.climbing)
            animator.Play(controller.playerAnimation.ToString());
    }

    public void QuitDying() {
        controller.dying = false;
    }

    public void QuitSpawning() {
        controller.spawning = false;
    }
}
