using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkItemSpawnerBehavior : MonoBehaviour
{

    public List<PerkOrItemButtonBehavior> potentialSpawns = new List<PerkOrItemButtonBehavior>();   
    private PerkOrItemButtonBehavior spawnedPerkOrItem;

    private void OnEnable()
    {
        int randomValue = Random.Range(0, potentialSpawns.Count);
        spawnedPerkOrItem = Instantiate(potentialSpawns[randomValue], transform.position, transform.rotation);
        spawnedPerkOrItem.transform.SetParent(transform);
    }

    private void OnDisable() {
        Destroy(spawnedPerkOrItem.gameObject);
    }

    
}
