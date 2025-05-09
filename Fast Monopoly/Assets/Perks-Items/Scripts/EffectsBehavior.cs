using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsBehavior : MonoBehaviour
{
    public static int skipNextPlayer = 0;
    public static bool lose500AtStartOfEveryTurn = false;
    public static bool duckigeton = false;
    public static bool endTurnAfterAction = true;
    public void skipNextPlayersTurn(){

        if(skipNextPlayer == 0){
            skipNextPlayer++;
        }
        skipNextPlayer++;

        endSelection(false);  
    }

    public void doublePropertyCost(){

        foreach (BordSpaceBehavior property in References.canvas.currentUsersTurn.propertiesOwned)
            {
                property.multiplyLandingCost(2f);
            }

        endSelection(false);    
    }

    public void obtainARandomUnownedProperty(){
        List<BordSpaceBehavior> allUnownedProperties = new List<BordSpaceBehavior>();

        for (int i = 1; i < References.boardSpaces.Length; i++)
        {
            Debug.Log(i);
            if(References.boardSpaces[i].owner == null){
                if(References.boardSpaces[i].isPurchasable()){
                    allUnownedProperties.Add(References.boardSpaces[i]);
                }
                
            }
        }

        if(allUnownedProperties.Count != 0){
            int randomNumber = Random.Range(0, allUnownedProperties.Count);

            allUnownedProperties[randomNumber].owner = References.canvas.currentUsersTurn;
            References.canvas.currentUsersTurn.propertiesOwned.Add(allUnownedProperties[randomNumber]);
            allUnownedProperties[randomNumber].activateOwnershipHighlight(References.canvas.currentUsersTurn.playerNumber);
        }

        endSelection(false);
    }

    public void randomlyDivideAllUnownedProperties(){

        List<BordSpaceBehavior> allUnownedProperties = new List<BordSpaceBehavior>();

        for (int i = 1; i < References.boardSpaces.Length; i++)
        {
            if(References.boardSpaces[i].owner == null){
                if(References.boardSpaces[i].isPurchasable()){
                    allUnownedProperties.Add(References.boardSpaces[i]);
                }
                
            }
        }

        foreach (BordSpaceBehavior property in allUnownedProperties){
            int randomNumber;

            do{
                randomNumber = Random.Range(0, References.users.Count);
            }while(References.users[randomNumber].dead);

            property.owner = References.users[randomNumber];
            References.users[randomNumber].propertiesOwned.Add(property);
            property.activateOwnershipHighlight(References.users[randomNumber].playerNumber);
        }

        endSelection(true);
    }

    public void cursed(){
        for (int i = 0; i < References.canvas.currentUsersTurn.items.Length; i++)
        {
            References.canvas.currentUsersTurn.items[i] = 0;
        }
        for (int i = 0; i < References.canvas.currentUsersTurn.perks.Length; i++)
        {
            References.canvas.currentUsersTurn.perks[i] = false;
        }

        endSelection(false);
    }

    public void loseAllOfYourProperties(){
        var properties = References.canvas.currentUsersTurn.propertiesOwned;
            for (int i = properties.Count - 1; i >= 0; i--)
            {
                var property = properties[i];
                property.owner = null;
                property.removeOwnershipHighlight();
                property.paymentForLanding = property.basePaymentForLanding;
                properties.RemoveAt(i);
            }

        endSelection(false);
    }

    public void rollWith3DiceEvent(){

        GameObject dicePrefab = GetComponent<RollWith3Dice>().DicePrefab;

        Instantiate(dicePrefab, DiceBehavior.dice3SpawnPoint.transform);

        endSelection(true);
    }

    public void duckEvent(){
        duckigeton = true;

        endSelection(true);
    }

    public void loss500AtStartOfEveryTurnEvent(){
        lose500AtStartOfEveryTurn = true;

        endSelection(true);
    }

    public void onlyMoveClockwiseEvent(){
        References.canvas.fullCounterClockwiseOption.SetActive(false);

        endSelection(true);
    }

    public void sendAllPlayersToJail(){
        
        foreach (User user in References.users)
        {
            if(!user.dead){
                user.goToJail();
            }
        }

        endSelection(false);
    }

    public void goToJailEffect(){
        References.canvas.currentUsersTurn.goToJail();

        endSelection(false);
    }

    public void twoRandomEffects(){
        Debug.Log(1);
        endTurnAfterAction = false;
        Debug.Log(2);
        References.canvas.twoMoreEffectsMenu.gameObject.SetActive(true);
        Debug.Log(3);
        endSelection(false);
    }

    private void endSelection(bool removeCardFromDeck){

        if(removeCardFromDeck){

            CommunityChestManager.removeCard(this.GetComponent<PerkOrItemButtonBehavior>());

        }
        Debug.Log(4);
            References.canvas.communityChestMenu.SetActive(false);
            References.canvas.chanceTimeMenu.SetActive(false);
        Debug.Log(5);
        if(endTurnAfterAction){
            References.canvas.nextTurn();
        }
        Debug.Log(6);

    }


}
