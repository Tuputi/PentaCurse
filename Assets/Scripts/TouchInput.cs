using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchInput : MonoBehaviour {


    private Vector2 startPos;
    private float swipeStartTime;
    public float minVelocity = 50f;
    public float minSwipeDistance = 2000.0f;
    Vector2 mXAxis = new Vector2(1, 0);
    Vector2 mYAxis = new Vector2(0, 1);
    float minAngle = 30f;

    public Image cursorImage;

    public static bool ActiveTouch = false;

    public bool TouchEnabled = true;

    public enum GameState { draw, swipe};
    public enum SwipeDirection { sLeft, sRight, sUp, sDown };
    GameState state;
    public SwipeDirection swipeDir;

    void Update()
    {
        if (!TouchEnabled)
        {
            MouseControl();
            return;
        }

        if (state == GameState.draw)
        {
            DrawState();
        }
        else
        {
            SwipeState();
        }
    }

    public void DrawState()
    {
        int NumberOfTouches = Input.touchCount;
        if (NumberOfTouches > 0)
        {
            //first registered touch
            Touch touch = Input.GetTouch(0);
            cursorImage.transform.position = touch.position;


            if (touch.phase == TouchPhase.Began)
            {
                ActiveTouch = true;
            }
        }

        else {
            ActiveTouch = false;
        }
    }


    public void SwipeState()
    {
        int NumberOfTouches = Input.touchCount;
        if (NumberOfTouches > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
                swipeStartTime = Time.time;
            }

            if(touch.phase == TouchPhase.Ended)
            {
                float DeltaTime = Time.time - swipeStartTime;
                Vector2 endPos = touch.position;

                Vector2 swipeVector = endPos - startPos;
                float velocity = swipeVector.magnitude / DeltaTime;

                if(velocity > minVelocity && swipeVector.magnitude > minSwipeDistance)
                {
                    //ladies and gentlement, we have a swipe
                    swipeVector.Normalize();

                    float angleOfSwipe = Vector2.Dot(swipeVector, mXAxis);
                    angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;

                    if (angleOfSwipe < minAngle)
                    {
                        //right
                        swipeDir = SwipeDirection.sRight;
                    }
                    else if ((180f - angleOfSwipe) < minAngle)
                    {
                        //left
                        swipeDir = SwipeDirection.sLeft;
                    }
                    else
                    {
                        angleOfSwipe = Vector2.Dot(swipeVector, mYAxis);
                        angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;
                        if (angleOfSwipe < minAngle)
                        {
                            //top
                            swipeDir = SwipeDirection.sUp;
                        }
                        else if ((180f - angleOfSwipe) < minAngle)
                        {
                            //down
                            swipeDir = SwipeDirection.sDown;;
                        }
                        else
                        {
                            //errror
                        }
                    }
                }
            }
        }
    }

    public void MouseControl()
    {
        cursorImage.transform.position = Input.mousePosition;

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
