using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Healbar : Manager<Healbar>
{

    public Slider OpponentHealthbar;
    public Slider PlayerHealthbar;


    void Update()
    {
        if(PlayerScript.LocalInstance != null) {
            PlayerHealthbar.value = Mathf.Lerp(PlayerHealthbar.value, PlayerScript.LocalInstance.CurrentHealth, 0.1f);
        }

        if (PlayerScript.OtherInstance != null) {
            OpponentHealthbar.value = Mathf.Lerp(OpponentHealthbar.value, PlayerScript.OtherInstance.CurrentHealth, 0.1f);
        }

    }
    public void ChangeHealth(int amount, bool self)
    {
        if (self)
        {
            PlayerHealthbar.value += amount;
        }
        else
        {
            OpponentHealthbar.value += amount;
        }

    }
}
