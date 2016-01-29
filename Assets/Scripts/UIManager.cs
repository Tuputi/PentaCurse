using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    static Text Console;

    void Awake()
    {
        Console = GameObject.Find("Console").GetComponent<Text>();
        Console.text = "Initiated";
    }


    public static void WriteToConsole(string text)
    {
        Console.text = text;
    }
	
}
