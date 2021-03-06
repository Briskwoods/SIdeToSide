using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCharacterController : MonoBehaviour
{
    //[SerializeField] private Animator m_selfAnimator;

    //[SerializeField] private Rigidbody m_rigidbody;

    //public bool isCentre = true;
    //public bool isRight = false;
    //public bool isLeft = false;

    //public bool isFromLeft = false;
    //public bool isFromRight = false;

    public int m_timesClickedOn = 0; 

    [SerializeField] private Rigidbody m_self;

    [SerializeField] private Animator m_selfAnim;

    [SerializeField] private GameManager m_gameManager;

    [SerializeField] private GameObject m_PredictionTiles;

    [SerializeField] private RayCastDirectionManager m_raycastManager;

    [SerializeField] private bool playerTurn;
    [SerializeField] private bool enemyTurn;

    [SerializeField] private Color m_outlineColour;

    public int m_timesMoved = 0;
    public int m_runTrigger = 0;

    //[SerializeField] private float m_horizontalSpeed = 45f;
    [SerializeField] private float m_speed = 5f;
    //[SerializeField] private float m_horizontalMoveDistance = 1.15f;
    //[SerializeField] private float m_verticalMoveDistance = 1.5f;
    [SerializeField] private float m_moveTime = 5f;
    [SerializeField] private float m_originalMoveTime;

    public Transform m_up;
    public Transform m_down;
    public Transform m_left;
    public Transform m_right;

    public bool isSelected =  false;
    public bool isMatched =  false;

    public bool m_moveUp = false;
    public bool m_moveDown = false;
    public bool m_moveRight = false;
    public bool m_moveLeft = false;

    public bool b_canMoveLeft = false;
    public bool b_canMoveRight = false;
    public bool b_canMoveUp = false;
    public bool b_canMoveDown = false;

    public bool isMoving = false;



    // Start is called before the first frame update
    void Start()
    {
        m_self = this.GetComponent<Rigidbody>();
        m_selfAnim = this.GetComponent<Animator>();
        m_gameManager = FindObjectOfType<GameManager>();
        m_PredictionTiles = GetComponentInChildren<EnablePredictionTiles>().gameObject;
        m_originalMoveTime = m_moveTime;
        m_PredictionTiles.SetActive(true);
        m_raycastManager = this.gameObject.GetComponent<RayCastDirectionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerTurn = m_gameManager.m_playerTurn;
        enemyTurn = m_gameManager.m_enemyTurn;

        switch (isSelected && !isMoving)
        {
            case true:
                GetComponentInChildren<Outline>().enabled = true;
                m_PredictionTiles.SetActive(true);
                break;
            case false:
                GetComponentInChildren<Outline>().enabled = false;
                m_PredictionTiles.SetActive(false);
                break;
        }

        switch (m_moveLeft && b_canMoveLeft)
        {
            case true:
                //m_self.MovePosition(transform.position + new Vector3(-m_horizontalMoveDistance, 0, 0) * m_horizontalSpeed * Time.deltaTime);
                //m_self.MovePosition(m_left.position);
                m_self.MovePosition(Vector3.Lerp(m_self.transform.position, m_left.position + new Vector3(0, 15, 0), m_speed * Time.deltaTime));
                m_self.MoveRotation(Quaternion.Euler(0,270,0));
                break;
            case false:
                break;
        }

        switch (m_moveRight && b_canMoveRight)
        {
            case true:
                //m_self.MovePosition(transform.position + new Vector3(m_horizontalMoveDistance, 0, 0) * m_horizontalSpeed * Time.deltaTime);
                //m_self.MovePosition(m_right.position);
                m_self.MovePosition(Vector3.Lerp(m_self.transform.position, m_right.position + new Vector3(0, 15, 0), m_speed * Time.deltaTime));
                m_self.MoveRotation(Quaternion.Euler(0,90,0));
                break;
            case false:
                break;
        }

        switch (m_moveUp && b_canMoveUp)
        {
            case true:
                //m_self.MovePosition(transform.position + new Vector3(0, 0, m_verticalMoveDistance) * m_verticalSpeed * Time.deltaTime);
                m_self.MovePosition(Vector3.Lerp(m_self.transform.position, m_up.position + new Vector3(0, 15, 0), m_speed * Time.deltaTime));
                m_self.MoveRotation(Quaternion.Euler(0,0,0));
                break;
            case false:
                break;
        }

        switch (m_moveDown && b_canMoveDown)
        {
            case true:
                //m_self.MovePosition(transform.position + new Vector3(0, 0, -m_verticalMoveDistance) * m_verticalSpeed * Time.deltaTime);
                //m_self.MovePosition(m_down.position);
                m_self.MovePosition(Vector3.Lerp(m_self.transform.position, m_down.position + new Vector3(0,15,0), m_speed * Time.deltaTime));
                m_self.MoveRotation(Quaternion.Euler(0,180,0));
                break;
            case false:
                break;
        }

        switch (m_timesMoved > 0)
        {
            case true:
                switch (playerTurn)
                {
                    case true:
                        m_gameManager.wasPlayerTurn = true;
                        m_gameManager.wasEnemyTurn = false;
                        m_gameManager.m_playerTurn = false;
                        m_gameManager.isSwitching = true;
                        m_timesMoved = 0;
                        m_gameManager.smokeCounter = 0;
                        isSelected = false;
                        break;
                    case false:
                        m_gameManager.wasPlayerTurn = false;
                        m_gameManager.wasEnemyTurn = true;
                        m_gameManager.m_playerTurn = true;
                        m_gameManager.isSwitching = true;
                        m_timesMoved = 0;
                        m_gameManager.smokeCounter = 0;
                        isSelected = false;
                        break;
                }
                break;
            case false:
                break;
        }

    }

    private void FixedUpdate()
    {
        switch (isMoving)
        {
            case true:                 
                m_moveTime -= Time.deltaTime;
                m_selfAnim.SetBool("isMoving", isMoving);
                
                m_raycastManager.m_upDetectCount = 0;
                m_raycastManager.m_downDetectCount = 0;
                m_raycastManager.m_leftDetectCount = 0;
                m_raycastManager.m_rightDetectCount = 0;
                break;
            case false:
                m_moveTime = m_originalMoveTime;
                m_selfAnim.SetBool("isMoving", isMoving);
                break;
        }

        switch(m_moveTime <= 0)
        {
            case true:
                m_moveLeft = m_moveRight = m_moveUp = m_moveDown = false;
                isMoving = false;
                m_self.MoveRotation(Quaternion.Euler(0, 180, 0));
                break;
                case false: break;
        }

        switch (isMatched)
        {
            case true: 
                break;
            case false: break;
        }
    }
}
