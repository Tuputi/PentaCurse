using UnityEngine;
using System.Collections;

public class RuneSymbol : MonoBehaviour {

	public void Delete()
    {
        Destroy(this.gameObject);
    }

    public void ReadyToSend()
    {
        Debug.Log("Ready to send");
    }
}
