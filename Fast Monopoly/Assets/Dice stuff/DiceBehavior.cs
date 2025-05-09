using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBehavior : MonoBehaviour
{
    public GameObject[] mySides = new GameObject[6];
    public GameObject diceThrowingSound;
    public GameObject diceRollingSound;
    [HideInInspector] public GameObject topSide;
    [HideInInspector] public bool justSpawned = true;
    public static Dice3SpawnpointBehavior dice3SpawnPoint;

    private void Awake(){
        References.allDice.Add(this);
    }

    public int FindTopSide(){

        //temp values
        float highestSideYPos = -10;

        //for each side of the dice, check if it's y position is greater than the current highest y position
        foreach (GameObject currentDiceSide in mySides)
        {
            
            if(currentDiceSide.transform.position.y >= highestSideYPos){

                highestSideYPos = currentDiceSide.transform.position.y;
                topSide = currentDiceSide;
                
            }

        }

        return topSide.GetComponent<DiceSide>().mySideValue;

    }


    public void Roll(){

        
        //starting sound
        Instantiate(diceThrowingSound, transform);

        ////////////making it jump/////////////////

        //making variables for the dice to move a random amount
        float speedX = Random.Range(-2f, 2f);
        float speedY = Random.Range(7f, 9f);
        float speedZ = Random.Range(-2f, 2f);

        //finding what the current velocity of the dice is (should be 0,0,0)
        Vector3 currentVelocity = GetComponent<Rigidbody>().velocity;

        //assigning the new velocity to the dice
        currentVelocity.x = speedX;
        currentVelocity.y = speedY;
        currentVelocity.z = speedZ;
        GetComponent<Rigidbody>().velocity = currentVelocity;

        ////////////making it rotate/////////////////

        //making variables for the dice to rotate a random amount
        float rotationSpeedX = Random.Range(-360, 360);
        float rotationSpeedY = Random.Range(-360, 360);
        float rotationSpeedZ = Random.Range(-360, 360);

        //finding what the current angular velocity of the dice is (should be 0,0,0)
        Vector3 currentAngularVelocity = GetComponent<Rigidbody>().angularVelocity;

        //assigning the new angular velocity to the dice
        currentAngularVelocity.x = rotationSpeedX;
        currentAngularVelocity.y = rotationSpeedY;
        currentAngularVelocity.z = rotationSpeedZ;
        GetComponent<Rigidbody>().angularVelocity = currentAngularVelocity;


    }

    public bool DiceIsRotating()
    {
        return GetComponent<Rigidbody>().angularVelocity.magnitude > 0.1;
    }

    public bool DiceIsMoving()
    {
        return GetComponent<Rigidbody>().velocity.magnitude > 0.1;
    }

    void OnCollisionEnter(Collision collision)
    {
        //when dice hits table, play sound
        if(collision.gameObject.layer != LayerMask.NameToLayer("Dice") && !justSpawned){
            Instantiate(diceRollingSound, transform);
        }else{
            justSpawned = false;
        }

    }



}
