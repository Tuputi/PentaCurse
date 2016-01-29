using UnityEngine;
using System.Collections.Generic;

public class RuneTouch : Manager<RuneTouch> {

    List<RuneType> touchedRunes;

    void Awake()
    {
        touchedRunes = new List<RuneType>();
    }

    public void AddTouch(RuneType rune)
    {
        touchedRunes.Add(rune);
    }

    public void EmptyTouches()
    {
        touchedRunes.Clear();
    }

    public void WriteOut()
    {
        string tempString = "Spell cast:";
        foreach(RuneType rt in touchedRunes)
        {
            tempString += "\n" + rt;
        }
        UIManager.WriteToConsole(tempString);

        //clear
        EmptyTouches();
    }
}
