using UnityEngine;
using System.Collections;

public class DamageShutDown : MonoBehaviour {

    public void ShutDown(){
        this.gameObject.SetActive(false);
    }
}
