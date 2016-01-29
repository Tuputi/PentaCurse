using UnityEngine;
using System.Collections;

public class Rune : MonoBehaviour {

    public RuneType runeType;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (TouchInput.ActiveTouch)
        {
            RuneTouch.Instance.AddTouch(runeType);
        }

        
    }
}
