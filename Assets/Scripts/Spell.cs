using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu]
[System.Serializable]
public class Spell : ScriptableObject{

    public List<RuneType> Runes;
    public string SpellName;
    public Sprite RuneSymbol;
    public AudioClip SendSound;

    public bool AllowReversed;
    public bool CircularPattern;

    public List<CounterSpellResult> Counters;

    public SpellResult GetResultForSpell(Spell spell)
    {
        foreach(var counter in Counters) {
            if(counter.Spell == spell) {
                return counter.Result;
            }
        }

        return SpellResult.Losing;
    }
}
