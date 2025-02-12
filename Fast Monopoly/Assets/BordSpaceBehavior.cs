using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordSpaceBehavior : MonoBehaviour
{

    public string spaceName;
    public int spaceIndex;
    public GameObject highLight;
    public int paymentForLanding;
    public int ColorID;
    private string propertyColor;
    public int price;
    [HideInInspector] public bool owned;
    void Awake(){
        References.boardSpaces[spaceIndex] = this;
        propertyColor = PropertyColors.findPropertyColor(ColorID);
    }
    
}
