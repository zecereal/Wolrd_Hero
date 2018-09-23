using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementController : MonoBehaviour {
	public GameObject player;
	public LayerMask playerMask;
	public float speed = 10, jumpVelocity = 100f, dashVelocity = 100f, checkRadius = 0.3f;
	Transform myTrans, tagGround;
	Rigidbody2D myBody;
	public bool isGrounded = false;
	public bool isRight = true;

	public string animationState;
	private Vector2 bulletPosition;
	private float firerate = 0.3f;
	private float nextfire = 0.0f;
	public GameObject bullet_Right;
	public GameObject bullet_Left;

	private float dashrate = 2f;
	private float nextdash = 0.0f;

	private LeftButton leftButton;
	private RightButton rightButton;
	private jumpButtonController jumpButton;
	private DashController dashButton;
	private AttackController attackButton;

	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
		myTrans = this.transform;
		leftButton = FindObjectOfType<LeftButton> ();
		rightButton = FindObjectOfType<RightButton> ();
		jumpButton = FindObjectOfType<jumpButtonController> ();
		dashButton = FindObjectOfType<DashController> ();
		attackButton = FindObjectOfType<AttackController> ();

		tagGround = GameObject.Find (this.name + "/tag_ground").transform;
		
	}

	void attack () {
		if ((attackButton.Pressed || Input.GetKeyDown ("f")) && Time.time > nextfire)
		//if (Input.GetKeyDown("f") && Time.time > nextfire) 
		{
			bulletPosition = transform.position;
			nextfire = Time.time + firerate;
			if (isRight) {
				bulletPosition.x = transform.position.x + 1;
				Instantiate (bullet_Right, bulletPosition, Quaternion.identity);

			} else {
				bulletPosition.x = transform.position.x - 1;
				Instantiate (bullet_Left, bulletPosition, Quaternion.identity);

			}
		}
	}
	public void Move (float horizonalInput) {
		if (horizonalInput > 0) {
			isRight = true;
		} else if (horizonalInput < 0) {
			isRight = false;
		}
		Vector2 moveVel = myBody.velocity;
		moveVel.x = horizonalInput * speed;
		myBody.velocity = moveVel;
		
	}

	void movement () {

		if (leftButton.Pressed) {
			if (isRight) Flip ();
			Move (-1);
		} else if (rightButton.Pressed) {
			if (!isRight) Flip ();
			Move (1);
		} else {
			Move (0);
			
		}
	}
	void Flip () {
		isRight = !isRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void jump () {
		if (jumpButton.Pressed || Input.GetKeyDown ("space")) {

			if (isGrounded) {
				myBody.velocity += jumpVelocity * Vector2.up;
			}
		}
	}
	
	void dash () {
		if ((dashButton.Pressed || Input.GetKeyDown ("r")) && Time.time > nextdash) {
			//if (Input.GetKeyDown ("space") && Time.time > nextdash) {

			nextdash = Time.time + dashrate;

			if (isRight) {
				//transform.Translate (5, 0, 0);
				myBody.AddForce (Vector2.right * dashVelocity);
			} else {
				myBody.AddForce (Vector2.left * dashVelocity);
			}

			StartCoroutine (resetVelocity (5.0f));
		}
	}

	private IEnumerator resetVelocity (float waitTime) {
		Debug.Log ("reset velocity");
		yield return new WaitForSeconds (waitTime);
		myBody.velocity = new Vector3 (0, 0, 0);
		dashButton.Pressed = false;
	}

	void Update () {
		//isGrounded = Physics2D.Linecast (myTrans.position, tagGround.position, playerMask);
		isGrounded = Physics2D.OverlapCircle (tagGround.position, checkRadius, playerMask);
		attack ();
		movement ();
		jump ();
		dash ();

	}
}