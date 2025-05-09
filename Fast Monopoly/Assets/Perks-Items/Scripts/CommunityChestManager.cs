using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunityChestManager : MonoBehaviour
{
    public static CommunityChestManager communityChestManager;
    public List<PerkOrItemButtonBehavior> cards = new List<PerkOrItemButtonBehavior>(); 
    public PerkOrItemButtonBehavior testingThing;
    public List<AudioClip> quacks = new List<AudioClip>();

    private void Awake() {
        communityChestManager = this;
    }
    
    private void Start() {
        if (testingThing != null){
            for (int i = 0; i < cards.Count; i++) {
                cards[i] = testingThing;
            }
        }
    }

    public static void removeCard(PerkOrItemButtonBehavior card){
        for (int i = 0; i < communityChestManager.cards.Count; i++) {
            if(communityChestManager.cards[i].removalIndex == card.removalIndex){
                communityChestManager.cards.RemoveAt(i);
                return;
            }
        }
    }
}
