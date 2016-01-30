﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class SpellList : Manager<SpellList>
{
    public List<Image> PublicRuneImages;
    private Dictionary<RuneType, Image> RuneImages;
    public List<Spell> spells;
    public Spell fallBack;
    Color oldColor;


    void Awake()
    {
        oldColor = PublicRuneImages[0].color;
        RuneImages = new Dictionary<RuneType, Image>();
        int i = 1;
        foreach(Image im in PublicRuneImages)
        {
            RuneImages.Add((RuneType)i, im);
            i++;
        }

        Debug.Log("RuneCount " + RuneImages.Count);
    }


    public Spell CheckWhichSpell(List<RuneType> runes)
    {
        string str = "";
        Debug.Log(runes.Count);
        foreach(RuneType rune in runes)
        {
            str += "\n" + rune.ToString();
        }
        Debug.Log(str);
        Spell rightSpell = null;
        foreach(Spell sp in spells)
        {
            if(runes.Count == sp.Runes.Count && rightSpell == null)
            {
                Debug.Log("Right amount of symbols");
                for (int i = 0; i < runes.Count; i++){
                    if(runes[i] == sp.Runes[i])
                    {
                        Debug.Log("Symbol " + i + " right");
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
        RuneImages[runeType].GetComponent<Image>().color = new Color(255, 255, 255);
    }

    public void DarkenRunes()
    {
        foreach(Image i in PublicRuneImages)
        {
            //i.GetComponent<Animator>().SetBool("LightUp", false);
            i.color = oldColor;
        }
    }

}
