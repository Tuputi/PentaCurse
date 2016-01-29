using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchInput : MonoBehaviour {


    private Vector2 startPos;

    public Image testImage;

    public static bool ActiveTouch = false;

    public bool TouchEnabled = true;

    void Update()
    {
        if (!TouchEnabled)
        {
            MouseControl();
            return;
        }

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

           
        }

        else {
            testImage.material.color = Color.blue;
            ActiveTouch = false;
        }
    }


    public void MouseControl()
    {
        testImage.transform.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            ActiveTouch = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            ActiveTouch = false;
        }
    }
}
