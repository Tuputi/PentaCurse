using UnityEngine;
using System.Collections;

public class HotseatStartButton : MonoBehaviour
{
    public void StartGame()
    {
        HotseatManager.Instance.SetCountdownPhase();
        GameObject.Destroy(gameObject);
    }
}
