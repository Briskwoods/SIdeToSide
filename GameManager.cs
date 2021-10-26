using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Turn Controller Variables
    public HandController m_playerHand;
    public HandController m_enemyHand;

    public bool m_playerTurn;
    public bool m_enemyTurn;

    public bool wasPlayerTurn = false;
    public bool wasEnemyTurn = false;

    public bool isSwitching = false;
    public bool isMatched = false;

    [SerializeField] private float m_moveTime = 1f;
    [SerializeField] private float m_originalMoveTime;
    [SerializeField] private float m_matchedTime = 5f;

    public List<BoardCharacterController> m_matchedBoardCharacters;
    public List<Transform> m_playerSide;
    public List<Transform> m_enemySide;

    // Start is called before the first frame update
    void Start()
    {
        m_playerTurn = true;
        m_playerHand.m_isActive = m_playerTurn;
        m_originalMoveTime = m_moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        switch (!m_playerTurn)
        {
            case true:
                switch (m_moveTime <= 0)
                {
                    case true:
                        isSwitching = false;
                        m_enemyTurn = true;
                        m_playerHand.m_isActive = false;
                        m_enemyHand.m_isActive = true;
                        m_moveTime = m_originalMoveTime;
                        wasPlayerTurn = false;
                        wasEnemyTurn = false;
                        break;
                    case false: break;
                }
                break;
            case false:
                switch (m_moveTime <= 0)
                {
                    case true:
                        isSwitching = false;
                        m_enemyTurn = false;
                        m_playerHand.m_isActive = true;
                        m_enemyHand.m_isActive = false;
                        m_moveTime = m_originalMoveTime;
                        wasPlayerTurn = false;
                        wasEnemyTurn = false;
                        break;
                    case false: break;
                }
                break;
        }

        switch (isSwitching)
        {
            case true:
                m_moveTime -= Time.deltaTime;
                m_playerHand.m_isActive = false;
                m_enemyHand.m_isActive = false;
                break;
            case false: break;
        }

        switch (isMatched)
        {
            case true:
                isMatched = false;
                m_moveTime = m_matchedTime;
                m_moveTime -= Time.deltaTime;
                switch (m_moveTime <= 0)
                {
                    case true:
                        m_moveTime = m_originalMoveTime;
                        break;
                    case false: break;
                }
                break;
            case false: break;
        }

        switch(m_matchedBoardCharacters.Count != 0)
        {
            case true:
                switch (wasPlayerTurn)
                {
                    case true:
                        //m_moveTime = m_timeBefroreSlerp;
                        //m_moveTime -= Time.deltaTime;
                        switch(m_moveTime <= 0)
                        {
                            case true:
                                //m_matchedBoardCharacters[0].gameObject.GetComponent<Animator>().SetBool("isMoving", true);
                                m_matchedBoardCharacters[0].gameObject.GetComponent<SlerpScript>().sunrise = m_matchedBoardCharacters[0].transform;
                                m_matchedBoardCharacters[0].gameObject.GetComponent<SlerpScript>().sunset = m_playerSide[0].transform;
                                m_matchedBoardCharacters[0].gameObject.GetComponent<SlerpScript>().enabled = true;
                                m_matchedBoardCharacters[0].gameObject.GetComponent<BoardCharacterController>().isSelected = false;
                                m_matchedBoardCharacters[0].gameObject.GetComponent<BoardCharacterController>().enabled = false;
                                m_matchedBoardCharacters[0].gameObject.GetComponent<DetectorController>().disable = true;
                                //m_matchedBoardCharacters[0].gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                                //m_matchedBoardCharacters[1].gameObject.GetComponent<Animator>().SetBool("isMoving", true);
                                m_matchedBoardCharacters[1].gameObject.GetComponent<SlerpScript>().sunrise = m_matchedBoardCharacters[1].transform;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<SlerpScript>().sunset = m_playerSide[1].transform;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<SlerpScript>().enabled = true;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<BoardCharacterController>().isSelected = false;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<BoardCharacterController>().enabled = false;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<DetectorController>().disable = true;
                                //m_matchedBoardCharacters[1].gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                                m_matchedBoardCharacters.RemoveAt(0);
                                m_playerSide.RemoveAt(0);
                                m_matchedBoardCharacters.RemoveAt(0);
                                m_playerSide.RemoveAt(0);
                                m_moveTime = m_matchedTime;
                                break;
                            case false: break;
                        }
                        break;
                    case false: break;
                }

                switch (wasEnemyTurn)
                {
                    case true:
                        //m_moveTime = m_timeBefroreSlerp;
                        //m_moveTime -= Time.deltaTime;
                        switch (m_moveTime <= 0)
                        {
                            case true:
                                //m_matchedBoardCharacters[0].gameObject.GetComponent<Animator>().SetBool("isMoving", true);
                                m_matchedBoardCharacters[0].gameObject.GetComponent<SlerpScript>().sunrise = m_matchedBoardCharacters[0].transform;
                                m_matchedBoardCharacters[0].gameObject.GetComponent<SlerpScript>().sunset = m_enemySide[0].transform;
                                m_matchedBoardCharacters[0].gameObject.GetComponent<SlerpScript>().enabled = true;
                                m_matchedBoardCharacters[0].gameObject.GetComponent<BoardCharacterController>().isSelected = false;
                                m_matchedBoardCharacters[0].gameObject.GetComponent<BoardCharacterController>().enabled = false;
                                m_matchedBoardCharacters[0].gameObject.GetComponent<DetectorController>().disable = true;
                                //m_matchedBoardCharacters[0].gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                                //m_matchedBoardCharacters[1].gameObject.GetComponent<Animator>().SetBool("isMoving", true);
                                m_matchedBoardCharacters[1].gameObject.GetComponent<SlerpScript>().sunrise = m_matchedBoardCharacters[1].transform;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<SlerpScript>().sunset = m_enemySide[1].transform;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<SlerpScript>().enabled = true;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<BoardCharacterController>().isSelected = false;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<BoardCharacterController>().enabled = false;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<DetectorController>().disable = true;
                                //m_matchedBoardCharacters[1].gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                                m_matchedBoardCharacters.RemoveAt(0);
                                m_enemySide.RemoveAt(0);
                                m_matchedBoardCharacters.RemoveAt(0);
                                m_enemySide.RemoveAt(0);
                                m_moveTime = m_matchedTime;
                                break;
                            case false: break;
                        }
                        //m_enemySide[0].gameObject.GetComponent<TranformController>().isTaken = true;
                        break;
                    case false: break;
                }

                //switch (wasEnemyTurn)
                //{
                //    case true:
                //        foreach (BoardCharacterController matchedCharacter in m_matchedBoardCharacters)
                //        {
                //            matchedCharacter.transform.position = Vector3.Slerp(matchedCharacter.transform.position, m_playerSide[0].transform.position, 1);
                //            m_enemySide[0].transform.GetComponent<TranformController>().isTaken = true;
                //            //wasEnemyTurn = false;
                //            m_matchedBoardCharacters.Remove(matchedCharacter);
                //        }
                //        break;
                //    case false: break;
                //}

                break;
            case false: break;
        }
       
    }
}
