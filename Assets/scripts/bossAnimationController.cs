using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationController : MonoBehaviour {
    public BossController boss;
    public Animator boss_animator;
    void Start () {

        boss = this.GetComponentInParent<BossController> ();
        boss_animator = this.GetComponent<Animator> ();

    }

    public Animator getBossAnimator () {
        return boss_animator;
    }

}