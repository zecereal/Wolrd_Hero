using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlocking : MonoBehaviour {

	public GameObject camera;
	private bool isPlayerStay;
	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerStay2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			isPlayerStay = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			isPlayerStay = false;
		}
	}

	void FixedUpdate () {
		if (isPlayerStay) {
			camera.transform.position = new Vector2 (this.transform.position.x, camera.transform.position.y);
		}
	}
}