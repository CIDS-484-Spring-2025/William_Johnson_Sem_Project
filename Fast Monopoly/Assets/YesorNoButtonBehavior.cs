using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
            References.canvas.nextTurn();
            
        }else{//insufficient funds

            image.sprite = insufficientFundSprite;
            StartCoroutine(ResetSpriteAfterDelay(0.17f));
            buttonSound = declinedSound;

        }
    }

    private IEnumerator ResetSpriteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        image.sprite = bluePurchaseThumbSprite;
    }

    public void getOutOfJailPayment(){
        //can buy
        if(References.canvas.currentUsersTurn.money >= 300){

            References.canvas.currentUsersTurn.loseOrGainMoney(-300);
            References.canvas.jailedMenu.SetActive(false);
            References.canvas.currentUsersTurn.getOutOfJail();
            References.canvas.clickDicePrompt.SetActive(true);
            
        }else{//insufficient funds

            image.sprite = insufficientFundSprite;
            StartCoroutine(ResetSpriteAfterDelay(0.17f));
            buttonSound = declinedSound;

        }
    }

    public void noButton(){
        References.canvas.purchaseMenu.SetActive(false);
        References.canvas.rerollDiceMenu.SetActive(false);
        
        References.canvas.nextTurn();
    }

    public void jailedNoButton(){

        References.canvas.currentUsersTurn.loseOrGainMoney(-100);
        References.canvas.jailedMenu.SetActive(false);
        References.canvas.clickDicePrompt.SetActive(true);

    }

    public void rerollDice(){
        References.canvas.rerollDiceMenu.SetActive(false);
        References.canvas.clickDicePrompt.SetActive(true);
        References.canvas.currentUsersTurn.items[0]--;
    }
}
