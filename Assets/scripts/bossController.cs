using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossController : MonoBehaviour {
    public float speed;
    public float stopDistance;
    public float currentHp;
    public float maxHp;

    public Slider boss_hp;
    public int attack_power;

    public float attack_rate;
    public float next_attack;

    public GameObject growing_tree;

    public BossAnimationController anim;
    private Animator boss_animator;
    private Transform target;
    private Transform boss_sprite;

    private bool isWalking;
    private bool isAttacking;

    private bool isRight;

    private float skillCooldown = 10f;
    private float next_skill = 0.0f;
    public Collider2D collider;

    private float charging_time = 3f;

    private bool isHit;

    private bool isSkill;

    private bool isCharging;

    void Start () {
        target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
        boss_sprite = GameObject.Find ("Jason").GetComponent<Transform> ();
        currentHp = maxHp;
        isWalking = false;
        isAttacking = false;
        isRight = false;
        isSkill = false;
        isCharging = false;
        boss_hp = GameObject.Find ("boss_hp").GetComponent<Slider> ();
        boss_animator = anim.getBossAnimator ();

        next_skill = skillCooldown;
    }

    public int getAttackPower () {
        return attack_power;
    }
    public void takeDamage (int damage) {
        currentHp -= damage;
        if (currentHp <= 0) currentHp = 0;
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
        Instantiate (growing_tree,new Vector2(this.transform.position.x,this.transform.position.y+5), Quaternion.identity);
    }

    void chaseTarget () {

        if (Vector2.Distance (transform.position, target.position) > stopDistance) {
            if (Time.time > next_skill) {
                
                chargeAndChase ();
            } else if (Time.time <= next_skill && !isSkill) {
                isWalking = true;
                walk ();
            }
        } else {
            attack ();
        }

        if(isCharging){
            transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
        }
    }

    void walk () {
        if (isWalking) {
            boss_animator.SetBool ("isWalk", true);
            boss_animator.SetBool ("isAttack", false);
        }

        transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
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

    void chargeAndChase () {
        isSkill = true;
        isCharging = true;
        isWalking = false;

        next_skill = Time.time + skillCooldown;
        boss_animator.SetBool ("isSkill", true);
        boss_animator.SetBool ("isAttack", false);
        boss_animator.SetBool ("isWalk", false);
        speed = 3;
        
        StartCoroutine (hitAttack (charging_time));
    }

    IEnumerator hitAttack (float waitTime) {
        yield return new WaitForSeconds (waitTime);
        boss_animator.SetBool ("isAttackHitted", true);
        isCharging = false;
        StartCoroutine (resetSkill (3f));
    }

    IEnumerator resetSkill (float waitTime) {
        boss_animator.SetBool ("isSkill", false);
        yield return new WaitForSeconds (waitTime);
        boss_animator.SetBool ("isAttackHitted", false);
        isSkill = false;
        speed = 1;
    }
    void FixedUpdate () {
        die ();
        chaseTarget ();

        if (transform.position.x < target.position.x) {
            if (!isRight) Flip ();
        } else if (transform.position.x > target.position.x) {
            if (isRight) Flip ();
        }
        
        Physics2D.IgnoreLayerCollision (9, 12);
    }

    private void OnCollisionEnter2D (Collision2D other) {
        if (other.collider.CompareTag ("Player")) {
            isHit = true;
            Debug.Log("Hit");
        }
    }

    private void OnCollisionExit (Collision other) {
        if (other.collider.CompareTag ("Player")) {
            isHit = false;
        }
    }
}