using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkItemSpawnerBehavior : MonoBehaviour
{
    
    [HideInInspector] public PerkOrItemButtonBehavior spawnedPerkOrItem;
    public List<PerkOrItemButtonBehavior> restrictedCardList = new List<PerkOrItemButtonBehavior>(); 
    

    private void OnEnable()
    {
        int randomValue;

        do{
            randomValue = Random.Range(0, CommunityChestManager.communityChestManager.cards.Count);
        }while(restrictedCardCheck(CommunityChestManager.communityChestManager.cards[randomValue].removalIndex));

        spawnedPerkOrItem = Instantiate(CommunityChestManager.communityChestManager.cards[randomValue], transform.position, transform.rotation);
        spawnedPerkOrItem.transform.SetParent(transform);
    }

    private void OnDisable() {
        Destroy(spawnedPerkOrItem.gameObject);
    }

    private bool restrictedCardCheck(int cardID){

        foreach (PerkOrItemButtonBehavior card in restrictedCardList)
        {
            if(card.removalIndex == cardID){
                return true;
            }
        }

        return false;
    }

    
}
