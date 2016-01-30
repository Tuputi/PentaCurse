using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Rune : MonoBehaviour {

    public RuneType runeType;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (TouchInput.ActiveTouch)
        {
            RuneTouch.Instance.AddTouch(runeType);
        }
    }

    void OnTriggerExit2d(Collider2D other)
    {
        if (TouchInput.ActiveTouch)
        {
            RuneTouch.Instance.AddTouch(runeType);
        }
    }


}
