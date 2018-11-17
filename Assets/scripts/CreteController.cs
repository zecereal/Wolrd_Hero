using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreteController : MonoBehaviour {
    
    public GameObject first_aid;
    public GameObject randomItem;

    Vector2 position;

    private int randomNum;
    public void dropItem(){
        //randomNum = Random.Range(0,2);
        position = transform.position;
                
        if(randomNum == 0){
            Instantiate (first_aid,  position, Quaternion.identity);
        }else if(randomNum == 1){
            Instantiate (randomItem , position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
    
}