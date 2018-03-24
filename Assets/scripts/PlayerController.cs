using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject player;
    public JoypadController joypad;
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Input.GetAxis("Horizontal") *
        var x = Time.deltaTime * 100.0f * joypad.GetTouchPosition.x;
        var y = Time.deltaTime * 100.0f * joypad.GetTouchPosition.y;
        transform.Translate(x, y, 0);
    }
}
