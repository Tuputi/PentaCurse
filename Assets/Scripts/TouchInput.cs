using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchInput : MonoBehaviour {

    public UnityEngine.UI.Text text;

    private Vector2 startPos;
    private float swipeStartTime;
    public float minVelocity = -50f;
    public float minSwipeDistance = 25.0f;
    Vector2 mXAxis = new Vector2(1, 0);
    Vector2 mYAxis = new Vector2(0, 1);
    float minAngle = 30f;

    public Image cursorImage;

    public static bool ActiveTouch = false;

    public bool TouchEnabled = true;

   
    public enum SwipeDirection { sNone, sLeft, sRight, sUp, sDown };
    
    public static SwipeDirection swipeDir;

    void Start()
    {
        swipeDir = SwipeDirection.sNone;
    }

    void Update()
    {
        if (!TouchEnabled)
        {
            MouseControl();
            return;
        }

        if (GameManager.state == GameState.draw)
        {
            DrawState();
        }
        else if( GameManager.state == GameState.send)
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
        cursorImage.color = Color.black;

        int NumberOfTouches = Input.touchCount;
        if (NumberOfTouches > 0)
        {
            cursorImage.color = Color.red;

            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
                swipeStartTime = Time.time;
            }

            float tempDelta = Time.deltaTime - swipeStartTime;
            Vector2 tempswipeVector = touch.position - startPos;
            float tempvelocity = tempswipeVector.magnitude / tempDelta;
            text.text = "TempVelo " +tempvelocity.ToString();
            text.text += "\n tempmagni " + tempswipeVector.magnitude;

            if (touch.phase == TouchPhase.Ended)
            {
                float DeltaTime = Time.time - swipeStartTime;
                Vector2 endPos = touch.position;

                Vector2 swipeVector = endPos - startPos;
                float velocity = swipeVector.magnitude / DeltaTime;

               // if(velocity > minVelocity && swipeVector.magnitude > minSwipeDistance)
               // {
                    //ladies and gentlement, we have a swipe
                    cursorImage.color = Color.green;

                    swipeVector.Normalize();

                    float angleOfSwipe = Vector2.Dot(swipeVector, mXAxis);
                    angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;

                    if (angleOfSwipe < minAngle)
                    {
                        //right
                        swipeDir = SwipeDirection.sRight;
                        Debug.Log("Swipe to right");
                    }
                    else if ((180f - angleOfSwipe) < minAngle)
                    {
                        //left
                        swipeDir = SwipeDirection.sLeft;
                        Debug.Log("Swipe to left");
                    }
                    else
                    {
                        angleOfSwipe = Vector2.Dot(swipeVector, mYAxis);
                        angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;
                        if (angleOfSwipe < minAngle)
                        {
                            //top
                            swipeDir = SwipeDirection.sUp;
                            Debug.Log("Swipe to up");
                        }
                        else if ((180f - angleOfSwipe) < minAngle)
                        {
                            //down
                            swipeDir = SwipeDirection.sDown;
                            Debug.Log("Swipe to down");
                        }
                        else
                        {
                            //errror
                        }
                    }
                //}
            }
        }
    }

    public void MouseControl()
    {
        if (GameManager.state == GameState.draw)
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

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            swipeDir = SwipeDirection.sUp;
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            swipeDir = SwipeDirection.sDown;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            swipeDir = SwipeDirection.sLeft;
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            swipeDir = SwipeDirection.sRight;
        }
    }
}
