using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAnimationController : MonoBehaviour {
    public enemyController enemy;
    public Animator enemy_animator;
    void Start () {

        enemy = this.GetComponentInParent<enemyController> ();
        enemy_animator = this.GetComponent<Animator> ();

    }

    public Animator getEnemyAnimator(){
        return enemy_animator;
    }
    

    public void changeAnimation (string animationState) {
        switch (animationState) {
            case "Idle":
                enemy_animator.Play ("Idle");
                break;
            case "Walk":
                enemy_animator.Play ("Walk");
                break;
            case "Attack":
                enemy_animator.Play ("Attack");
                break;
            case "Dead":
                enemy_animator.Play ("Dead");
                break;

        }
    }

    public void setAllParametersFalse () {
        enemy_animator.SetBool ("isIdle", false);
        enemy_animator.SetBool ("isWalk", false);
        enemy_animator.SetBool ("isHpLessThanZero", false);
        enemy_animator.SetBool ("isAttack", false);
        
    }

}