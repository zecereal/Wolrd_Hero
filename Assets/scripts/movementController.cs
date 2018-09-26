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

	public bool isWalk = false;
	public bool isJump = false;
	public bool isAttacking = false;
	public bool isUseSkill = false;

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

	private float dashCooldown = 5f;
	private float skillCooldown = 5f;
	private float nextskill = 0.0f;
	private int bulletStack;
	private LeftButton leftButton;
	private RightButton rightButton;
	private jumpButtonController jumpButton;
	private DashController dashButton;
	private AttackController attackButton;

	private SkillButtonController skillButton;

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
		skillButton = FindObjectOfType<SkillButtonController> ();

		anim = GameObject.Find ("Catman").GetComponent<animationController> ();

		tagGround = GameObject.Find (this.name + "/tag_ground").transform;
		gun_effect = GameObject.Find (this.name + "/Catman/GunFireEffect").transform;

		currentHp = maxHp;
	}

	public float getFirerate () {
		return this.firerate;
	}
	void attack () {
		if ((attackButton.Pressed || Input.GetKeyDown ("f")) && Time.time > nextfire) {
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

	void skill () {
		if (skillButton.Pressed || Input.GetKeyDown ("q") && Time.time > nextskill) {
			bulletPosition = gun_effect.position;
			nextskill = Time.time + skillCooldown;
			bulletStack = 5;
			anim.animator.SetBool ("isSkillButtonActive", true);
			StartCoroutine (setSkillSequence (0.2f, 0.1f));
			StartCoroutine (resetSkill (skillCooldown));
		}
	}
	IEnumerator nextBullet (float waitTime) {
		yield return new WaitForSeconds (waitTime);
		bulletPosition = gun_effect.position;
		if (bulletStack > 0) {
			if (isRight) {
				Instantiate (bullet_Right, bulletPosition, Quaternion.identity);
			} else {
				Instantiate (bullet_Left, bulletPosition, Quaternion.identity);
			}
			bulletStack--;
			StartCoroutine (nextBullet (waitTime));
		} else {
			anim.animator.SetBool ("isSkillButtonActive", false);
		}
	}
	IEnumerator resetSkill (float waitTime) {
		yield return new WaitForSeconds (waitTime);
		skillButton.Pressed = false;
	}

	IEnumerator setSkillSequence (float delay, float nextBulletTime) {
		yield return new WaitForSeconds (delay);
		StartCoroutine (nextBullet (nextBulletTime));
	}
	public void Move (float horizonalInput) {
		if (!isGrounded) {
			anim.animator.SetBool ("isWalkButtonActive", false);
		} else {
			anim.animator.SetBool ("isWalkButtonActive", true);
		}

		if (horizonalInput > 0) {
			isRight = true;
		} else if (horizonalInput < 0) {
			isRight = false;
		} else {
			anim.animator.SetBool ("isWalkButtonActive", false);
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
			anim.animator.SetBool ("isWalkButtonActive", true);
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
		isGrounded = Physics2D.OverlapCircle (tagGround.position, checkRadius, playerMask);

		if (isGrounded) {
			isJump = false;
			anim.animator.SetBool ("isJumpButtonActive", false);
			if (jumpButton.Pressed || Input.GetKeyDown ("space")) {
				isJump = true;
				anim.animator.SetBool ("isJumpButtonActive", true);
				myBody.velocity += jumpVelocity * Vector2.up;
			}
		} else {
			if (isWalk) isWalk = false;
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
			nextdash = Time.time + dashrate;

			if (isRight) {
				myBody.AddForce (Vector2.right * dashVelocity);
			} else {
				myBody.AddForce (Vector2.left * dashVelocity);
			}
			anim.animator.SetBool ("isDashButtonActive", true);
			StartCoroutine (resetDashAnimation (0.5f));
			StartCoroutine (resetVelocity (dashCooldown));
		}
	}

	IEnumerator resetDashAnimation (float waitTime) {
		yield return new WaitForSeconds (waitTime);
		anim.animator.SetBool ("isDashButtonActive", false);
	}
	private IEnumerator resetVelocity (float waitTime) {
		yield return new WaitForSeconds (waitTime);
		myBody.velocity = new Vector3 (0, 0, 0);
		dashButton.Pressed = false;
	}

	void Update () {
		//isGrounded = Physics2D.Linecast (myTrans.position, tagGround.position, playerMask);

		attack ();
		movement ();
		jump ();
		dash ();
		skill ();
	}

	private void OnCollisionEnter2D (Collision2D other) {
		if (other.collider.CompareTag ("Enemy") || other.collider.CompareTag ("Weapon")) {
			int damage = other.gameObject.GetComponent<enemyController> ().getAttackPower ();
			hurt (damage);
		}
	}

}