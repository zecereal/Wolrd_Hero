using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerController : MonoBehaviour {

	public GameObject crete;
	public GameObject enemy;
	public GameObject boss;

	public int enemyQuantity;
	public bool isBossZone;

	private int enemyStack;
	private Vector2 position;

	void spawnEnemy(int quantity){
		enemyStack = quantity;
		position = transform.position;
		initNextEnemy();
	}

	void initNextEnemy(){
		if(enemyStack > 0){
			GameObject temp = Instantiate (enemy, new Vector2(position.x + 10 + enemyStack ,position.y) , Quaternion.identity);
			temp.name = ""+this.name+enemyStack;
			enemyStack--;
			initNextEnemy();
		}
	}
	void spawnBoss(){

	}

	private void OnCollisionEnter2D (Collision2D other) {

		if (other.collider.CompareTag ("Player")) {
			Destroy(gameObject);
		}

		if(enemyQuantity!=0) spawnEnemy(enemyQuantity);
		if(isBossZone) spawnBoss();
		
	}

}
