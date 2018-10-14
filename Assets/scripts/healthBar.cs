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
        
    }

    public void setSize(float scale){
        bar.localScale = new Vector2 (scale, bar.localScale.y);
    }
    
}