using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugText : Manager<DebugText>
{ 
    public Text TextObject;

	// Use this for initialization
	void Start () {
        TextObject = GetComponent<Text>();
	}
	
    public void UpdateText(string text)
    {
        TextObject.text = text;
    }
}
