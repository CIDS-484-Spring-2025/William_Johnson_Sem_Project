using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class References
{
    public static CanvasBehavior canvas;
    public static BordSpaceBehavior[] boardSpaces = new BordSpaceBehavior[40];
    public static PlayerBehavior player;
    public static MikeBehavior mike;
    public static List<DiceBehavior> allDice = new List<DiceBehavior>();
    public static int getDiceSum(){

        int sum = 0;
        foreach (DiceBehavior currentDie in References.allDice){
            sum += currentDie.FindTopSide();
        }

        return sum;

    }
}
