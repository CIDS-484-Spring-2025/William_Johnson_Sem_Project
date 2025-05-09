using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice3SpawnpointBehavior : MonoBehaviour
{
    private void Awake() {
        DiceBehavior.dice3SpawnPoint = this;
    }
}
