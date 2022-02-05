using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackForth : MonoBehaviour
{

    [Header("References")]
    public Transform trans;
    [Header("Stats")]
    [Tooltip("How many units the project will move forward per second.")]
    public  float speed = 2.5f;
    [Header("Direction")]
    [Tooltip("How many units the project will move on the x axis.")]
    public float x_Axis = 2;
    // Start is called before the first frame update

    void Start()
    {
        transform.localPosition = new Vector3(0,0,0);

    }

    // Update is called once per frame
    void Update()
    {
        // Move the projectile back and forth from localPosition to (8,0,0) and so on.
        //Use the same Mathf.PingPong function to the other axes if you need to move in those axes as well
        transform.localPosition = new Vector3(Mathf.PingPong(Time.time * speed, x_Axis), transform.localPosition.y, transform.localPosition.z);
    }
}
