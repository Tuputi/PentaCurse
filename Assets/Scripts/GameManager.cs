using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    bool SpellInitiated = false;

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
    }

    public void CompleteSpell(Spell spell)
    {
        //do stuff
    }
    

}
