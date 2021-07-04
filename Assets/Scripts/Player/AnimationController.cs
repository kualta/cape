using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public RuntimeAnimatorController[] animatorControllers;
    public Animator animator;
    public PlayerController controller;

    public void OnIdle() {
        animator.runtimeAnimatorController = animatorControllers[0];
    }

    public void OnWalk() {
        animator.runtimeAnimatorController = animatorControllers[1];
    }

    public void OnSprint() {
        animator.runtimeAnimatorController = animatorControllers[2];
    }

    public void OnJump() {
        animator.runtimeAnimatorController = animatorControllers[3];
    }

    void Start() {
        controller = transform.parent.gameObject.GetComponent<PlayerController>();
    }
}
