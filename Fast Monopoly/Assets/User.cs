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
    public int[] items = new int[1]; //increase this as you add more items
    public PlayerBehavior piece;
    public PointerAboveHead pointer;
    public GameObject jailedSound;
    [HideInInspector] public playerInformationDisplayBehavior displayInfo;
    [HideInInspector] public JailedPlayerBehavior jailedPiece;
    [HideInInspector] public bool jailed;
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

    public void goToJail(){
        jailed = true;
        Instantiate(jailedSound);
        displayInfo.jailBarsOverlay.SetActive(true);
        jailedPiece.gameObject.SetActive(true);
        if(jailedPiece.GetComponent<PointerAboveHead>().instantiatedPointer != null){
            jailedPiece.GetComponent<PointerAboveHead>().instantiatedPointer.SetActive(true);
        }

        piece.model.SetActive(false);
        piece.GetComponent<PointerAboveHead>().instantiatedPointer.SetActive(false);
        piece.currentSpace = References.boardSpaces[10];
        piece.navAgent.destination = References.boardSpaces[10].transform.position;
        
        References.canvas.nextTurn();
    }

    public void getOutOfJail(){
        jailed = false;
        displayInfo.jailBarsOverlay.SetActive(false);
        jailedPiece.gameObject.SetActive(false);
        if(jailedPiece.GetComponent<PointerAboveHead>().instantiatedPointer != null){
            jailedPiece.GetComponent<PointerAboveHead>().instantiatedPointer.SetActive(false);
        }

        piece.model.SetActive(true);
        piece.GetComponent<PointerAboveHead>().instantiatedPointer.SetActive(true);
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
