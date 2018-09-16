using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    public float velX;
    public float velY;
    private Rigidbody2D rb;
    private float timer;
    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D> ();

    }

    private void OnTriggerEnter2D (Collider2D collision) {
        Destroy (gameObject, 0f);
    }

    // Update is called once per frame
    void Update () {
        rb.velocity = new Vector2 (velX, velY);
        timer += 1.0F * Time.deltaTime;
        if (timer >= 1) {
            GameObject.Destroy (gameObject);
        }
    }
}