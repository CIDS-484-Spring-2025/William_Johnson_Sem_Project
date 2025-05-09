using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemBehavior : MonoBehaviour
{
    public int itemOrPerkID;

    public void getItem(){

        References.canvas.currentUsersTurn.items[itemOrPerkID]++;

            References.canvas.communityChestMenu.SetActive(false);
            References.canvas.chanceTimeMenu.SetActive(false);

        if(EffectsBehavior.endTurnAfterAction){
            References.canvas.nextTurn();
        }
    }

    public void getPerk(){

        References.canvas.currentUsersTurn.perks[itemOrPerkID] = true;

        CommunityChestManager.removeCard(this.GetComponent<PerkOrItemButtonBehavior>());

            References.canvas.communityChestMenu.SetActive(false);
            References.canvas.chanceTimeMenu.SetActive(false);

        if(EffectsBehavior.endTurnAfterAction){
            References.canvas.nextTurn();
        }
    }
}
