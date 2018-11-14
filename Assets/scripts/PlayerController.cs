using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public AnimationController anim;

    public EventController eventsystem;
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

    private Vector2 bulletPosition;

    private Transform gun_effect;
    private float firerate = 0.3f;
    private float nextfire = 0.0f;

    private float knockback_time = 0.3f;
    public GameObject bullet_Right;
    public GameObject bullet_Left;

    private float nextdash = 0.0f;

    private float dashCooldown = 5f;
    private float skillCooldown = 5f;
    private float nextskill = 0.0f;
    private int bulletStack;
    private LeftButton leftButton;
    private RightButton rightButton;
    private JumpButton jumpButton;
    private DashButton dashButton;
    private AttackButton attackButton;

    private SkillButton skillButton;

    public float currentHp;
    public float maxHp;

    public float current_mana;
    public float maxMana;

    public Slider hp_bar;
    public Slider mana_bar;
    public int attack_power;

    void Start () {
        myBody = this.GetComponent<Rigidbody2D> ();
        myTrans = this.transform;
        leftButton = FindObjectOfType<LeftButton> ();
        rightButton = FindObjectOfType<RightButton> ();
        jumpButton = FindObjectOfType<JumpButton> ();
        dashButton = FindObjectOfType<DashButton> ();
        attackButton = FindObjectOfType<AttackButton> ();
        skillButton = FindObjectOfType<SkillButton> ();

        eventsystem = FindObjectOfType<EventController> ();

        anim = GameObject.Find ("Catman").GetComponent<AnimationController> ();

        tagGround = GameObject.Find (this.name + "/tag_ground").transform;
        gun_effect = GameObject.Find (this.name + "/Catman/GunFireEffect").transform;

        currentHp = maxHp;
        current_mana = maxMana;

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
        if ((skillButton.Pressed || Input.GetKeyDown ("q") )&& Time.time > nextskill) {
            consumeMana();
            bulletPosition = gun_effect.position;
            nextskill = Time.time + skillCooldown;
            bulletStack = 5;
            anim.animator.SetBool ("isSkillButtonActive", true);
            skillButton.GetComponent<Button>().interactable = false;
            skillButton.cooldown(skillCooldown);
            StartCoroutine (setSkillSequence (0.2f, 0.1f));
            StartCoroutine (resetSkill (skillCooldown));
        }
    }

    void consumeMana () {
        if (current_mana >= 20) {
            current_mana -= 20;
            setManaBarSize ();
        }
    }
    void setManaBarSize () {
        float mana_size = current_mana / maxMana;
        mana_bar.value = mana_size;
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
        skillButton.GetComponent<Button>().interactable = false;
        //skillButton.Pressed = false;
    }

    IEnumerator setSkillSequence (float delay, float nextBulletTime) {
        yield return new WaitForSeconds (delay);
        StartCoroutine (nextBullet (nextBulletTime));
    }
    public void Move (float horizonalInput) {

        if (horizonalInput != 0 && !isJump) {
            anim.animator.SetBool ("isWalkButtonActive", true);
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
            jumpButton.isJump = false;
            anim.animator.SetBool ("isJumpButtonActive", false);
            if (jumpButton.Pressed || Input.GetKeyDown ("space")) {
                isJump = true;
                anim.animator.SetBool ("isJumpButtonActive", true);
                myBody.velocity += jumpVelocity * Vector2.up;
            }
        } else {
            if (isWalk) isWalk = false;
            jumpButton.isJump = true;
        }

    }

    void hurt (int damage) {
        currentHp -= damage;
        setHPBarSize ();
        //healthBar.setSize (hp_size);
        anim.animator.SetBool ("isHurt", true);
        StartCoroutine (Knockback (knockback_time));
        StartCoroutine (resetVelocity (5.0f));
    }

    void setHPBarSize () {
        float hp_size = currentHp / maxHp;
        hp_bar.value = hp_size;
    }
    IEnumerator Knockback (float waitTime) {
        yield return new WaitForSeconds (waitTime);
        anim.animator.SetBool ("isHurt", false);
    }

    void dash () {
        if ((dashButton.Pressed || Input.GetKeyDown ("r")) && Time.time > nextdash) {
            nextdash = Time.time + dashCooldown;

            if (isRight) {
                myBody.AddForce (Vector2.right * dashVelocity);
            } else {
                myBody.AddForce (Vector2.left * dashVelocity);
            }
            anim.animator.SetBool ("isDashButtonActive", true);

            dashButton.GetComponent<Button>().interactable = false;
            dashButton.cooldown(dashCooldown);
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
        dashButton.GetComponent<Button>().interactable = true;
        
        
    }

    void dead () {
        if (currentHp <= 0) {
            anim.animator.SetBool ("isHpLessThanZero", true);
            StartCoroutine (destroyHero (1.2f));
        }
    }

    IEnumerator destroyHero (float waitTime) {

        yield return new WaitForSeconds (waitTime);
        Destroy (gameObject);
    }

    void regenerateHP (float percent) {

        float regen = (percent / 100f) * maxHp;
        currentHp += regen;
        if (currentHp >= maxHp) currentHp = maxHp;
        setHPBarSize ();

    }
    void FixedUpdate () {
        isGrounded = Physics2D.Linecast (myTrans.position, tagGround.position, playerMask);
        attack ();
        movement ();
        jump ();
        dash ();
        skill ();
        dead ();

        //Physics2D.IgnoreLayerCollision (8,11);
    }

    private void OnCollisionEnter2D (Collision2D other) {

        if (other.collider.CompareTag ("Enemy")) {
            int damage = other.gameObject.GetComponent<EnemyController> ().getAttackPower ();
            hurt (damage);
        } else if (other.collider.CompareTag ("Boss")) {
            int bossDamage = other.gameObject.GetComponent<BossController> ().getAttackPower ();
            hurt (bossDamage);
        } else if (other.collider.CompareTag ("first_aid")) {
            regenerateHP (20f);
            Destroy (other.collider.gameObject);
        } else if (other.collider.CompareTag ("item")) {
            Destroy (other.collider.gameObject);
        }

    }

    private void OnCollisionStay2D (Collision2D other) {
        if (other.collider.CompareTag ("Blocking_area")) {
            eventsystem.blockingAreaActive ();
            Debug.Log ("ignore is not affect");
        }

    }

}