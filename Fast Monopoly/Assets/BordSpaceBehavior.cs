using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BordSpaceBehavior : MonoBehaviour
{
    public string spaceName;
    public int spaceIndex;
    public GameObject highLight;
    public int paymentForLanding;
    public int ColorID;
    private string propertyColor;
    public int price;
    public GameObject[] ownershipHighlights = new GameObject[5];
    public User owner;
    public GameObject costForLandingCanvas;
    public TextMeshProUGUI costForLandingTMP;
    public bool communityChest;
    public bool chanceTime;
    public bool jail;
    void Awake(){
        References.boardSpaces[spaceIndex] = this;
        propertyColor = PropertyColors.findPropertyColor(ColorID);
    }

    void Start(){
        if(owner != null && owner.banker){
            activateOwnershipHighlight(5);
        }
    }

    public void activateOwnershipHighlight(int playerNumber){
        ownershipHighlights[playerNumber - 1].SetActive(true);
        costForLandingCanvas.SetActive(true);
        updateCostText(paymentForLanding);
    }

    public void updateCostText(int cost){
        costForLandingTMP.text = "$" + cost.ToString();
    }

    public void doubleCost(){
        paymentForLanding *= 2;
        updateCostText(paymentForLanding);
    }
    
}
