using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    //**References**//
    [Header("References")]
    public Transform trans;
    public Transform modelTrans;
    public CharacterController characterController;

    //**Movement**//
    [Header("Movement")]
    [Tooltip("Units moved per second at maximum speed.")]
    public float movespeed = 24;

    [Tooltip("Time in seconds to reach maximum speed.")]
    public float timeToMaxSpeed = .26f;

    //**Death and Respawning**//
    [Header("Death and Respawning")]
    [Tooltip("Respawn Duration")]
    public float respawnWaitTime = 2f; // Speed of respawn time

    private bool dead = false;
    private Vector3 spawnPoint;
    private Quaternion spawnRotation; // Record the rotation of the player; current rotation spawned in

    private float velocityGainPerSecond // invisible in the component; cannot be changed or manipulated //Properties of common math
    {
        get
        {
            return movespeed / timeToMaxSpeed;
        }
    }

    [Tooltip("Time in seconds to go from maximum speed to stationary")]
    public float timeToLoseMaxSpeed = .2f;

    private float velocityLossPerSecond
    {
        get
        {
            return movespeed / timeToLoseMaxSpeed;
        }
    }

    [Tooltip("Multiplier for momentum when attempting to move in a direction opposite the current traveling direction")]
    public float reverseMomentumMultiplier = 2.2f;

    private Vector3 movementVelocity = Vector3.zero;

    public void Die()
    {
        if (!dead) 
        {
            dead = true;
            enabled = false; //once it respawns back // instance variable; disables the script itself
            characterController.enabled = false; // Deactivate the player's CharacterController
            modelTrans.gameObject.SetActive(false);
        
            SceneManager.LoadScene("EndScene"); // Calls End Scene for Game OVer

        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = trans.position; // Set the local transform position of the Player
        spawnRotation = modelTrans.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
    }

    void Movement()
    {
        //If W or Up arrow key is held
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
        {
            if(movementVelocity.z  >= 0)
            {
                //Increase z velocity by  VelocityGainPerSecond, but don't go higher
                movementVelocity.z = Mathf.Min(movespeed, movementVelocity.z + velocityGainPerSecond * Time.deltaTime);
            }
            else // Else if we're moving back
            {
                movementVelocity.z = Mathf.Min(0, movementVelocity.z + velocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
            }
        }
        //If S or the down arrow key is held
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if(movementVelocity.z > 0) // If already moving forward
            {
                movementVelocity.z = Mathf.Max(0, movementVelocity.z - velocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
            }
            else //If we're moving back or not moving at all
            {
                movementVelocity.z = Mathf.Max(-movespeed, movementVelocity.z - velocityGainPerSecond * Time.deltaTime);
            } 
        } 
        else //If neither forward or back are being held
        {
            //We must bring the Z velocity back to 0 over time
            if (movementVelocity.z > 0) //If we're moving up, decrease Z velocity by VelocityLossPerSecond, but don't go any lower than 0
            {
                movementVelocity.z = Mathf.Max(0, movementVelocity.z - velocityLossPerSecond * Time.deltaTime);
            }
            else //If we're moving down, increase the Z velocity back towards 0 by VelocityLossPerSecond, but don't go any lower than 0
            {
                movementVelocity.z = Mathf.Min(0, movementVelocity.z + velocityLossPerSecond * Time.deltaTime);
            }
        }
       
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
        {
            if(movementVelocity.x  >= 0)
            {
              
                movementVelocity.x = Mathf.Min(movespeed, movementVelocity.x + velocityGainPerSecond * Time.deltaTime);
            }
            else
            {
                movementVelocity.x = Mathf.Min(0, movementVelocity.x + velocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
            }
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // the || indicates OR
        {
            if(movementVelocity.x > 0) 
            {
                movementVelocity.x = Mathf.Max(0, movementVelocity.x - velocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
            }
            else 
            {
                movementVelocity.x = Mathf.Max(-movespeed, movementVelocity.x - velocityGainPerSecond * Time.deltaTime);
            } 
        } 
        else //If neither forward or back are being held
        {
            //We must bring the Z velocity back to 0 over time
            if (movementVelocity.x > 0) //If we're moving up, decrease Z velocity by VelocityLossPerSecond, but don't go any lower than 0
            {
                movementVelocity.x = Mathf.Max(0, movementVelocity.x - velocityLossPerSecond * Time.deltaTime);
            }
            else //If we're moving down, increase the Z velocity back towards 0 by VelocityLossPerSecond, but don't go any lower than 0
            {
                movementVelocity.x = Mathf.Min(0, movementVelocity.x + velocityLossPerSecond * Time.deltaTime);
            }

        }

        


        if (movementVelocity.x != 0 || movementVelocity.z != 0) // detecting the x and z movements
        {
            //Applying the movement velocity
            characterController.Move(movementVelocity * Time.deltaTime);
            
            modelTrans.rotation = Quaternion.Slerp(modelTrans.rotation, Quaternion.LookRotation(movementVelocity), .18f); //rotating the model holder
            // Single instance of Quaternion represents a rotation which is pretty much a direction where a point towards
            // Slerp - Helps in making turn smoother. Deals with the vectors (Short for: Spherical, liniear, enterpolation)
            // 1st - initial (modelTrans)
            //2nd - Quaternion - rotation where we want to end
            // Quaternion.LookRotation(movementVelocity) - how fast you want to get there
        }
    }

   
}
