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
    public static List<User> users = new List<User>();
    public static int getDiceSum(){

        int sum = 0;
        foreach (DiceBehavior currentDie in References.allDice){
            sum += currentDie.FindTopSide();
        }

        return sum;

    }

    public static bool rolledDoubledCheck(){

        int rolledValue = References.allDice[0].FindTopSide();
        foreach (DiceBehavior currentDie in References.allDice){
            if(rolledValue != currentDie.FindTopSide()){
                return false;
            }
        }
        return true;
    }
}
