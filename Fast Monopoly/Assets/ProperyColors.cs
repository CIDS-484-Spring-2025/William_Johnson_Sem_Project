using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyColors
{

    public static BordSpaceBehavior[] brownProperties = new BordSpaceBehavior[2];
    public static BordSpaceBehavior[] lightBlueProperties = new BordSpaceBehavior[3];
    public static BordSpaceBehavior[] pinkProperties = new BordSpaceBehavior[3];
    public static BordSpaceBehavior[] orangeProperties = new BordSpaceBehavior[3];
    public static BordSpaceBehavior[] redProperties = new BordSpaceBehavior[3];
    public static BordSpaceBehavior[] yellowProperties = new BordSpaceBehavior[3];
    public static BordSpaceBehavior[] greenProperties = new BordSpaceBehavior[3];
    public static BordSpaceBehavior[] blueProperties = new BordSpaceBehavior[2];

    public static string findPropertyColor(int colorID){
        switch(colorID){
            case 0:
                return null;
            case 1:
                return "Brown";
            case 2:
                return "Light Blue";
            case 3:
                return "Pink";
            case 4:
                return "Orange";
            case 5:
                return "Red";
            case 6:
                return "Yellow";
            case 7:
                return "Green";
            case 8:
                return "Blue";
        }
        return null;
    }
}
