using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject player;
    public JoypadController joypad;
    public Animator anim;
    public bool isRight;
    

    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
        isRight = true;
	}
	
	// Update is called once per frame
	void Update () {
        //Input.GetAxis("Horizontal") *
        var x = Time.deltaTime * 100.0f * joypad.GetTouchPosition.x;
        var y = Time.deltaTime * 100.0f * joypad.GetTouchPosition.y;
        transform.Translate(x, y, 0);
        if (joypad.GetTouchPosition.x > 0)
        {
            isRight = true;
            anim.Play("walk_R");
        }
        else if (joypad.GetTouchPosition.x < 0)
        {
            isRight = false;
            anim.Play("walk_L");
        }

        if (joypad.GetTouchPosition.x ==0 && joypad.GetTouchPosition.y==0)
        {
            if(isRight)
            {
                anim.Play("idle_R");
            }
            else
            {
                anim.Play("idle_L");
            } 
        }
    }
}
