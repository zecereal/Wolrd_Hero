using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour {
    public float speed;
    public float stopDistance;

    public float currentHp;
    public float maxHp;
    private Transform target;
    private GameObject target_hero;
    [SerializeField]
    private healthBar healthBar;
    void Start () {
        target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
        currentHp = maxHp;
    }

    public void takeDamage(int damage){
        currentHp -= damage;
        float hp_size = currentHp/maxHp;
        healthBar.setSize(hp_size);
    }

    void die(){
        if(currentHp <= 0){
            Destroy(gameObject);
        }
    }
    void attack(){

    }
    void chaseTarget(){
        if (Vector2.Distance (transform.position, target.position) > stopDistance) {
            transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
        }else {
            attack();
        }
    }
    void Update () {
        die();
        chaseTarget();
    }
}