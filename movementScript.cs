using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class movementScript : MonoBehaviour
{
    Rigidbody playerRb;
    Transform playerTf;
    //import components 

    public float movementSpeed;  //velocity
    public float rotationSpeed;  //rotation
    public float driftSpeed = 125f;  //rotation while drift
    public float maxSpeed = 210.0f;
    public float accel = 2.0f;
    public float rMutlipier = 0.2f; //rotation multiplier
    public int food = 0;
    //public float idkWhatToCallIt = 1.5f; //self explanitory

    public KeyCode forwardKey;
    public KeyCode backKey;
    public KeyCode rightKey;
    public KeyCode leftKey;
    public KeyCode driftKey;
    public KeyCode interactKey;

    //public GameObject finishline;
    //public int lapCount = -1;


    public GUI canvas;

    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        playerTf = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame

    //change to fixed Update
    //change to fixed delta Time
    //change inputs
    void Update()
    {
        Vector3 stopVector = new Vector3(0, 0, 0);
        //Vector3 additive = new Vector3(40, 0, 0);
        playerRb.velocity = stopVector;
        Vector3 fowardVector = playerTf.forward;

        //movement
        if (Input.GetKey(forwardKey)) //forward
        {
            if (movementSpeed >= maxSpeed)
            {
                playerRb.AddForce(fowardVector * movementSpeed);
            } else {
                playerRb.AddForce(fowardVector * movementSpeed * accel);
                if(movementSpeed < maxSpeed){
                    movementSpeed += 0.1f;
                }
                
            }
            canvas.setHud(movementSpeed);
            //playerRb.AddForce(fowardVector * (movementSpeed + 40));
        }
        if (Input.GetKey(leftKey)) //move left
        {
            if (Input.GetKey(driftKey))
            {
                float angleD = -driftSpeed * Time.deltaTime;
                playerRb.AddForce(fowardVector * 5.0f);
                playerTf.Rotate(Vector3.up, angleD);
            }
            else
            {
                float angle = -rotationSpeed * Time.deltaTime;
                float angleF = (1.899f -(movementSpeed/maxSpeed))* rMutlipier;
                //playerTf.Rotate(Vector3.up, (1/(movementSpeed * angle)));
                playerTf.Rotate(Vector3.up, -angleF);
            }
        }
        if (Input.GetKey(backKey)) //move back
        {
            Vector3 backwardVector = -1 * playerTf.forward;
            if (movementSpeed >= maxSpeed)
            {
                playerRb.AddForce(backwardVector * movementSpeed);
            }
            else
            {
                playerRb.AddForce(backwardVector * movementSpeed * accel);
                if (movementSpeed < maxSpeed)
                {
                    movementSpeed += 0.1f;
                }

            }
            canvas.setHud(movementSpeed);
        }
        if (Input.GetKey(rightKey)) //move right
        {
            
            if (Input.GetKey(driftKey))
            {
                float angleD = driftSpeed * Time.deltaTime;
                playerRb.AddForce(fowardVector * 5.0f);
                playerTf.Rotate(Vector3.up, angleD);
            } else
            {
                float angle = rotationSpeed * Time.deltaTime;
                float angleF = (1.899f -(movementSpeed/maxSpeed))* rMutlipier;
                playerTf.Rotate(Vector3.up, angleF);
            }
        }
        if (!Input.anyKey) //no key pressed
        {
            if(movementSpeed != 0.0f && movementSpeed > 0.0f)
            {
                movementSpeed = 0.01f;
            }
            canvas.setHud(movementSpeed);
        }
        canvas.setFood(food);
        if(Input.GetKeyDown(interactKey)){
            food++;
        }
    }
    void OnCollisionEnter(Collision collision){
        GameObject other = collision.gameObject;
        string otherTag = other.tag;
        if(other.tag == "foodBank"){
            food = food + 5;
            canvas.setFood(food);
        } else if(other.tag == "objective"){
            food--;
            canvas.setFood(food);
        }
    }
}