using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    public float velX;
    public float velY;
    public int damage;
    public LayerMask solid;
    private Rigidbody2D rb;
    public float timer;
    private float distance;

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D> ();
        Invoke ("autoDestroy", timer);
    }

    void checkColliosion () {
        RaycastHit2D hitinfo = Physics2D.Raycast (transform.position, transform.up, distance, solid);
        if (hitinfo.collider != null) {
            if (hitinfo.collider.CompareTag ("Enemy")) {
                Debug.Log ("hit");
                hitinfo.collider.GetComponent<EnemyController> ().takeDamage (damage);
                Destroy (gameObject);
            } else if (hitinfo.collider.CompareTag ("Boss")) {
                Debug.Log ("Boss hit");
                hitinfo.collider.GetComponent<BossController> ().takeDamage (damage);
                Destroy (gameObject);
            } else if (hitinfo.collider.CompareTag ("box")) {
                hitinfo.collider.GetComponent<CreteController> ().dropItem ();
                Destroy (gameObject);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        rb.velocity = new Vector2 (velX, velY);
        checkColliosion ();
    }

}