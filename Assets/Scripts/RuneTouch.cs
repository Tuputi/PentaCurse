using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RuneTouch : Manager<RuneTouch> {

    List<RuneType> touchedRunes;

    void Awake()
    {
        touchedRunes = new List<RuneType>();
    }

    public void AddTouch(RuneType rune)
    {
        if (touchedRunes.Count > 0 && touchedRunes.Last() == rune) {
            return;
        }
       
        touchedRunes.Add(rune);
        SpellList.Instance.LightUpRune(rune);

		SoundScript.Instance.PlaySound (SoundScript.Instance.selectsound);

        if(touchedRunes.Count > 1)
        {
            LineLighterHelper.Instance.LightLineBetween(touchedRunes[touchedRunes.Count - 2], rune);
        }
    }

    public void ClearRunes()
    {
        touchedRunes.Clear();
    }

    public List<RuneType> GetRunes()
    {
        return touchedRunes;
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
        ClearRunes();

		SoundScript.Instance.LetGo ();
    }
}
