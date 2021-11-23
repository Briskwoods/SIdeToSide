using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    [SerializeField] private Transform m_player;

    [SerializeField] private Animator m_playerAnimator;

    [SerializeField] private Rigidbody m_rigidbody;

    public bool m_movingForward = false;
    public bool m_movingBack = false;
    public bool m_movingRight = false;
    public bool m_movingLeft = false;
    
    public bool isCentre = true;
    public bool isRight = false;
    public bool isLeft = false;

    public bool isFromLeft = false;
    public bool isFromRight = false;


    public float m_moveDistanceMultiplier = 3f;

    #region Public Bools

    public bool b_canSwipeLeft;
    public bool b_canSwipeRight;
    public bool b_canSwipeUp;
    public bool b_canSwipeDown;
    #endregion

    #region Unused Variable
    private bool m_tap;
    #endregion

    #region Private Bools

    private bool m_swipeLeft;
    private bool m_swipeRight;
    private bool m_swipeUp;
    private bool m_swipeDown;
    private bool m_isDragging;
    #endregion

    private float direction;
    private float m_startMoveDistance;

    private Vector2 m_startTouch;
    private Vector2 m_swipeDelta;

    private void Start()
    {
        m_startMoveDistance = m_moveDistanceMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        m_tap = m_swipeLeft = m_swipeRight = m_swipeUp = m_swipeDown = false;

        
        direction = (Mathf.Atan2(m_swipeDelta.y, m_swipeDelta.x) / (Mathf.PI));

        #region PC Controls
        //PC Controls Test Code, uncoomment if you wish to Test with a mouse instead of Unity Remote 5

        switch (Input.GetMouseButtonDown(0))
        {
            case true:
                m_isDragging = true;
                m_tap = true;
                m_startTouch = Input.mousePosition;
                break;
            case false:
                break;
        }

        switch (Input.GetMouseButtonUp(0))
        {
            case true:
                m_isDragging = false;
                Reset();
                break;
            case false:
                break;
        }
        #endregion

        #region Mobile Controls
        switch (Input.touches.Length > 0)
        {
            case true:
                switch (Input.touches[0].phase == TouchPhase.Began)
                {
                    case true:
                        m_isDragging = true;
                        m_tap = true;
                        m_startTouch = Input.touches[0].position;
                        break;
                    case false:
                        break;
                }
                switch(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    case true:
                        m_isDragging = false;
                        Reset();
                        break;
                    case false:
                        break;
                }
                break;
            case false:
                break;
        }
        #endregion

        // Calculate the distance
        m_swipeDelta = Vector2.zero;
        
        switch (m_isDragging)
        {
            case true:
                switch (Input.touches.Length > 0)
                {
                    case true:
                        m_swipeDelta = Input.touches[0].position - m_startTouch;
                        break;
                    case false:
                        break;
                }
                switch (Input.GetMouseButton(0))
                {
                    case true:
                        m_swipeDelta = (Vector2)Input.mousePosition - m_startTouch;
                        break;
                    case false:
                        break;
                }
                break;
            case false:
                break;
        }

        // Checking if deadzone was crossed.
        switch (m_swipeDelta.magnitude > 125)
        {
            //Detect direction
            case true:
                float x = m_swipeDelta.x;
                float y = m_swipeDelta.y;
                switch (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    case true:
                        // Left Swipe
                        switch (x > 0 && direction > -0.125f && direction < 0.125f)
                        {
                            case true:
                                m_swipeRight = true;
                                break;
                            case false:
                                break;
                        }

                        //Right Swipe 
                        switch (x < 0 && direction < -0.875f || direction > 0.875f)
                        {
                            case true:
                                m_swipeLeft = true;
                                break;
                            case false:
                                break;
                        }
                        break;
                    case false:
                        // Up
                        switch (y > 0 && direction > 0.375f && direction < 0.625f)
                        {
                            case true:
                                m_swipeUp = true;
                                break;
                            case false:
                                break;
                        }

                        // Down
                        switch (y < 0 && direction < -0.375f && direction > -0.625f)
                        {
                            case true:
                                m_swipeDown = true;
                                break;
                            case false:
                                break;
                        }
                        break;
                }
                Reset();
                break;
            case false:
                break;
        }

        #region Use Cases

        switch (m_swipeLeft && b_canSwipeLeft)
        {
            case true:
                m_movingLeft = true;
                m_movingRight = false;
                Reset();
                break;
            case false:
                break;
        }
        
        switch (m_swipeRight && b_canSwipeRight)
        {
            case true:
                m_movingLeft = false;
                m_movingRight = true;
                Reset();
                break;
            case false:
                break;
        }
        
        switch (m_swipeUp && b_canSwipeUp)
        {
            case true:
                m_movingForward = true;
                m_movingBack = false;
                Reset();
                break;
            case false:
                break;
        }

        switch (m_swipeDown && b_canSwipeDown)
        {
            case true:
               m_movingForward = false;
                m_movingBack = true;
                Reset();
                break;
            case false:
                break;
        }
        #endregion

   }

    private void Reset()
    {
        m_startTouch = m_swipeDelta = Vector2.zero;
        m_isDragging = false;
    }
}
