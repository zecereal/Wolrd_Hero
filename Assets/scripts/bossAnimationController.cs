using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAnimationController : MonoBehaviour {
    public bossController boss;
    public Animator boss_animator;
    void Start () {

        boss = this.GetComponentInParent<bossController> ();
        boss_animator = this.GetComponent<Animator> ();

    }

    public Animator getBossAnimator () {
        return boss_animator;
    }

}