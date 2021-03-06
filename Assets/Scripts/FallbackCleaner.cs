﻿using UnityEngine;
using System.Collections;

public class FallbackCleaner : MonoBehaviour {

	public void Clean()
    {
        SpellList.Instance.DarkenRunes();
        LineLighterHelper.Instance.DarkenLines();
        GameManager.Instance.currentSpell = null;
        GameManager.state = GameState.draw;
        TouchInput.ActiveTouch = false;
        TouchInput.swipeDir = TouchInput.SwipeDirection.sNone;
        this.gameObject.SetActive(false);
    }
}
