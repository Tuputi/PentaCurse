using UnityEngine;
using System.Collections.Generic;


public class SpellList : Manager<SpellList>
{

    public List<Spell> spells;
  

    public void CheckWhichSpell(List<RuneType> runes)
    {
        Spell rightSpell = null;
        foreach(Spell sp in spells)
        {
            if(runes.Count == sp.Runes.Count)
            {
                for (int i = 0; i < runes.Count; i++){
                    if(runes[i] == sp.Runes[i])
                    {
                        rightSpell = sp;
                    }
                    else
                    {
                        rightSpell = null;
                        break;
                    }
                }
            }
        }

        if(rightSpell != null)
        {
            Debug.Log("Spell casted: " + rightSpell.SpellName);
        }
        else
        {
            Debug.Log("Fail");
        }
    }

}
