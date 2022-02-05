using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTraps : MonoBehaviour
{
    [Header("References")]
    public Transform trans;
    
    [Header("Stats")]
    [Tooltip("How many units the project will move forward per second.")]
    public  float speed = 2.5f;

    [Header("Direction")]
    [Tooltip("How many units the project will move on the x axis.")]
    public float y_Axis = 2;
    
    // Start is called before the first frame updaste
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Move the projectile back and forth from (0,0,0) to (8,0,0) and so on.
        //Use the same Mathf.PingPong function to the other axes if you need to move in those axes as well
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * speed, y_Axis), transform.position.z);
    }
}
