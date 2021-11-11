using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    // Raycast Control Variable
    [SerializeField] private Camera m_mainCamera;

    // Hand Control Variables
    [SerializeField] private GameObject m_rightHand;

    [Range (1, 100)]
    [SerializeField] private int m_handSpeed = 75;

    [Range(1, 100)]
    [SerializeField] private int m_handReturnSpeed = 9;

    [SerializeField] private BoardCharacterController m_selectedCharacter;

    public LayerMask Layer_;

    private Vector3 m_originalPosition;

    public bool m_isActive;

    [SerializeField] private Animator m_handAnimator;

    // Swipe Variables

    private bool m_swipeLeft;
    private bool m_swipeRight;
    private bool m_swipeUp;
    private bool m_swipeDown;
    [SerializeField] private bool m_isDragging;

    [SerializeField] private bool firstClick = false;
    [SerializeField] private bool secondClick = false;

    public bool m_hasSelectedCharacter;

    private float direction;

    private Vector2 m_startTouch;
    private Vector2 m_swipeDelta;

    //[SerializeField] private float m_hoverDistance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        m_originalPosition = transform.position;
        m_handAnimator = this.GetComponent<Animator>();
        m_rightHand.transform.position = m_originalPosition;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_isActive)
        {
            case true:
                // Animator Controller
                m_handAnimator.SetBool("isActive", true);

                // Swipe Reset
                m_swipeLeft = m_swipeRight = m_swipeUp = m_swipeDown = false;

                // Check Swipe Direction
                direction = (Mathf.Atan2(m_swipeDelta.y, m_swipeDelta.x) / (Mathf.PI));

                // Hand Follows Mouse Movement
                Ray ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
                switch (Physics.Raycast(ray, out RaycastHit raycastHit))
                {
                    case true:
                        m_rightHand.transform.position = Vector3.MoveTowards(m_rightHand.transform.position, raycastHit.point, m_handSpeed);
                        break;
                    case false: break;
                }

                // OnClick/Select Events
                switch (Input.GetMouseButtonDown(0))
                {
                    case true:
                        // As long as mouse is down the mouse is dragging
                        m_isDragging = true;
                        m_startTouch = Input.mousePosition;

                        //We cast a ray to detect if a character is hit, character is then stored in selectedCharacter Variable. This can be used to control active and inactive state of characters
                        RaycastHit hitInfo;
                        Ray ray_ = Camera.main.ScreenPointToRay(Input.mousePosition);
                        bool hit = (Physics.Raycast(ray_, out hitInfo, Mathf.Infinity, Layer_));

                        switch (hit)
                        {
                            case true:

                                // Code to unselect characters on 2nd click       (Not Necessary, doesn't work)                      
                                //switch ((m_hasSelectedCharacter == true && hitInfo.transform.gameObject.tag == "BoardCharacter" && m_selectedCharacter.m_timesClickedOn == 1 && m_selectedCharacter.isSelected == true && firstClick == true && m_isDragging == false))
                                //{
                                //    case true:
                                //        m_selectedCharacter.isSelected = false;
                                //        m_hasSelectedCharacter = false;
                                //        m_selectedCharacter.m_timesClickedOn = 0;
                                //        m_selectedCharacter = null;
                                //        secondClick = true;
                                //        firstClick = false;
                                //        break;
                                //    case false:
                                //        break;
                                //}

                                // Switch Character
                                switch ((m_hasSelectedCharacter == true && m_selectedCharacter != null && hitInfo.transform.gameObject != m_selectedCharacter && hitInfo.transform.gameObject.tag == "BoardCharacter" && firstClick == true))
                                {
                                    case true:
                                        m_selectedCharacter.isSelected = false;
                                        m_selectedCharacter = hitInfo.transform.gameObject.GetComponentInChildren<BoardCharacterController>();
                                        m_selectedCharacter.isSelected = true;
                                        firstClick = true;
                                        secondClick = false;
                                        break;
                                    case false: break;
                                }

                                // Select Character
                                switch ((m_hasSelectedCharacter == false &&  hitInfo.transform.gameObject.tag == "BoardCharacter" && m_selectedCharacter == null && hitInfo.transform.gameObject.GetComponent<BoardCharacterController>().isSelected == false && secondClick == false))
                                {
                                    case true:
                                        hitInfo.transform.gameObject.GetComponentInChildren<BoardCharacterController>().isSelected = true;
                                        m_selectedCharacter = hitInfo.transform.gameObject.GetComponentInChildren<BoardCharacterController>();
                                        m_selectedCharacter.m_timesClickedOn = 1;
                                        m_hasSelectedCharacter = true;
                                        firstClick = true;
                                        secondClick = false;
                                        break;
                                    case false:
                                        break;
                                }
                                secondClick = false;
                                break;

                            case false: break;
                        }
                        break;
                    case false: break;
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

                // Calculate the swipe distance and direction
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

                switch (m_swipeDelta.magnitude > 125)
                {
                    //Detect direction
                    case true:
                        float x = m_swipeDelta.x;
                        float y = m_swipeDelta.y;
                        switch (Mathf.Abs(x) > Mathf.Abs(y))
                        {
                            case true:
                                // Right Swipe
                                switch (x > 0 && direction > -0.375f && direction < 0.375f)
                                {
                                    case true:
                                        m_swipeRight = true;
                                        m_selectedCharacter.m_moveRight = m_swipeRight;
                                        m_selectedCharacter.isMoving = true;
                                        m_selectedCharacter.m_timesMoved = 1;
                                        break;
                                    case false:
                                        break;
                                }

                                // Left Swipe 
                                switch (x < 0 && direction < -0.875f || direction > 0.875f)
                                {
                                    case true:
                                        m_swipeLeft = true;
                                        m_selectedCharacter.m_moveLeft = m_swipeLeft;
                                        m_selectedCharacter.isMoving = true;
                                        m_selectedCharacter.m_timesMoved = 1;
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
                                        m_selectedCharacter.m_moveUp = m_swipeUp;
                                        m_selectedCharacter.isMoving = true;
                                        m_selectedCharacter.m_timesMoved = 1;
                                        break;
                                    case false:
                                        break;
                                }

                                // Down
                                switch (y < 0 && direction < -0.375f && direction > -0.625f)
                                {
                                    case true:
                                        m_swipeDown = true;
                                        m_selectedCharacter.m_moveDown = m_swipeDown;
                                        m_selectedCharacter.isMoving = true;
                                        m_selectedCharacter.m_timesMoved = 1;
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
                break;
            case false:
                m_handAnimator.SetBool("isActive", false);
                m_rightHand.transform.position = Vector3.MoveTowards(this.transform.position, m_originalPosition, m_handReturnSpeed);
                break;
        }
        
    }

    private void Reset()
    {
        m_startTouch = m_swipeDelta = Vector2.zero;
        m_isDragging = false;
    }
}
