using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour {

    [SerializeField] public Camera camera;
    public bool isEnemyCleared;
    public int enemyQuantity;
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
    void blockingArea () {

    }
    void blockingCamera () {

    }

    void Update () {
        checkEnemyQuantity ();
    }
}