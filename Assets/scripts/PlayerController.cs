using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject player;
    public JoypadController joypad;
    public Animator anim;
    

    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //Input.GetAxis("Horizontal") *
        var x = Time.deltaTime * 100.0f * joypad.GetTouchPosition.x;
        var y = Time.deltaTime * 100.0f * joypad.GetTouchPosition.y;
        transform.Translate(x, y, 0);

        if (joypad.GetTouchPosition.x!=0)
        {
            anim.Play("walk_R");
        }
        else
        {
            anim.Play("idle_R");
        }
    }
}
