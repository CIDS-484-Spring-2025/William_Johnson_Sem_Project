using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesorNoButtonBehavior : ButtonBehavior
{

    // Update is called once per frame
    protected override void Update()
    {

        if(RectTransformUtility.RectangleContainsScreenPoint(rectangle, Input.mousePosition)){

            image.color = Color.white;

            if(Input.GetButtonDown("Fire1")){
                References.canvas.hasJustClickedButton = true;
                playButtonSound();
                eventToTrigger.Invoke();
            }

        }else{

            image.color = Color.grey;

        }
        
    }

    public void noButton(){
        References.canvas.purchaseMenu.SetActive(false);
        References.canvas.clickDicePrompt.SetActive(true);
    }
}
