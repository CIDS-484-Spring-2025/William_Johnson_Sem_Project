using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class User : MonoBehaviour
{
    public int playerNumber;
    public int money;
    public bool banker;
    public List<BordSpaceBehavior> propertiesOwned = new List<BordSpaceBehavior>();    
    public PlayerBehavior piece;
    [HideInInspector] public playerInformationDisplayBehavior displayInfo;
    private void Awake() {
        if (!banker){
            References.users.Add(this);
        }
    }

    public void UpdateMoneyText(){
            displayInfo.moneyAmountText.text = money.ToString();
    }

    public void loseOrGainMoney(int moneyToExchange){
        money += moneyToExchange;
        if(!banker){
            UpdateMoneyText();
            producePopupProfitText(moneyToExchange);
        }

    }

    public void PurchaseProperty(BordSpaceBehavior property){
        //subtract money
        money -= property.price;
        UpdateMoneyText();
        producePopupProfitText(-property.price);

        //set property ownerships
        property.owner = this;
        propertiesOwned.Add(property);
        property.activateOwnershipHighlight(playerNumber);
    }

    public void producePopupProfitText(int amount){
        PopupProfitTextBehavior PopupText = Instantiate(displayInfo.PopuptextPrefab, displayInfo.popUpProfitPartent.transform);
        if(amount < 0){
            PopupText.text.color = Color.red;
            PopupText.text.text = amount.ToString();
            StartCoroutine(RemoveBubbleAfterDelay(4.0f, PopupText.gameObject));
            return;
        }
        PopupText.text.text = "+" + amount.ToString();
        StartCoroutine(RemoveBubbleAfterDelay(4.0f, PopupText.gameObject));
    }

    private IEnumerator RemoveBubbleAfterDelay(float delay, GameObject PopupText)
    {
        yield return new WaitForSeconds(delay);
        Destroy(PopupText);
    }

    
}
