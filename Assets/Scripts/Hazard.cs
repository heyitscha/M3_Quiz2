using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){ //Automatically called when one collider hits with another
        if(other.gameObject.layer == 8) {
            Player player = other.GetComponent<Player>();
            if(player != null) { //if not null, call Die function
                player.Die();
            }
        }
    }
}