using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {

    [SerializeField] private GameObject pause_panel;
    [SerializeField] private GameObject serrender_panel;
    [SerializeField] private bool isPause;

    public void activatePausePanel () {
        pause_panel.SetActive (true);
        isPause = true;
    }

    public void deactivatePausePanel () {
        pause_panel.SetActive (false);
        isPause = false;
    }

    public void activateSurrenderPanel () {
        pause_panel.SetActive (false);
        serrender_panel.SetActive (true);
    }

    public void deactivateSurrenderPanel () {
        serrender_panel.SetActive (false);
        isPause = false;
    }

    public void quitGame () {
        Application.Quit();
    }

    void Update () {
        if (isPause) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

}