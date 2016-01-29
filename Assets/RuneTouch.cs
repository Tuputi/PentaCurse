using UnityEngine;
using System.Collections.Generic;

public class RuneTouch : MonoBehaviour {

    static List<RuneType> touchedRunes;


    public static void AddTouch(RuneType rune)
    {
        touchedRunes.Add(rune);
    }

    public static void WriteOut()
    {
        string tempString = "";
        foreach(RuneType rt in touchedRunes)
        {
            tempString += "\n" + rt;
        }
        UIManager.WriteToConsole(tempString);
    }
}
