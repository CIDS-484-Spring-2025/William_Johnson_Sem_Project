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
    public int[] items;
    public bool[] perks;
    public PlayerBehavior piece;
    public PointerAboveHead pointer;
    public GameObject jailedSound;
    [HideInInspector] public playerInformationDisplayBehavior displayInfo;
    [HideInInspector] public JailedPlayerBehavior jailedPiece;
    [HideInInspector] public bool jailed;
    [HideInInspector] public bool dead = false;
    private void Awake() {
        if (!banker){
            References.users.Add(this);
        }
    }

    private void Start() {
        items = new int[2]; //increase this as you add more items
        perks = new bool[4]; //increase this when you add more perks
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

        if(deathCheck()){
            die();
            int winningPlayerNumber = References.endGameCheck();
            if(winningPlayerNumber != 0){
                References.canvas.displayEndScreen(winningPlayerNumber);
            }
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

        if(!References.canvas.currentUsersTurn.perks[0]){//if user does not have jail perk
            
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

        }
        

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

    private bool deathCheck(){
        return money < 0;
    }

    private void die(){

        References.canvas.communityChestMenu.SetActive(false);
        References.canvas.chanceTimeMenu.SetActive(false);
        References.canvas.purchaseMenu.SetActive(false);

        var properties = propertiesOwned;
        for (int i = properties.Count - 1; i >= 0; i--)
        {
            var property = properties[i];
            property.owner = null;
            property.removeOwnershipHighlight();
            property.paymentForLanding = property.basePaymentForLanding;
            properties.RemoveAt(i);
        }

        dead = true;
        displayInfo.skullAndCrossbonesOverlay.SetActive(true);
        displayInfo.jailBarsOverlay.SetActive(false);
    }

    
}
