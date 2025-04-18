using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class getXMoneyBehavior : MonoBehaviour
{
    public TextMeshProUGUI text;
    private  int amount;

    // Start is called before the first frame update
    void Start()
    {
        amount = Random.Range(-5, 11) * 50; //random multiple of 50 number from -250 to 500
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

        References.canvas.nextTurn();
            
    }
}
