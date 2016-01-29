using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    bool SpellInitiated = false;
    public Image RuneSymbolBase;
    public Canvas canvas;

    public bool WaitingForSwipe = false;

    void Update()
    {
        if (TouchInput.ActiveTouch)
        {
            SpellInitiated = true;
        }

        if (SpellInitiated == true)
        {
            if (!TouchInput.ActiveTouch)
            {
                Spell spell = SpellList.Instance.CheckWhichSpell(RuneTouch.Instance.GetRunes());
                RuneTouch.Instance.ClearRunes();
                SpellInitiated = false;
                CompleteSpell(spell);
            }
        }

        if (WaitingForSwipe)
        {
           // if(TouchInput. == TouchInput.SwipeDirection.sUp)
           // {
                //do thing;
                WaitingForSwipe = false;
           // }
        }
    }

    public void CompleteSpell(Spell spell)
    {
        Image runeSymbol = Instantiate(RuneSymbolBase);
        runeSymbol.transform.SetParent(canvas.transform, false);
        runeSymbol.transform.localPosition = new Vector2(0, -120);
    }
    

}
