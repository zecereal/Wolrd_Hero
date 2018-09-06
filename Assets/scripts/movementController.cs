using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementController : MonoBehaviour {
	public GameObject player;
	public LayerMask playerMask;

	public float speed = 10, jumpVelocity = 100f;
	Transform myTrans, tagGround;
	Rigidbody2D myBody;
	public bool isGrounded = false;
	public bool isRight = true;

	private float dashrate = 2f;
	private float nextdash = 0.0f;

	private LeftButton leftButton;
	private RightButton rightButton;
	private jumpButtonController jumpButton;
	private DashController dashButton;
	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
		myTrans = this.transform;
		leftButton = FindObjectOfType<LeftButton> ();
		rightButton = FindObjectOfType<RightButton> ();
		jumpButton = FindObjectOfType<jumpButtonController> ();
		dashButton = FindObjectOfType<DashController> ();
		tagGround = GameObject.Find (this.name + "/tag_ground").transform;

	}

	void Move (float horizonalInput) {
		Vector2 moveVel = myBody.velocity;
		moveVel.x = horizonalInput * speed;
		myBody.velocity = moveVel;
	}

	void movement () {
		if (leftButton.Pressed || Input.GetKeyDown ("a")) {
			isRight = false;
			Move (-1);
		} else if (rightButton.Pressed || Input.GetKeyDown ("d")) {
			isRight = true;
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

	void dash () {
		if (dashButton.Pressed && Time.time > nextdash){
		//if (Input.GetKeyDown ("space") && Time.time > nextdash) {
			
			nextdash = Time.time + dashrate;

			if (isRight) {
				
				//transform.Translate (5, 0, 0);
				myBody.AddForce(Vector2.right*150000);
			} else {
				myBody.AddForce(Vector2.left*150000);
			}

			StartCoroutine (resetVelocity (5.0f));
		}
	}

	private IEnumerator resetVelocity (float waitTime) {
		Debug.Log("reset velocity");
		yield return new WaitForSeconds (waitTime);
		myBody.velocity = new Vector3 (0, 0, 0);
		dashButton.Pressed = false;
	}

	void Update () {
		isGrounded = Physics2D.Linecast (myTrans.position, tagGround.position, playerMask);

		movement ();
		jump ();
		dash ();

	}
}