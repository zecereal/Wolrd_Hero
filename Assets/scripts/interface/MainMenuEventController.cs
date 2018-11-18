using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuEventController : MonoBehaviour {

	public GameObject standby_screen;
	public GameObject main_menu;
	// Use this for initialization
	void Start () {
		standby_screen = GameObject.Find("/Canvas/MainMenu");
		standby_screen = GameObject.Find("/Canvas/StandbyScreen");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (LevelLoader.isPlayed)
		{
			standby_screen.SetActive(false);
			main_menu.SetActive(true);
		}
	}
}
