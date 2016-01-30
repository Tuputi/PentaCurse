using UnityEngine;
using System.Collections;

public class HotseatBoard : MonoBehaviour {

    public HotseatPlayer Owner;
    public BlackOverlay Overlay;

	// Use this for initialization
	void Start () {
        Overlay = GetComponentInChildren<BlackOverlay>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public BlackOverlay GetOverlay()
    {
        if(Overlay == null) {
            Overlay = GetComponentInChildren<BlackOverlay>();
        }

        return Overlay;
    }

    public void Enable()
    {
        foreach (var collider in GetComponentsInChildren<Collider2D>()) {
            collider.enabled = true;
        }

        GetOverlay().FadeIn();
    }

    public void Disable()
    {
        foreach(var collider in GetComponentsInChildren<Collider2D>()) {
            collider.enabled = false;
        }

        GetOverlay().FadeOut();
    }
}
