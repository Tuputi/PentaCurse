using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : Manager<GameManager>
{

    bool SpellInitiated = false;
    public Image RuneSymbolBase;
    public Canvas canvas;
    public Spell currentSpell;

    public Image Clouds;

    public Image cloud;

    void Awake()
    {
        cloud = GameObject.Find("Cloud").GetComponent<Image>();
        cloud.gameObject.SetActive(false);
    }
    
    public static GameState state = GameState.draw;

    void Update()
    {
        if (currentSpell == null) {
            UpdateSpellDraw();
        }
    }

    public void UpdateSpellDraw()
    {
        if (TouchInput.ActiveTouch) {
            SpellInitiated = true;
        }

        if (SpellInitiated) {
            if (!TouchInput.ActiveTouch && RuneTouch.Instance.GetRunes().Count > 0) {
                Spell spell = SpellList.Instance.GetSpellFromPattern(RuneTouch.Instance.GetRunes());
                RuneTouch.Instance.ClearRunes();
                SpellInitiated = false;
                ReadySpell(spell);
            }
        }
    }

    public void ClearCurrentSpell()
    {
        SoundScript.Instance.LetGo();
        if (currentSpell != null) {
            SoundScript.Instance.PlaySound(currentSpell.SendSound);
        }

        HotseatManager.Instance.CastSpell(currentSpell);
        SpellList.Instance.DarkenRunes();
        LineLighterHelper.Instance.DarkenLines();
        currentSpell = null;
        state = GameState.draw;
        TouchInput.ActiveTouch = false;
        TouchInput.swipeDir = TouchInput.SwipeDirection.sNone;
        
    }

    public void ReadySpell(Spell spell)
    {
        currentSpell = spell;
        state = GameState.send;

        HotseatManager.Instance.ChangePlayerTurn();
    }
}
