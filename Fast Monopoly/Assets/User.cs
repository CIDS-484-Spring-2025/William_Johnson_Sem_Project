using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class User : MonoBehaviour
{
    public int playerNumber;
    public int Money;
    public List<BordSpaceBehavior> propertiesOwned = new List<BordSpaceBehavior>();    
    public PlayerBehavior piece;
}
