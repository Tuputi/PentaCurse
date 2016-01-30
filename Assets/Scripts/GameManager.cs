using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    bool SpellInitiated = false;
    public Image RuneSymbolBase;
    public Canvas canvas;
    public RuneSymbol currentSymbol;
    public Spell currentSpell;
    
    public static GameState state = GameState.draw;

    public bool WaitingForSwipe = false;

    void Update()
    {
        if (TouchInput.ActiveTouch && !WaitingForSwipe)
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
                ReadySpell(spell);
            }
        }

        if (WaitingForSwipe)
        {
            if(TouchInput.swipeDir == TouchInput.SwipeDirection.sUp)
            {
                WaitingForSwipe = false;
                state = GameState.draw;
                Debug.Log("Send spell");
                OnComplete(currentSpell);
            }
        }
    }

    public void ReadySpell(Spell spell)
    {
        currentSpell = spell;
        Image runeSymbol = Instantiate(RuneSymbolBase);
        runeSymbol.transform.SetParent(canvas.transform, false);
        runeSymbol.transform.localPosition = new Vector2(0, -120);
        WaitingForSwipe = true;
        currentSymbol = runeSymbol.GetComponent<RuneSymbol>();
    }

    public void OnComplete(Spell spell)
    {

    }
    

}
