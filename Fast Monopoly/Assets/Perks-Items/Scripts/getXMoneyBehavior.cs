using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class getXMoneyBehavior : MonoBehaviour
{
    public TextMeshProUGUI text;
    private int amount;

    // Start is called before the first frame update
    void Start()
    {

        do{
            amount = Random.Range(-10, 11) * 50; //random multiple of 50 number from -500 to 500
        }while(amount == 0);

        if(amount < 0){
            text.text = "lose $" + amount;
        }else{
            text.text = "gain $" + amount;
        }
    }

    public void action(){

        References.canvas.currentUsersTurn.loseOrGainMoney(amount);

            References.canvas.communityChestMenu.SetActive(false);
            References.canvas.chanceTimeMenu.SetActive(false);

        if(EffectsBehavior.endTurnAfterAction){
            References.canvas.nextTurn();
        }
            
    }
}
