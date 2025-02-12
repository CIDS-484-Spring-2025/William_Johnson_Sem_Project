using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiviotPointBehavior : MonoBehaviour
{
    [HideInInspector] public Quaternion currentRotation;
    public float speed;

    void Start()
    {
        currentRotation = Quaternion.identity;
    }

    void FixedUpdate()
    {
        Quaternion rotationIncrement = Quaternion.Euler(0, speed, 0); 
        currentRotation *= rotationIncrement;
        transform.rotation = currentRotation;
    }

}
