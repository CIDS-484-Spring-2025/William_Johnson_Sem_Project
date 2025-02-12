using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareBehavior : MonoBehaviour
{

    public float shakeAmount;
    public float maxMoveSpeed;
    public AudioSource jumpScareSound;
    Vector2 startingPos;
    Vector2 desiredPosition;

    void Start()
    {
        startingPos = transform.position;
    }

    void Update()
    {

        Vector2 shakeVector =  new Vector2(GetRandomShakeAmount(), GetRandomShakeAmount());
        desiredPosition = startingPos + shakeVector;

        
        transform.position = Vector2.MoveTowards(transform.position,  desiredPosition, maxMoveSpeed * Time.deltaTime);

        if(!jumpScareSound.isPlaying){
            Application.Quit();
        }
    }

    private float GetRandomShakeAmount(){
        return Random.Range(-shakeAmount, shakeAmount);
    }
}
