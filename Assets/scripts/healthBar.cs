using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBar : MonoBehaviour {
    private Transform bar;
    private Transform green;
    private Transform yellow;
    private Transform red;

    GameObject game;

    void Awake()
    {
        bar = transform.Find("bar");
        green = transform.Find("bar").Find("green");
        yellow = transform.Find("bar").Find("yellow");
        red = transform.Find("bar").Find("red");
        
    }

    public void setSize(float scale){
        bar.localScale = new Vector2 (scale, bar.localScale.y);
        if(scale <=0.2f){
            yellow.gameObject.SetActive(false);
        }else if(scale<=0.5f){
            green.gameObject.SetActive(false);
        }
    }
    
}