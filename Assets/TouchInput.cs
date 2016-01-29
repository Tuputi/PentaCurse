using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchInput : MonoBehaviour {


    private Vector2 startPos;

    public Image testImage;

    public static bool ActiveTouch = false;


    void Update()
    {
        int NumberOfTouches = Input.touchCount;
        if(NumberOfTouches > 0)
        {
            //first registered touch
            Touch touch = Input.GetTouch(0);
            testImage.transform.position = touch.position;


            if (touch.phase == TouchPhase.Began)
            {
                testImage.material.color = Color.red;
                ActiveTouch = true;
            }

            if(touch.phase == TouchPhase.Ended)
            {
                testImage.material.color = Color.blue;
                ActiveTouch = false;
            }
        }
    }
}
