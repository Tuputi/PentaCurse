using UnityEngine;
using System.Collections;

public class HotseatRestartButton : MonoBehaviour {

	public void RestartGame()
    {
        HotseatManager.Instance.ResetBoard();
        HotseatManager.Instance.SetCountdownPhase();
        GameObject.Destroy(gameObject);
    }
}
