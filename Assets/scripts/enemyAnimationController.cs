using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour {
    public EnemyController enemy;
    public Animator enemy_animator;
    void Start () {

        enemy = this.GetComponentInParent<EnemyController> ();
        enemy_animator = this.GetComponent<Animator> ();

    }

    public Animator getEnemyAnimator(){
        return enemy_animator;
    }
}