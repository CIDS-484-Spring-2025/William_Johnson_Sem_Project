using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoMoreEffectsMenu : MonoBehaviour
{
    public List<PerkItemSpawnerBehavior> spawns = new List<PerkItemSpawnerBehavior>();

    private void OnEnable() {
        foreach (PerkItemSpawnerBehavior spawn in spawns)
        {
            spawn.spawnedPerkOrItem.clickable = false;
        }
    }

    



}
