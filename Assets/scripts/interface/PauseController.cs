using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {
    
    [SerializeField] private GameObject pause_panel;
    [SerializeField] private bool isPause;
    
    public void activatePausePanel(){
        Time.timeScale = 0;
        pause_panel.SetActive(true);
        isPause = true;
    }

    public void deactivatePausePanel(){
        Time.timeScale = 1;
        pause_panel.SetActive(false);
        isPause = false;
    }

}