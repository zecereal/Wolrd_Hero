using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour,IPointerUpHandler,IPointerDownHandler{

	[HideInInspector]
    public bool Pressed;

    public Image cooldownImg;
    private bool isCooldown;

    private float cooldownTime;

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }    

    public void cooldown(float cooldown)
    {
        cooldownTime = cooldown;
        isCooldown = true;
        cooldownImg.fillAmount = 1;
    }

    void FixedUpdate()
    {
        if (isCooldown)
        {
            cooldownImg.fillAmount -= 1 / cooldownTime * Time.deltaTime;
            if (cooldownImg.fillAmount <= 0)
            {
                isCooldown = false;
            }
        }
    }
}
