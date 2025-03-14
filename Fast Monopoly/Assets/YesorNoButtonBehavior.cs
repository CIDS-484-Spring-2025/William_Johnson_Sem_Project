using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesorNoButtonBehavior : ButtonBehavior
{
    public Sprite insufficientFundSprite;
    public Sprite bluePurchaseThumbSprite;
    public GameObject declinedSound;
    public GameObject acceptedSound;

    protected override void Update()
    {

        if(RectTransformUtility.RectangleContainsScreenPoint(rectangle, Input.mousePosition)){

            image.color = Color.white;

            if(Input.GetButtonDown("Fire1")){
                References.canvas.hasJustClickedButton = true;
                eventToTrigger.Invoke();
                playButtonSound();
            }

        }else{

            image.color = Color.grey;

        }
        
    }

    public void yesButton(){
        //can buy
        if(References.canvas.currentUsersTurn.money >= References.canvas.currentSpace.price){

            //purchase property
            References.canvas.currentUsersTurn.PurchaseProperty(References.canvas.currentSpace);
            buttonSound = acceptedSound;

            //move on with menu
            References.canvas.purchaseMenu.SetActive(false);
            References.canvas.clickDicePrompt.SetActive(true);

            References.canvas.nextTurn();
            
        }else{ //insufficient funds

            image.sprite = insufficientFundSprite;
            StartCoroutine(ResetSpriteAfterDelay(0.15f));
            buttonSound = declinedSound;

        }
    }

    private IEnumerator ResetSpriteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        image.sprite = bluePurchaseThumbSprite;
    }

    public void noButton(){
        References.canvas.purchaseMenu.SetActive(false);
        References.canvas.clickDicePrompt.SetActive(true);
        
        References.canvas.nextTurn();
    }
}
