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

    void Update()
    {
        if (currentSpell == null) {
            UpdateSpellDraw();
        } else {
            UpdateSwipe();
        }
    }

    public void UpdateSpellDraw()
    {
        if (TouchInput.ActiveTouch) {
            SpellInitiated = true;
        }

        if (SpellInitiated) {
            if (!TouchInput.ActiveTouch) {
                Spell spell = SpellList.Instance.CheckWhichSpell(RuneTouch.Instance.GetRunes());
                RuneTouch.Instance.ClearRunes();
                SpellInitiated = false;
                ReadySpell(spell);
            }
        }
    }

    public void UpdateSwipe()
    {
        if (TouchInput.swipeDir == TouchInput.SwipeDirection.sUp) {
            currentSpell = null;
            state = GameState.draw;
            TouchInput.ActiveTouch = false;
            TouchInput.swipeDir = TouchInput.SwipeDirection.sNone;
            OnComplete(currentSpell);
        }
    }

    public void ReadySpell(Spell spell)
    {
        currentSpell = spell;
        state = GameState.send;
        Image runeSymbol = Instantiate(RuneSymbolBase);
        runeSymbol.transform.SetParent(canvas.transform, false);
        runeSymbol.transform.localPosition = new Vector2(0, -120);
        currentSymbol = runeSymbol.GetComponent<RuneSymbol>();
    }

    public void OnComplete(Spell spell)
    {
        Debug.Log("ReadySpell");
    }
}
