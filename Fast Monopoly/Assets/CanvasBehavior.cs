using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasBehavior : MonoBehaviour
{

    public GameObject newPlayersTurnMenu;
    public TextMeshProUGUI newPlayersTurnText;
    public GameObject clickDicePrompt;
    public GameObject selectionMenu;
    public GameObject purchaseMenu;
    public GameObject communityChestMenu;
    public GameObject chanceTimeMenu;
    public GameObject jailedMenu;
    public GameObject rerollDiceMenu;
    public GameObject justSayNoMenu;
    public TwoMoreEffectsMenu twoMoreEffectsMenu;
    [HideInInspector] public bool noToRerollDice;
    public GameObject endScreenMenu;
    public TextMeshProUGUI diceRollText;
    public TextMeshProUGUI moveToCCText;
    public TextMeshProUGUI moveToCText;
    public TextMeshProUGUI endScreenText;
    public TextMeshProUGUI turnTrackerText;
    public TextMeshProUGUI BestTurnTrackerText;
    public CorCCButtonBehavior counterClockwiseButton;
    public GameObject fullCounterClockwiseOption;
    public CorCCButtonBehavior clockwiseButton;
    public Transform pointersChild;
    public TextMeshProUGUI PurchaseMenuHeaderText;
    public GameObject UserDisplayInfo;
    public playerInformationDisplayBehavior playerInfoPrefab;
    [HideInInspector] public User currentUsersTurn;
    [HideInInspector] public BordSpaceBehavior counterClockwiseSpace;
    [HideInInspector] public BordSpaceBehavior clockwiseSpace;
    [HideInInspector] public BordSpaceBehavior currentSpace;
    [HideInInspector] public bool waitingForPiecesToBeInPosition = false;
    [HideInInspector] public bool hasJustClickedButton = false;
    [HideInInspector] public int turnCount;


    void Awake(){
        References.canvas = this;
    }

    private void Start() {

        StartCoroutine(setUpUserDisplays());

        
    }

    IEnumerator setUpUserDisplays()
    {
        yield return null; //wait a frame

        turnCount = 1;

        //Create display info
        foreach (User user in References.users)
        {
            playerInformationDisplayBehavior newPlayerInfo = Instantiate(playerInfoPrefab, UserDisplayInfo.transform);
            user.displayInfo = newPlayerInfo;
            newPlayerInfo.playerTagText.text = "Player " + user.playerNumber;
            newPlayerInfo.name = "Player " + user.playerNumber + " Info"; 
            user.UpdateMoneyText();
        }
        currentUsersTurn = References.users[0];
        currentUsersTurn.displayInfo.currentTurnTracker.SetActive(true);
    }

    

    // Update is called once per frame
    void Update()
    {

        //if the click dice prompt is active and the cursor in a valid location, roll the dice when clicked
        if(Input.GetButtonDown("Fire1") && clickDicePrompt.activeSelf && !FastForwardButtons.MouseCursorIsCurrentlyHere){

            foreach (DiceBehavior die in References.allDice)
            {
                die.Roll();
                
            }
            clickDicePrompt.SetActive(false);
        }

        //if the click dice prompt is not active, wait until dice are finished rolling
        if( !clickDicePrompt.activeSelf && !waitingForPiecesToBeInPosition && !purchaseMenu.activeSelf && 
            !communityChestMenu.activeSelf && !chanceTimeMenu.activeSelf && !jailedMenu.activeSelf &&
            !newPlayersTurnMenu.activeSelf && !endScreenMenu.activeSelf  && !justSayNoMenu.activeSelf &&
            !twoMoreEffectsMenu.gameObject.activeSelf){

            foreach (DiceBehavior die in References.allDice)
            {
                //if any die is moving or rotating, get out of this loop
                if(die.DiceIsMoving() || die.DiceIsRotating()){
                    break;
                }

                //if this is the last die in the loop, then dice have finished rolling
                if(die == References.allDice[References.allDice.Count - 1]){

                    if(currentUsersTurn.items[0] > 0 && !noToRerollDice && !selectionMenu.activeSelf){ //if User has reroll dice item
                        rerollDiceMenu.SetActive(true);
                    }else{
                        if(!currentUsersTurn.jailed || References.rolledDoubledCheck()){
                            currentUsersTurn.getOutOfJail();
                            displaySelectionMenu();
                            noToRerollDice = false;
                        }else{//if jailed
                            nextTurn();
                        }
                    }
                }

            }

        }

        //if we have just finished rolling dice and player has reached space
        if(waitingForPiecesToBeInPosition && currentUsersTurn.piece.hasReachedSpace()){

            if(currentSpace.jail){
                currentUsersTurn.goToJail();
                nextTurn();
            }else if(currentSpace.communityChest){
                communityChestMenu.SetActive(true);
            }else if(currentSpace.chanceTime){
                chanceTimeMenu.SetActive(true);
            }
            //if property is purchasable, open purchase menu. otherwise, next turn.
            else if(currentSpace.owner != null){ 
                //lose money if you landed someone else's space
                if(currentSpace.owner == currentUsersTurn){ //if you land on your own property, double the landing cost of that property

                currentSpace.multiplyLandingCost(2f);
                nextTurn();
                    
                }else{
                    if(currentUsersTurn.items[1] == 0){//if user has just say no
                        payOtherPlayer(currentSpace.owner, currentUsersTurn);
                        nextTurn();
                    }else{
                        justSayNoMenu.SetActive(true);
                    }
                }
                
            }else if(currentSpace.price != 0){
                displayPurchaseMenu();
            }else{
                nextTurn();
            }
            waitingForPiecesToBeInPosition = false;


        }


    }

    public void payOtherPlayer(User payer, User Reciever){

        int payment = currentSpace.paymentForLanding;
        if(payer.perks[1] && !Reciever.banker){//if player has 25% discount perk
            payment = (payment - (payment / 4));
        }

        payer.loseOrGainMoney(payment);

        if(payer.dead){
            payment += payer.money;
        }

        Reciever.loseOrGainMoney(-payment);

    }

    public void displaySelectionMenu(){

        //setting all text/buttons to the correct thing
        diceRollText.SetText("You Rolled A " + References.getDiceSum());

        counterClockwiseSpace = currentUsersTurn.piece.getDestinationCounterClockwiseBSB();
        moveToCCText.SetText(counterClockwiseSpace.spaceName);

        clockwiseSpace = currentUsersTurn.piece.getDestinationClockwiseBSB();
        moveToCText.SetText(clockwiseSpace.spaceName);

        counterClockwiseButton.spaceButtonIsUsedFor = counterClockwiseSpace;
        clockwiseButton.spaceButtonIsUsedFor = clockwiseSpace;

        //actually displaying the menu
        selectionMenu.SetActive(true);

    }

    public void displayNewPlayersTurnMenu(){
        if(References.endGameCheck() == 0){
            newPlayersTurnText.text = "Player " + currentUsersTurn.playerNumber + "'s Turn";
            newPlayersTurnMenu.SetActive(true);
        }
    }

    public void displayEndScreen(int playerNumber){
        endScreenMenu.SetActive(true);
        endScreenText.text = ("Player " + playerNumber + " Wins!!!!");
    }


    public void moveCC(){

        moveGivens();
        currentUsersTurn.piece.moveCounterClockwise();

    }

    public void moveC(){

        moveGivens();
        currentUsersTurn.piece.moveClockwise();
        
    }

    private void moveGivens(){

        selectionMenu.SetActive(false);
        References.mike.moveToNextSpace();
        waitingForPiecesToBeInPosition = true;

    }
    

    public void StartNewGame(){

        Time.timeScale = 1;
        References.allDice.Clear();;

        SceneManager.LoadScene("Game");

    }

    public void nextTurn(){


        if(!References.rolledDoubledCheck() || currentUsersTurn.jailed){

            updatePlayerInControl();

            displayNewPlayersTurnMenu();

        }else if(false){//TODO Check if player rolled 3 doubles in a row

        }else{//if player has rolled less than 3 doubles in a row

            if(currentUsersTurn.dead){
                updatePlayerInControl();
            }else{
                clickDicePrompt.SetActive(true);
            }
        }
        
        hasJustClickedButton = false; 

    }

    void updatePlayerInControl(){

        User NextUser;

        do {

            int currentIndex = References.users.IndexOf(currentUsersTurn);
            int nextIndex = (currentIndex + 1) % References.users.Count;
            NextUser = References.users[nextIndex];

            while(NextUser.dead){
                nextIndex = (nextIndex + 1) % References.users.Count;
                NextUser = References.users[nextIndex];
            }

            // full turn passes if previous player's index is higher
            if (currentUsersTurn.playerNumber > NextUser.playerNumber) {
                endFullTurn();
            }

            // update turn tracker
            currentUsersTurn.displayInfo.currentTurnTracker.SetActive(false);
            NextUser.displayInfo.currentTurnTracker.SetActive(true);
            currentUsersTurn = NextUser;


            if (EffectsBehavior.skipNextPlayer > 0) {
                EffectsBehavior.skipNextPlayer--;
            }
        } while (EffectsBehavior.skipNextPlayer > 0);
    }


    
    void endFullTurn(){

        turnTrackerText.SetText((++turnCount).ToString());

        foreach (User user in References.users)
        {

            if(!user.dead){
                int moneyPerTurn = 100;

                if(currentUsersTurn.jailed){
                    moneyPerTurn = 0;
                }

                if(EffectsBehavior.lose500AtStartOfEveryTurn){//-250 per turn event
                    moneyPerTurn = -250;
                }
                if(user.perks[2]){//+50 per turn perk
                    moneyPerTurn += 50;
                }

                user.loseOrGainMoney(moneyPerTurn);

                foreach (BordSpaceBehavior property in user.propertiesOwned)
                {
                    property.multiplyLandingCost(1.25f);
                }
            }

        }
        
    }

    private void displayPurchaseMenu(){
        purchaseMenu.SetActive(true);
        PurchaseMenuHeaderText.SetText("Would you like to purchase " + currentSpace.spaceName + " for $" + currentSpace.price);
    }


}
