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
            if (!TouchInput.ActiveTouch && RuneTouch.Instance.GetRunes().Count > 0) {
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
            OnComplete(currentSpell);
            ClearCurrentSpell();
        } else if(TouchInput.swipeDir == TouchInput.SwipeDirection.sLeft || TouchInput.swipeDir == TouchInput.SwipeDirection.sRight) {
            ClearCurrentSpell();
        }
    }

    public void ClearCurrentSpell()
    {
        SpellList.Instance.DarkenRunes();
        //GameObject.Destroy(currentSymbol.gameObject);
        currentSymbol.GetComponent<Animator>().Play("RuneSymbolSend");
        currentSymbol = null;
        currentSpell = null;
        state = GameState.draw;
        TouchInput.ActiveTouch = false;
        TouchInput.swipeDir = TouchInput.SwipeDirection.sNone;
    }

    public void ReadySpell(Spell spell)
    {
        currentSpell = spell;
       // SpellList.Instance.LightUpSpellRunes(currentSpell);
        state = GameState.send;
        Image runeSymbol = Instantiate(RuneSymbolBase);
        runeSymbol.transform.SetParent(canvas.transform, false);
        runeSymbol.transform.localPosition = new Vector2(0, -120);
        currentSymbol = runeSymbol.GetComponent<RuneSymbol>();
    }

    public void OnComplete(Spell spell)
    {
        Debug.Log("ReadySpell");

        if (PlayerScript.LocalInstance != null) {
            PlayerScript.LocalInstance.SetCurrentSpell(spell);
        }
    }
}
