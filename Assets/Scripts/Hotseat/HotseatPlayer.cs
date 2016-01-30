using UnityEngine;
using System.Collections;

public class HotseatPlayer : MonoBehaviour {

    public Spell CurrentSpell;
    public HotseatBoard PlayerBoard;

    public void ResetPlayer()
    {
        PlayerBoard.Overlay.FadeOut();
    }
}
