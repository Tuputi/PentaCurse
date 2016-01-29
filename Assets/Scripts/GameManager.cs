using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    bool SpellInitiated = false;

    void Update()
    {
        if (TouchInput.ActiveTouch)
        {
            SpellInitiated = true;
        }

        if (SpellInitiated == true)
        {
            if (!TouchInput.ActiveTouch)
            {
                RuneTouch.Instance.WriteOut();
                SpellInitiated = false;
            }
        }
    }


    

}
