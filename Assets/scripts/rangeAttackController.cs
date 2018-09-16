using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangeAttackController : MonoBehaviour {

    public GameObject player;
    public GameObject bullet_Right;
    public GameObject bullet_Left;
    public AttackController attackButton;
    public DashController dashButton;
    public Rigidbody2D rb;
    public Animator anim;
    

    private bool isRight;
    private float movementSpeed;
    private float dashSpeed;
    
    private Vector2 bulletPosition;
    private float firerate = 0.3f;
    private float nextfire = 0.0f;
    private float dashrate = 0.5f;
    private float nextdash = 0.0f;



    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        attackButton = FindObjectOfType<AttackController>();
        dashButton = FindObjectOfType<DashController>();
        isRight = true;
    }
    void attack()
    {
        //if (attackButton.Pressed && Time.time > nextfire) 
        if (Input.GetKeyDown("space") && Time.time > nextfire) 
        {
            bulletPosition = transform.position;
            nextfire = Time.time + firerate;
            if (isRight)
            {
                Instantiate(bullet_Right, bulletPosition, Quaternion.identity);
                anim.Play("attack_pistol_R");
            }
            else
            {
                Instantiate(bullet_Left, bulletPosition, Quaternion.identity);
                anim.Play("attack_pistol_L");
            }
        }
    }
    // Update is called once per frame
    void Update () {
        attack();
    }
}

