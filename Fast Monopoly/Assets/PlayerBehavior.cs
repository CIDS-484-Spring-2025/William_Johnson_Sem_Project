using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehavior : MonoBehaviour
{
    public BordSpaceBehavior currentSpace;
    [HideInInspector] public NavMeshAgent navAgent;
    [HideInInspector] public bool playerIsMovingToMike = false;


    void Awake(){
        //References.player = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }


    public void moveClockwise(){

        int spaceToMoveTo = getDestinationClockwiseInt();
        navAgent.destination = References.boardSpaces[spaceToMoveTo].transform.position;
        currentSpace = getDestinationClockwiseBSB();

        playerIsMovingToMike = playerIsMovingToMikeCheck(currentSpace);
            
    }

    public void moveCounterClockwise(){

        int spaceToMoveTo = getDestinationCounterClockwiseInt();
        navAgent.destination = References.boardSpaces[spaceToMoveTo].transform.position;
        currentSpace = getDestinationCounterClockwiseBSB();

        playerIsMovingToMike = playerIsMovingToMikeCheck(currentSpace);

    }

    int getDestinationClockwiseInt(){
        return ((currentSpace.spaceIndex + References.getDiceSum()) % 40);
    }

    public BordSpaceBehavior getDestinationClockwiseBSB(){
        return References.boardSpaces[getDestinationClockwiseInt()];
    }

    int getDestinationCounterClockwiseInt(){
        return ((currentSpace.spaceIndex - References.getDiceSum() + 40) % 40);
    }

    public BordSpaceBehavior getDestinationCounterClockwiseBSB(){
        return References.boardSpaces[getDestinationCounterClockwiseInt()];
    }

    public bool hasReachedSpace(){
        return navAgent.remainingDistance <= 0.3f;
    }

    bool playerIsMovingToMikeCheck(BordSpaceBehavior spaceToMoveTo){

        int mikeLocationIndex = References.mike.currentSpace.spaceIndex;
        int playerLocationIndex = spaceToMoveTo.spaceIndex;

        //if Mike lands on Go To Jail, his true location index is at jail
        if(mikeLocationIndex == 30){
            mikeLocationIndex = 10;
        }

        //odd cases where mike and player is on the edges of the boardspaces array
        if(mikeLocationIndex == 39 && playerLocationIndex == 0){
            return true;
        }
        if(mikeLocationIndex == 0 && playerLocationIndex == 39){
            return true;
        }

        //return true if mike is within one space of the player
        return (mikeLocationIndex - 1 == playerLocationIndex ||
                mikeLocationIndex == playerLocationIndex ||
                mikeLocationIndex + 1 == playerLocationIndex );
        
    }
    
}
