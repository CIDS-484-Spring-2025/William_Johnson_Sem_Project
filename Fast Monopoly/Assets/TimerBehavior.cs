using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerBehavior : MonoBehaviour
{

    public float time;
    public UnityEvent eventToTrigger;
    public GameObject objectToInstantiate;
    public GameObject objectToEnable;
    
    void Update()
    {

        time -= Time.fixedDeltaTime;

        if(time <= 0){
            eventToTrigger.Invoke();
        }
        
    }

    public void instantiateObject(){
        Instantiate(objectToInstantiate);
    }

    public void enableObject(){
        objectToEnable.SetActive(true);
    }


}
