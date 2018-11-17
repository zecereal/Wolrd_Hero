using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingTree : MonoBehaviour {
	private float detonate_time;

	void Start () {
		detonate_time = 2f;
		detonate_time += Time.time;		
	}
	void FixedUpdate () {
		if(Time.time > detonate_time){
			Destroy(gameObject);
		}
	}
}