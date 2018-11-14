using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour {

    [SerializeField] public Camera camera;
    [SerializeField] public GameObject player;


    public bool isEnemyCleared;
    public int enemyQuantity;
    private float temp;

    void Start () {
        isEnemyCleared = true;
        enemyQuantity = 0;
    }

    public void increseEnemy () {
        enemyQuantity++;
    }

    public void decreseEnemy(){
        enemyQuantity--;
    }

    void checkEnemyQuantity () {
        if (enemyQuantity == 0) {
            isEnemyCleared = true;
        } else {
            isEnemyCleared = false;
        }
    }
    public void blockingAreaActive () {
        
    }
    void blockingCamera () {

    }

    void FixedUpdate () {
        checkEnemyQuantity ();
    }

}