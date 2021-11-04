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
    [SerializeField] private float m_mergeTime = 1f;

    public List<BoardCharacterController> m_matchedBoardCharacters;
    public List<BoardCharacterController> m_charactersToMerge;
    public List<Transform> m_playerSide;
    public List<Transform> m_enemySide;


    public bool merge = false;

    public float maxSize;
    public float growFactor;
    public float waitTime;


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
                        switch(m_moveTime <= 0)
                        {
                            case true:
                                m_matchedBoardCharacters[0].gameObject.GetComponent<SlerpScript>().sunrise = m_matchedBoardCharacters[0].transform;
                                m_matchedBoardCharacters[0].gameObject.GetComponent<SlerpScript>().sunset = m_playerSide[0].transform;
                                m_matchedBoardCharacters[0].gameObject.GetComponent<SlerpScript>().enabled = true;
                                m_matchedBoardCharacters[0].gameObject.GetComponent<BoardCharacterController>().isSelected = false;
                                m_matchedBoardCharacters[0].gameObject.GetComponent<BoardCharacterController>().enabled = false;
                                m_matchedBoardCharacters[0].gameObject.GetComponent<DetectorController>().disable = true;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<SlerpScript>().sunrise = m_matchedBoardCharacters[1].transform;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<SlerpScript>().sunset = m_playerSide[1].transform;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<SlerpScript>().enabled = true;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<BoardCharacterController>().isSelected = false;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<BoardCharacterController>().enabled = false;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<DetectorController>().disable = true;
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
                        switch (m_moveTime <= 0)
                        {
                            case true:
                                m_matchedBoardCharacters[0].gameObject.GetComponent<SlerpScript>().sunrise = m_matchedBoardCharacters[0].transform;
                                m_matchedBoardCharacters[0].gameObject.GetComponent<SlerpScript>().sunset = m_enemySide[0].transform;
                                m_matchedBoardCharacters[0].gameObject.GetComponent<SlerpScript>().enabled = true;
                                m_matchedBoardCharacters[0].gameObject.GetComponent<BoardCharacterController>().isSelected = false;
                                m_matchedBoardCharacters[0].gameObject.GetComponent<BoardCharacterController>().enabled = false;
                                m_matchedBoardCharacters[0].gameObject.GetComponent<DetectorController>().disable = true;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<SlerpScript>().sunrise = m_matchedBoardCharacters[1].transform;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<SlerpScript>().sunset = m_enemySide[1].transform;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<SlerpScript>().enabled = true;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<BoardCharacterController>().isSelected = false;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<BoardCharacterController>().enabled = false;
                                m_matchedBoardCharacters[1].gameObject.GetComponent<DetectorController>().disable = true;
                                m_matchedBoardCharacters.RemoveAt(0);
                                m_enemySide.RemoveAt(0);
                                m_matchedBoardCharacters.RemoveAt(0);
                                m_enemySide.RemoveAt(0);
                                m_moveTime = m_matchedTime;
                                break;
                            case false: break;
                        }
                        break;
                    case false: break;
                }

                // Merge Controller values
                switch (!m_charactersToMerge.Contains(m_matchedBoardCharacters[0]) && !m_charactersToMerge.Contains(m_matchedBoardCharacters[1]))
                {
                    case true:
                        m_charactersToMerge.Add(m_matchedBoardCharacters[0]);
                        m_charactersToMerge.Add(m_matchedBoardCharacters[1]);
                        break;
                    case false:
                        break;
                }
                break;
            case false: break;
        }

        switch (merge)
        {
            case true:
                switch (m_charactersToMerge.Count != 0 && !m_charactersToMerge[0].isMoving && !m_charactersToMerge[1].isMoving)
                {
                    case true:
                        Merge();
                        break;
                    case false:
                        break;
                }

                break;
            case false:
                break;
        }

    }

    public void Merge()
    {
        StartCoroutine(MergeCoroutine());
        
    }

    IEnumerator Scale()
    {
            switch (m_matchedBoardCharacters.Count != 0)
        {
            case true:
                float timer = 0;


                while (merge) // this could also be a condition indicating "alive or dead"
                {
                    // we scale all axis, so they will have the same value, 
                    // so we can work with a float instead of comparing vectors
                    while (maxSize > m_matchedBoardCharacters[1].transform.localScale.x)
                    {
                        timer += Time.deltaTime;
                        m_matchedBoardCharacters[1].transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;

                        yield return null;
                    }
                    // reset the timer


                    yield return new WaitForSeconds(waitTime);

                    timer = 0;
                    while (1 < transform.localScale.x)
                    {
                        timer += Time.deltaTime;
                        transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * growFactor;

                        yield return null;
                    }

                    timer = 0;
                    yield return new WaitForSeconds(waitTime);
                }
                break;
            case false:
                break;
        }
    }

    IEnumerator MergeCoroutine()
    {
        switch (m_charactersToMerge.Count != 0)
        {
            case true:
                m_charactersToMerge[1].gameObject.transform.LookAt(m_matchedBoardCharacters[0].transform.position);
                m_charactersToMerge[0].gameObject.transform.LookAt(m_matchedBoardCharacters[1].transform.position);
                StartCoroutine(Scale());
                m_charactersToMerge[1].gameObject.GetComponent<Animator>().SetBool("isMoving", true);
                m_charactersToMerge[0].gameObject.GetComponent<Animator>().SetBool("isMoving", true);
                m_charactersToMerge[1].gameObject.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(m_matchedBoardCharacters[1].transform.position, m_matchedBoardCharacters[0].transform.position, 3 * Time.deltaTime));
                m_charactersToMerge[0].gameObject.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(m_matchedBoardCharacters[0].transform.position, m_matchedBoardCharacters[1].transform.position, 3 * Time.deltaTime));
                yield return new WaitForSeconds(0.5f);

                m_charactersToMerge[1].gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                m_charactersToMerge[0].gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                m_charactersToMerge[0].gameObject.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                switch (wasPlayerTurn) 
                {
                    case true:
                        m_charactersToMerge[1].gameObject.GetComponent<Animator>().SetBool("isMoving", true);
                        m_charactersToMerge[1].gameObject.transform.LookAt(m_playerSide[0].transform);
                        yield return new WaitForSeconds(1f);
                        m_charactersToMerge[1].gameObject.GetComponent<Rigidbody>().MoveRotation(Quaternion.Lerp(Quaternion.Euler(m_charactersToMerge[1].transform.eulerAngles), Quaternion.Euler(0, 180, 0), 1));
                        m_charactersToMerge[1].gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                        break;
                    case false:
                        break;
                }

                switch (wasEnemyTurn)
                {
                    case true:
                        m_charactersToMerge[1].gameObject.GetComponent<Animator>().SetBool("isMoving", true);
                        m_charactersToMerge[1].gameObject.transform.LookAt(m_enemySide[0].transform);
                        yield return new WaitForSeconds(1f);
                        m_charactersToMerge[1].gameObject.GetComponent<Rigidbody>().MoveRotation(Quaternion.Lerp(Quaternion.Euler(m_charactersToMerge[1].transform.eulerAngles),Quaternion.Euler(0, 180, 0),1));
                        m_charactersToMerge[1].gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                        break;
                    case false:
                        break;
                }

                //m_charactersToMerge[0].gameObject.GetComponent<Animator>().SetBool("isMoving", false);
               
                //yield return new WaitForSeconds(0.8f);

                m_charactersToMerge.Remove(m_charactersToMerge[0]);
                m_charactersToMerge.Remove(m_charactersToMerge[0]);
                //StopCoroutine(MergeCoroutine());
                break;
            case false:
                break;
        }
    }
}
