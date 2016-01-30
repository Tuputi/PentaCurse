using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Healbar : Manager<Healbar>
{

    public Slider OpponentHealthbar;
    public Slider PlayerHealthbar;



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
