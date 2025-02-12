using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MikeBehavior : MonoBehaviour
{
    public BordSpaceBehavior currentSpace;
    public GameObject goToJailSound;
    [HideInInspector] public NavMeshAgent navAgent;


    void Awake(){
        References.mike = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Update(){

        //highlight the next space your moving to in red
        if(References.canvas.selectionMenu.activeSelf){
            nextSpaceBNB().highLight.SetActive(true);
        }

        //once the user selects a spot to move too, stop highlighting the space
        if(References.canvas.hasJustClickedButton){
            currentSpace.highLight.SetActive(false);
        }

        //if Mike lands on Go To Jail... then go to jail
        if(navAgent.remainingDistance <= 0.2 && 
           currentSpace == References.boardSpaces[30]){

            goToJail();

        }


    }

    public void moveToNextSpace(){
        int spaceToMoveTo = nextSpaceInt();
        navAgent.destination = References.boardSpaces[spaceToMoveTo].transform.position;
        currentSpace = nextSpaceBNB();
    }

    public int nextSpaceInt(){
        return ((currentSpace.spaceIndex + (References.getDiceSum()/2) ) % 40);
    }

    public BordSpaceBehavior nextSpaceBNB(){
        return References.boardSpaces[nextSpaceInt()];
    }

    public void goToJail(){

        Instantiate(goToJailSound, transform);

        currentSpace = References.boardSpaces[10];
        transform.position = References.boardSpaces[10].transform.position;
        navAgent.destination = transform.position;
        transform.LookAt(References.boardSpaces[11].transform.position);

    }
}

