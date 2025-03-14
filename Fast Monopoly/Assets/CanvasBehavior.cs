using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasBehavior : MonoBehaviour
{

    public GameObject clickDicePrompt;
    public GameObject selectionMenu;
    public GameObject purchaseMenu;
    public GameObject deathMenu;
    public TextMeshProUGUI diceRollText;
    public TextMeshProUGUI moveToCCText;
    public TextMeshProUGUI moveToCText;
    public TextMeshProUGUI mikeMoveToText;
    public TextMeshProUGUI deathText;
    public TextMeshProUGUI turnTrackerText;
    public TextMeshProUGUI BestTurnTrackerText;
    public CorCCButtonBehavior counterClockwiseButton;
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
        if(!clickDicePrompt.activeSelf && !waitingForPiecesToBeInPosition && !purchaseMenu.activeSelf){

            foreach (DiceBehavior die in References.allDice)
            {
                //if any die is moving or rotating, get out of this loop
                if(die.DiceIsMoving() || die.DiceIsRotating()){
                    break;
                }

                //if this is the last die in the loop, display selection menu
                if(die == References.allDice[References.allDice.Count - 1]){
                    displaySelectionMenu();
                }

            }

        }

        //if we have just finished rolling dice
        if(waitingForPiecesToBeInPosition && currentUsersTurn.piece.hasReachedSpace()){

            //if property is purchasable, open purchase menu. otherwise, next turn.
            if(References.canvas.currentSpace.owner != null){ 
                //lose money if you landed someone else's space
                if(References.canvas.currentSpace.owner == currentUsersTurn){ //if you land on your own property, do thingthign special (increase rent idk)
                    
                }else{
                    References.canvas.currentSpace.owner.loseOrGainMoney(References.canvas.currentSpace.paymentForLanding);
                    currentUsersTurn.loseOrGainMoney(-References.canvas.currentSpace.paymentForLanding);
                }
                
                clickDicePrompt.SetActive(true);
                nextTurn();
            }else if(currentSpace.price != 0){
                displayPurchaseMenu();
            }else{
                clickDicePrompt.SetActive(true);
                nextTurn();
            }
            waitingForPiecesToBeInPosition = false;


        }


    }

    private void displaySelectionMenu(){

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

        if(!References.rolledDoubledCheck()){

            int currentUserNumber = currentUsersTurn.playerNumber;
            User NextUser = References.users[(References.users.Count + (currentUserNumber - 1) + 1) % References.users.Count];

            //full turn passes if previous players index is higher
            if(currentUserNumber > NextUser.playerNumber){
                endFullTurn();
            }

            //update turn tracker in user display info
            currentUsersTurn.displayInfo.currentTurnTracker.SetActive(false);
            NextUser.displayInfo.currentTurnTracker.SetActive(true);

            currentUsersTurn = NextUser;

        }
        
        hasJustClickedButton = false; 

    }

    void endFullTurn(){

        turnTrackerText.SetText((++turnCount).ToString());
        
    }

    private void displayPurchaseMenu(){
        purchaseMenu.SetActive(true);
        PurchaseMenuHeaderText.SetText("Would you like to purchase " + currentSpace.spaceName + " for $" + currentSpace.price);
    }


}
