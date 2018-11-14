using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JumpButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    [HideInInspector]
    public bool Pressed;
    public Image cooldownImg;
    public bool isJump;

    public void OnPointerDown (PointerEventData eventData) {
        Pressed = true;
    }

    public void OnPointerUp (PointerEventData eventData) {
        Pressed = false;
    }
    
    void FixedUpdate () {
        if (isJump) {
            cooldownImg.fillAmount = 1;
        } else {
            cooldownImg.fillAmount = 0;
        }
    }
}