using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour {
    public  string animationState;
    public Animator animator;
    void Start () {
        animationState = this.GetComponent<movementController> ().animationState;
        animator = this.GetComponent<Animator> ();
    }

    private void changeAnimation () {
        switch (animationState) {
            default : animator.SetTrigger (animationState);
            break;
            case "Walk":
                    animator.SetBool ("IsWalkButtonActive", true);
                break;
            case "Dash":
                    break;
            case "Jump":
                    break;
            case "Attack":
                    break;
            case "RapidfireSkill":
                    break;
            case "Knockback":
                    break;

        }
    }
    void Update () {
        changeAnimation();
    }
}