using UnityEngine;
using System.Collections;

public class HotseatBoard : MonoBehaviour {

    public HotseatPlayer Owner;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Enable()
    {
        foreach (var collider in GetComponentsInChildren<Collider2D>()) {
            collider.enabled = true;
        }
    }

    public void Disable()
    {
        foreach(var collider in GetComponentsInChildren<Collider2D>()) {
            collider.enabled = false;
        }
    }
}
