using UnityEngine;
using System.Collections;

public class Rune : MonoBehaviour {

    public RuneType runeType;

    void OnMouseOver()
    {
        if (TouchInput.ActiveTouch)
        {
            RuneTouch.AddTouch(runeType);
        }
    }
}
