using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour {
    public float speed;
    public float stopDistance;

    public int health;
    private Transform target;

    void Start () {
        target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
    }

    public void takeDamage(int damage){
        health -= damage;
    }

    void die(){
        if(health <= 0){
            Destroy(gameObject);
        }
    }

    void chaseTarget(){
        if (Vector2.Distance (transform.position, target.position) > stopDistance) {
            transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
        }
    }
    void Update () {
        die();
        chaseTarget();
    }
}