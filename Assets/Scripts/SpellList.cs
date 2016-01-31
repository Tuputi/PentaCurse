using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;


public class SpellList : Manager<SpellList>
{
    public List<Image> PublicRuneImages1;
    public List<Image> PublicRuneImages2;
    private Dictionary<RuneType, Image> RuneImages1;
    private Dictionary<RuneType, Image> RuneImages2;
    public List<Spell> spells;
    public Spell fallBack;
    Color oldColor;


    void Awake()
    {
        oldColor = PublicRuneImages1[0].color;
        RuneImages1 = new Dictionary<RuneType, Image>();
        int i = 1;
        foreach(Image im in PublicRuneImages1)
        {
            RuneImages1.Add((RuneType)i, im);
            i++;
        }

        RuneImages2 = new Dictionary<RuneType, Image>();
        int j = 1;
        foreach (Image im in PublicRuneImages2)
        {
            RuneImages2.Add((RuneType)j, im);
            j++;
        }

    }

    public Spell GetSpellFromPattern(List<RuneType> runes)
    {
        var normalCandidates = new List<Spell>();
        var CircularCandidates = new List<Spell>();
        var reversedCircularCandidates = new List<Spell>();

        // Make a first sorting based on the length of the pattern
        foreach(var spell in spells) {
            if (spell.CircularPattern) {
                if(spell.Runes.Count +1 == runes.Count) {
                    CircularCandidates.Add(spell);
                    if (spell.AllowReversed) {
                        reversedCircularCandidates.Add(spell);
                    }
                }
            } else {
                if (spell.Runes.Count == runes.Count) {
                    normalCandidates.Add(spell);
                }
            }
        }

        var validExactSpells = FilterExactPatterns(runes, normalCandidates);
        var validRevereseSpells = FilterReversablePatterns(runes, normalCandidates);
        var validCirularSpells = FilterCircularPatterns(runes, CircularCandidates);
        var validReversedCircularSpells = FilterReversedCircularPatterns(runes, reversedCircularCandidates);

        var totalList = new List<Spell>();
        totalList.AddRange(validExactSpells);
        totalList.AddRange(validRevereseSpells);
        totalList.AddRange(validCirularSpells);
        totalList.AddRange(validReversedCircularSpells);

        var result = totalList.FirstOrDefault();

        if(result == null) {
            result = fallBack;
        }

        Debug.Log("Combined spell:" + result.SpellName);
        return result;
    }

    private List<Spell> FilterExactPatterns(List<RuneType> runes, List<Spell> candidates)
    {
        var result = new List<Spell>(candidates);
        var invalidList = new HashSet<Spell>();
        for(int i = 0; i < runes.Count; i++) {
            foreach(var spell in result) {
                if (runes[i] != spell.Runes[i]) {
                    invalidList.Add(spell);
                }
            }
        }

        foreach(var item in invalidList) {
            result.Remove(item);
        }

        return result;
    }

    private List<Spell> FilterReversablePatterns(List<RuneType> runes, List<Spell> candidates)
    {
        var validForReverse = candidates.Where(c => c.AllowReversed).ToList();
        var reversedRuneList = new List<RuneType>(runes);
        reversedRuneList.Reverse();
        return FilterExactPatterns(reversedRuneList, validForReverse);
    }

    private List<Spell> FilterCircularPatterns(List<RuneType> runes, List<Spell> candidates)
    {
        var validCircularSpells = candidates.Where(c => c.CircularPattern).ToList();

        var result = new List<Spell>();

        foreach (var spell in validCircularSpells) {
            var spellValid = true;
            var startIndex = FindIndexOfset(spell, runes);
            if(startIndex > -1) {
                for(int i = 0; i < runes.Count; i++) {
                    if(spell.Runes[(i + startIndex) % spell.Runes.Count] != runes[i]) {
                        spellValid = false;
                    }
                }
            } else {
                spellValid = false;
            }

            if (spellValid) {
                result.Add(spell);
            }
        }

        return result;
    }

    private int FindIndexOfset(Spell spell, List<RuneType> runes)
    {
        for (int i = 0; i < spell.Runes.Count; i++) {
            if (runes[0] == spell.Runes[i]) {
                return i;
            }
        }

        return -1;
    }

    private List<Spell> FilterReversedCircularPatterns(List<RuneType> runes, List<Spell> candidates)
    {
        var reversedRuneList = new List<RuneType>(runes);
        reversedRuneList.Reverse();

        return FilterCircularPatterns(reversedRuneList, candidates);
    }

    public Spell CheckWhichSpell(List<RuneType> runes)
    {
        string str = "";
        foreach(RuneType rune in runes)
        {
            str += "\n" + rune.ToString();
        }
        Spell rightSpell = null;
        foreach(Spell sp in spells)
        {
            if(runes.Count == sp.Runes.Count && rightSpell == null)
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
            return rightSpell;
        }
        else
        {
            Debug.Log("Spell casted: " + fallBack.SpellName);
            return fallBack;
        }
    }

    public void LightUpRune(RuneType runeType)
    {
        //RuneImages[runeType].GetComponent<Animator>().SetBool("LightUp", true);
        //oldColor = RuneImages[runeType].GetComponent<Image>().color;
        int i = HotseatManager.Instance.CurrentPlayerIndex;
        if (i == 0)
        {
            RuneImages1[runeType].GetComponent<Image>().color = new Color(255, 255, 255);
        }
        else
        {
            RuneImages2[runeType].GetComponent<Image>().color = new Color(255, 255, 255);
        }
    }

    public void DarkenRunes()
    {
        int i = HotseatManager.Instance.CurrentPlayerIndex;
        if (i == 0)
        {
            foreach (Image img in PublicRuneImages1)
            {
                //i.GetComponent<Animator>().SetBool("LightUp", false);
                img.color = oldColor;
            }
        }
        else
        {
            foreach (Image img in PublicRuneImages2)
            {
                //i.GetComponent<Animator>().SetBool("LightUp", false);
                img.color = oldColor;
            }
        }
    }

}
