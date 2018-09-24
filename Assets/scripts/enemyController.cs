using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour {
    public float speed;
    public float stopDistance;
    public float currentHp;
    public float maxHp;
    public int attack_power;

    public float attack_rate;
    public float next_attack;
    public enemyAnimationController anim;
    private Animator enemy_animator;
    private Transform target;
    public healthBar healthBar;

    private bool isWalking;
    private bool isAttacking;

    public Collider2D collider;
    void Start () {
        target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
        currentHp = maxHp;
        isWalking = false;
        isAttacking = false;
        enemy_animator = anim.getEnemyAnimator ();
    }

    public int getAttackPower () {
        return attack_power;
    }
    public void takeDamage (int damage) {
        currentHp -= damage;
        float hp_size = currentHp / maxHp;
        healthBar.setSize (hp_size);
    }

    void die () {
        if (currentHp <= 0) {
            enemy_animator.SetBool ("isHpLessThanOne", true);
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
            walk ();
            transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
        } else {
            attack ();
        }
    }

    void walk () {
        if (isWalking) {
            enemy_animator.SetBool ("isWalk", true);
            enemy_animator.SetBool ("isAttack", false);
        }
    }

    void attack () {
        isAttacking = true;
        next_attack = Time.time + next_attack;
        enemy_animator.SetBool ("isAttack", true);
        enemy_animator.SetBool ("isWalk", false);
        StartCoroutine (resetAttack (attack_rate));
    }

    IEnumerator resetAttack (float waitTime) {
        yield return new WaitForSeconds (waitTime);
        isAttacking = false;
        enemy_animator.SetBool ("isAttack", false);
    }
    void Update () {
        die ();
        chaseTarget ();

    }
}