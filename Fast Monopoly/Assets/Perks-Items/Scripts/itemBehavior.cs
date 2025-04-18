using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemBehavior : MonoBehaviour
{
    public int itemID;

    public void getItem(){

        References.canvas.currentUsersTurn.items[itemID]++;

        References.canvas.communityChestMenu.SetActive(false);
        References.canvas.chanceTimeMenu.SetActive(false);

        References.canvas.nextTurn();
    }
}
