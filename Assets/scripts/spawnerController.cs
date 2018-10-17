using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class spawnerController : MonoBehaviour {

	public GameObject crete;
	public GameObject enemy;

	public GameObject boss;

	public GameObject boss_hp;	
	public int enemyQuantity;

	public bool isEnemySpawner;
	public bool isCreteSpawner;
	public bool isBossZone;
	private int enemyStack;
	private Vector2 position;

	void Awake()
	{
		boss_hp.SetActive(false);
		
	}
	
	void spawnEnemy(int quantity){
		enemyStack = quantity;
		position = transform.position;
		initNextEnemy();
	}

	void initNextEnemy(){
		int randomSpace = Random.Range(3,5);
		if(enemyStack > 0){
			Debug.Log(randomSpace);
			GameObject temp = Instantiate (enemy, new Vector2(position.x + 10 + (enemyStack*randomSpace) ,position.y) , Quaternion.identity);
			temp.name = ""+this.name+enemyStack;
			enemyStack--;
			initNextEnemy();
		}
	}

	void spawnCrete(){
		Instantiate (crete, new Vector2(position.x + 25 ,position.y) , Quaternion.identity);
	}

	void spawnBoss(){
		Instantiate (boss, new Vector2(position.x + 25 ,position.y) , Quaternion.identity);
		boss_hp.SetActive(true);
	}
	private void OnCollisionEnter2D (Collision2D other) {

		if (other.collider.CompareTag ("Player")) {
			Destroy(gameObject);
		}

		if(enemyQuantity!=0) spawnEnemy(enemyQuantity);
		if(isCreteSpawner) spawnCrete();
		if(isBossZone) spawnBoss();
		
	}

}
