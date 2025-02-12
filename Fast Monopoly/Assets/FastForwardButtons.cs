using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastForwardButtons : ButtonBehavior
{

    public static FastForwardButtons currentlyToggledButton;
    public bool startingSpeedButton;
    public RectTransform wholeRectangle;
    public static bool MouseCursorIsCurrentlyHere;

    protected override void Start(){

        base.Start();
        if(startingSpeedButton){
            currentlyToggledButton = this;
        }

    }

    // Update is called once per frame
    protected override void Update()
    {

        if(RectTransformUtility.RectangleContainsScreenPoint(rectangle, Input.mousePosition)){

            image.color = Color.white;

            if(Input.GetButtonDown("Fire1")){
                currentlyToggledButton = this;
                playButtonSound();
                eventToTrigger.Invoke();
            }

        }else{

            if(currentlyToggledButton != this){
                image.color = Color.grey;
            }

        }


        //make sure you can't roll dice while selecting a speed
        mouseCursonIsCurrentlyHereCheck();
        
    }

    public void mouseCursonIsCurrentlyHereCheck(){

        //It will only check the one button with wholeRectangle Set
        if(wholeRectangle == null){
            return;
        }

        if(RectTransformUtility.RectangleContainsScreenPoint(wholeRectangle, Input.mousePosition)){

            MouseCursorIsCurrentlyHere = true;

        }else{

            MouseCursorIsCurrentlyHere = false;

        }
        
    }


    public void x1Speed(){
        Time.timeScale = 1;
    }

    public void x2Speed(){
        Time.timeScale = 2;
    }

    public void x4Speed(){
        Time.timeScale = 4;
    }
}
