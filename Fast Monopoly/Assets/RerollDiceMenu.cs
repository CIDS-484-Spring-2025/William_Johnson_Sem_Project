using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerollDiceMenu : MonoBehaviour
{
    private BordSpaceBehavior clockwiseDestBHB;
    private BordSpaceBehavior counterClockwiseDestBHB;
    

    private void OnEnable() {
        clockwiseDestBHB = References.canvas.currentUsersTurn.piece.getDestinationClockwiseBSB();
        counterClockwiseDestBHB = References.canvas.currentUsersTurn.piece.getDestinationCounterClockwiseBSB();

        clockwiseDestBHB.blueRerollDiceHighlight.SetActive(true);
        counterClockwiseDestBHB.blueRerollDiceHighlight.SetActive(true);
    }

    private void OnDisable() {
        clockwiseDestBHB.blueRerollDiceHighlight.SetActive(false);
        counterClockwiseDestBHB.blueRerollDiceHighlight.SetActive(false);
    }
}
