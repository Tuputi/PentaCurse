using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LineLighterHelper : Manager<LineLighterHelper>
{

    public List<Line> lines;
    Color oldColor;

    void Awake()
    {
        oldColor = lines[0].GetComponent<Image>().color;
    }

    public void LightLineBetween(RuneType a, RuneType b)
    {
        Debug.Log("a: " + a + " b: " + b);
        foreach(Line line in lines)
        {
            if(line.runes.Contains(a) && line.runes.Contains(b))
            {
                Debug.Log("Foundline");
               
                line.GetComponent<Image>().color = new Color(255, 255, 255);
                return;
            }
        }
    }

    public void DarkenLines()
    {
        foreach(Line line in lines)
        {
            line.GetComponent<Image>().color = oldColor;
        }
    }
}
