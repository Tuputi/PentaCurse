using UnityEngine;
using System.Collections.Generic;
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
