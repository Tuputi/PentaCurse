using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : Manager<GameManager>
{

    bool SpellInitiated = false;
    public Image RuneSymbolBase;
    public Canvas canvas;
    public RuneSymbol currentSymbol;
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
        //sorry for silvertejp
        if (cloud.gameObject.activeSelf)
        {
            cloud.gameObject.SetActive(false);
        }

        HotseatManager.Instance.CastSpell(currentSpell);
        SpellList.Instance.DarkenRunes();
        LineLighterHelper.Instance.DarkenLines();
        currentSymbol.GetComponent<Animator>().Play("RuneSymbolSend");
        currentSymbol = null;
        currentSpell = null;
        state = GameState.draw;
        TouchInput.ActiveTouch = false;
        TouchInput.swipeDir = TouchInput.SwipeDirection.sNone;
        HotseatManager.Instance.ChangePlayerTurn();
    }

    public void ReadySpell(Spell spell)
    {
        currentSpell = spell;
        state = GameState.send;
        Image runeSymbol = Instantiate(RuneSymbolBase);

        if (currentSpell == SpellList.Instance.fallBack){
            if (PlayerScript.LocalInstance != null)
            {
                var health = PlayerScript.LocalInstance.CurrentHealth - 10;
                PlayerScript.LocalInstance.SetCurrentHealth(health);
            }
            cloud.gameObject.SetActive(true);
            OnComplete(currentSpell);
        } 

        runeSymbol.sprite = spell.RuneSymbol;
        runeSymbol.transform.SetParent(canvas.transform, false);
        runeSymbol.transform.localPosition = new Vector2(0, -120);
        currentSymbol = runeSymbol.GetComponent<RuneSymbol>();
        ClearCurrentSpell();
    }

    public void OnComplete(Spell spell)
    {
        if (PlayerScript.LocalInstance != null) {
            PlayerScript.LocalInstance.SetCurrentSpell(spell);
        }
		SoundScript.Instance.LetGo ();
		SoundScript.Instance.PlaySound (spell.SendSound);
    }
}
