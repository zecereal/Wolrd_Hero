using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatmanScript : MonoBehaviour {

	bool facingLeft = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetKeyDown("a")){
			Flip();
		}
	}

	void Flip(){
		facingLeft = !facingLeft;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
