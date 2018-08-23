using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementController : MonoBehaviour {
	public GameObject player;
	public LeftButton leftButton;
	public RightButton rightButton;
	public jumpButtonController jumpButton;
	public LayerMask playerMask;

	public float speed = 10, jumpVelocity = 100f;
	Transform myTrans, tagGround;
	Rigidbody2D myBody;
	public bool isGrounded = false;

	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
		myTrans = this.transform;
		leftButton = FindObjectOfType<LeftButton> ();
		rightButton = FindObjectOfType<RightButton> ();
		jumpButton = FindObjectOfType<jumpButtonController> ();
		tagGround = GameObject.Find (this.name + "/tag_ground").transform;

	}

	void Move (float horizonalInput) {
		Vector2 moveVel = myBody.velocity;
		moveVel.x = horizonalInput * speed;
		myBody.velocity = moveVel;
	}

	void movement () {
		if (leftButton.Pressed || Input.GetKeyDown ("a")) {
			Move (-1);
		} else if (rightButton.Pressed || Input.GetKeyDown ("d")) {
			Move (1);
		} else {
			Move (0);
		}
	}

	public void jump () {
		if (jumpButton.Pressed || Input.GetKeyDown ("space")) {
			if (isGrounded) {
				myBody.velocity += jumpVelocity * Vector2.up;
			}
		}
	}

	void Update () {
		isGrounded = Physics2D.Linecast (myTrans.position, tagGround.position, playerMask);

		movement ();
		jump ();
	}
}