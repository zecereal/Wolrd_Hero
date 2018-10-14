using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
    public  string animationState;
    public PlayerController player;
    public Animator animator;
    void Start () {

        player = this.GetComponentInParent<PlayerController>();
        animator = this.GetComponent<Animator> ();
        
    }
}