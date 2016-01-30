using UnityEngine;
using System.Collections.Generic;

public class LineLighterHelper : Manager<LineLighterHelper>
{

    public List<Line> lines;


    public void LightLineBetween(RuneType a, RuneType b)
    {
        foreach(Line line in lines)
        {
            if(line.runes.Contains(a) && line.runes.Contains(b))
            {
                line.GetComponent<Animator>().SetBool("LightUp", true);
            }
        }
    }

    public void DarkenLines()
    {
        foreach(Line line in lines)
        {
            line.GetComponent<Animator>().SetBool("LightUp", false);
        }
    }
}
