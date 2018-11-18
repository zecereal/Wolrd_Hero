using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour {

    [SerializeField] public Camera camera;
    [SerializeField] public GameObject panel;
    [SerializeField] public GameObject victory;
    [SerializeField] public GameObject failed;

    public PlayerController player;
    public LevelLoader levelLoader;
    public bool isEnemyCleared;
    public bool isBossAppeared;
    public int enemyQuantity;
    private float temp;

    void Start () {
        player = FindObjectOfType<PlayerController> ();
        isEnemyCleared = true;
        enemyQuantity = 0;
    }

    public void increseEnemy () {
        enemyQuantity++;
    }

    public void decreseEnemy () {
        enemyQuantity--;
    }

    void checkPlayerAlive () {
        if (player.currentHp <= 0) {
            panel.SetActive (true);
            failed.SetActive (true);
            StartCoroutine (loadMainMenu (5f));
        }
    }

    void checkEnemyQuantity () {
        if (enemyQuantity == 0) {
            isEnemyCleared = true;

            if (isBossAppeared) {
                panel.SetActive (true);
                victory.SetActive (true);
                StartCoroutine (loadMainMenu (5f));
            }
        } else {
            isEnemyCleared = false;
        }

    }
    IEnumerator loadMainMenu (float waitTime) {
        yield return new WaitForSeconds (waitTime);
        levelLoader.LoadLevel (0);
    }

    void FixedUpdate () {
        checkEnemyQuantity ();
        checkPlayerAlive();
    }

}