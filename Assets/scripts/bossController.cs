using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class bossController : MonoBehaviour {
    public float speed;
    public float stopDistance;
    public float currentHp;
    public float maxHp;

    public Slider boss_hp;
    public int attack_power;

    public float attack_rate;
    public float next_attack;
    public bossAnimationController anim;
    private Animator boss_animator;
    private Transform target;
    private Transform boss_sprite;
    public healthBar healthBar;

    private bool isWalking;
    private bool isAttacking;

    private bool isRight;
    public Collider2D collider;
    void Start () {
        target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
        boss_sprite = GameObject.Find ("Jason").GetComponent<Transform> ();
        currentHp = maxHp;
        isWalking = false;
        isAttacking = false;
        isRight = false;
        boss_animator = anim.getBossAnimator ();
    }

    public int getAttackPower () {
        return attack_power;
    }
    public void takeDamage (int damage) {
        currentHp -= damage;
        if(currentHp<=0) currentHp=0;
        float hp_size = currentHp / maxHp;
        boss_hp.value = hp_size;
    }

    void die () {
        if (currentHp <= 0) {
            boss_animator.SetBool ("isHpLessThanOne", true);
            StartCoroutine (DestroyItself (1f));
        }
    }
    IEnumerator DestroyItself (float waitTime) {
        Debug.Log ("dead");
        yield return new WaitForSeconds (waitTime);
        Destroy (gameObject);
    }

    void chaseTarget () {

        if (Vector2.Distance (transform.position, target.position) > stopDistance && !isAttacking) {
            isWalking = true;
            walk ();
            transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
        } else {
            attack ();
        }

    }

    void walk () {
        if (isWalking) {
            boss_animator.SetBool ("isWalk", true);
            boss_animator.SetBool ("isAttack", false);
        }

        if (transform.position.x < target.position.x) {
            if (!isRight) Flip ();
        } else if (transform.position.x > target.position.x) {
            if (isRight) Flip ();
        }
    }

    void Flip () {
        isRight = !isRight;
        Vector3 theScale = boss_sprite.localScale;
        theScale.x *= -1;
        boss_sprite.localScale = theScale;
    }
    void attack () {
        isAttacking = true;
        next_attack = Time.time + next_attack;
        boss_animator.SetBool ("isAttack", true);
        boss_animator.SetBool ("isWalk", false);
        StartCoroutine (resetAttack (attack_rate));
    }

    IEnumerator resetAttack (float waitTime) {
        yield return new WaitForSeconds (waitTime);
        isAttacking = false;
        boss_animator.SetBool ("isAttack", false);
    }
    void Update () {
        die ();
        chaseTarget ();
    }
}