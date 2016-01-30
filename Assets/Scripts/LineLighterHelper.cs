using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LineLighterHelper : Manager<LineLighterHelper>
{

    public List<Line> lines1;
    public List<Line> lines2;
    Color oldColor;

    void Awake()
    {
        oldColor = lines1[0].GetComponent<Image>().color;
    }

    public void LightLineBetween(RuneType a, RuneType b)
    {
        List<Line> activeList = new List<Line>();
        int i = HotseatManager.Instance.CurrentPlayerIndex;
        if(i == 0)
        {
            activeList = lines2;
        }
        else
        {
            activeList = lines1;
        }
        Debug.Log("a: " + a + " b: " + b);
        foreach(Line line in activeList)
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
        List<Line> activeList = new List<Line>();
        int i = HotseatManager.Instance.CurrentPlayerIndex;
        if (i == 0)
        {
            activeList = lines2;
        }
        else
        {
            activeList = lines1;
        }
        foreach (Line line in activeList)
        {
            line.GetComponent<Image>().color = oldColor;
        }
    }
}
