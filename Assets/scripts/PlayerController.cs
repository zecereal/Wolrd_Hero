using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
    public GameObject player;
    public JoypadController joypad;
    public GameObject button;
    public Animator anim;
    public bool isRight;

    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
        isRight = true;
	}
	
    void attack()
    {
        if (CrossPlatformInputManager.GetButtonDown("attack"))
        {
            if (isRight)
            {
                anim.Play("attack_pistol_R");
            }
            else
            {
                anim.Play("attack_pistol_L");
            }
        }
    }

    void movement()
    {
        var positionX = Time.deltaTime * 100.0f * joypad.GetTouchPosition.x;
        var positionY = Time.deltaTime * 100.0f * joypad.GetTouchPosition.y;
        transform.Translate(positionX, positionY, 0);
        flip();
    }

    private void flip()
    {
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

        if (joypad.GetTouchPosition.x == 0 && joypad.GetTouchPosition.y == 0)
        {
            if (isRight)
            {
                anim.Play("idle_R");
            }
            else
            {
                anim.Play("idle_L");
            }
        }
    }

    // Update is called once per frame
    void Update () {
        movement();
        attack();
    }
}
