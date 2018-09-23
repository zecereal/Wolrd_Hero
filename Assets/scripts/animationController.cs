using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour {
    public  string animationState;
    public movementController player;
    public Animator animator;
    void Start () {

        player = this.GetComponentInParent<movementController>();
        animator = this.GetComponent<Animator> ();
        
    }

    public void changeAnimation (string animationState) {
        setAllParametersFalse();
        switch (animationState) {
            case "Idle":
                animator.SetBool("isIdle",true);
                break;
            case "Walk":
                animator.SetBool ("isWalkButtonActive", true);
                break;
            case "Dash":
                break;
            case "Jump":
                break;
            case "Attack":
                Attack();
                break;
            case "RapidfireSkill":
                break;
            case "Knockback":
                break;

        }

    }

    void Attack(){
        //animator.SetBool ("isAttackButtonActive", true);
        animator.Play("Attack");
        Debug.Log("Attack is work");
    }
    
    void setAllParametersFalse () {
        animator.SetBool ("isIdle", false);
        animator.SetBool ("isHurt", false);
        animator.SetBool ("isHpLessThanZero", false);
        animator.SetBool ("isAttackButtonActive", false);
        animator.SetBool ("isWalkButtonActive", false);
        animator.SetBool ("isDashButtonActive", false);
        animator.SetBool ("isJumpButtonActive", false);
        animator.SetBool ("isSkillButtonActive", false);
        animator.SetBool ("isChangeHeroButtonActive", false);
    }

}