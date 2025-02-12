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
    [HideInInspector] public BordSpaceBehavior counterClockwiseSpace;
    [HideInInspector] public BordSpaceBehavior clockwiseSpace;
    [HideInInspector] public BordSpaceBehavior currentSpace;
    [HideInInspector] public bool waitingForPiecesToBeInPosition = false;
    [HideInInspector] public bool hasJustClickedButton = false;
    [HideInInspector] public int turnCount = -1;


    void Awake(){
        References.canvas = this;
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
        if(waitingForPiecesToBeInPosition && References.player.hasReachedSpace()){

            //if property is purchasable, open purchase menu. otherwise, next turn.
            if(currentSpace.price != 0){
                displayPurchaseMenu();
            }else{
                clickDicePrompt.SetActive(true);
            }
            waitingForPiecesToBeInPosition = false;
            nextTurn();


        }


    }

    private void displaySelectionMenu(){

        //setting all text/buttons to the correct thing
        diceRollText.SetText("You Rolled A " + References.getDiceSum());

        counterClockwiseSpace = References.player.getDestinationCounterClockwiseBSB();
        moveToCCText.SetText(counterClockwiseSpace.spaceName);

        clockwiseSpace = References.player.getDestinationClockwiseBSB();
        moveToCText.SetText(clockwiseSpace.spaceName);

        counterClockwiseButton.spaceButtonIsUsedFor = counterClockwiseSpace;
        clockwiseButton.spaceButtonIsUsedFor = clockwiseSpace;

        //actually displaying the menu
        selectionMenu.SetActive(true);

    }


    public void moveCC(){

        moveGivens();
        References.player.moveCounterClockwise();

    }

    public void moveC(){

        moveGivens();
        References.player.moveClockwise();
        
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

    void nextTurn(){

        turnTrackerText.SetText((++turnCount).ToString());
        hasJustClickedButton = false; //user has no longer "just clicked a button"
        
    }

    private void displayPurchaseMenu(){
        purchaseMenu.SetActive(true);
        PurchaseMenuHeaderText.SetText("Would you like to purchase " + currentSpace.spaceName + " for $" + currentSpace.price);
    }


}
