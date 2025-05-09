using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amountOfPlayersButtons : ButtonBehavior
{

    public static amountOfPlayersButtons currentlyToggledButton;
    public RectTransform wholeRectangle;
    public static bool MouseCursorIsCurrentlyHere;
    public int buttonIndex;
    public GameObject startButton;

    protected override void Start(){

        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {

        if(RectTransformUtility.RectangleContainsScreenPoint(rectangle, Input.mousePosition)){

            image.color = Color.black;

            if(Input.GetButtonDown("Fire1")){
                playButtonSound();
                eventToTrigger.Invoke();
            }

        }else{

            if(currentlyToggledButton != this){
                image.color = Color.grey;
            }

        }
    }

 


    public void select(){
        startButton.SetActive(true);
        currentlyToggledButton = this;
        References.numUsers = buttonIndex;
    }
}
