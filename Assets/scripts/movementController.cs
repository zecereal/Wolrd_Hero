using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementController : MonoBehaviour {
	public animationController anim;

	public LayerMask playerMask;
	public float speed = 10, jumpVelocity = 100f, dashVelocity = 100f, checkRadius = 0.3f;
	Transform myTrans, tagGround;
	Rigidbody2D myBody;
	public bool isGrounded = false;
	public bool isRight = true;

	public string animationState;
	private Vector2 bulletPosition;

	private Transform gun_effect;
	private float firerate = 0.3f;
	private float nextfire = 0.0f;

	private float knockback_time = 0.3f;
	public GameObject bullet_Right;
	public GameObject bullet_Left;

	private float dashrate = 2f;
	private float nextdash = 0.0f;

	private LeftButton leftButton;
	private RightButton rightButton;
	private jumpButtonController jumpButton;
	private DashController dashButton;
	private AttackController attackButton;

	public float currentHp;
	public float maxHp;
	public int attack_power;

	public healthBar health;

	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
		myTrans = this.transform;
		leftButton = FindObjectOfType<LeftButton> ();
		rightButton = FindObjectOfType<RightButton> ();
		jumpButton = FindObjectOfType<jumpButtonController> ();
		dashButton = FindObjectOfType<DashController> ();
		attackButton = FindObjectOfType<AttackController> ();

		anim = GameObject.Find ("Catman").GetComponent<animationController> ();

		tagGround = GameObject.Find (this.name + "/tag_ground").transform;
		gun_effect = GameObject.Find (this.name + "/Catman/GunFireEffect").transform;

		currentHp = maxHp;
	}

	public float getFirerate () {
		return this.firerate;
	}
	void attack () {
		if ((attackButton.Pressed || Input.GetKeyDown ("f")) && Time.time > nextfire)
		//if (Input.GetKeyDown("f") && Time.time > nextfire) 
		{
			bulletPosition = gun_effect.position;
			nextfire = Time.time + firerate;
			if (isRight) {
				Instantiate (bullet_Right, bulletPosition, Quaternion.identity);
			} else {
				Instantiate (bullet_Left, bulletPosition, Quaternion.identity);
			}
			anim.animator.SetBool ("isAttackButtonActive", true);
			StartCoroutine (resetAttack (firerate));
		}
	}
	IEnumerator resetAttack (float waitTime) {
		yield return new WaitForSeconds (waitTime);
		anim.animator.SetBool ("isAttackButtonActive", false);
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
			//anim.changeAnimation ("Idle");

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

	void hurt (int damage) {
		currentHp -= damage;
		float hp_size = currentHp / maxHp;
		//healthBar.setSize (hp_size);
		anim.animator.SetBool ("isHurt", true);
		StartCoroutine (Knockback (knockback_time));
		/* 
		if (isRight) {
				//transform.Translate (5, 0, 0);
				myBody.AddForce (Vector2.left * (dashVelocity/2));
			} else {
				myBody.AddForce (Vector2.right * (dashVelocity/2));
			}
		*/
		StartCoroutine (resetVelocity (5.0f));
	}

	IEnumerator Knockback (float waitTime) {
		yield return new WaitForSeconds (waitTime);
		anim.animator.SetBool ("isHurt", false);
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

	private void OnCollisionEnter2D (Collision2D other) {
		if (other.collider.CompareTag ("Enemy") || other.collider.CompareTag ("Weapon")) {
			int damage = other.gameObject.GetComponent<enemyController> ().getAttackPower ();
			hurt (damage);
		}
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