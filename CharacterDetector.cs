using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDetector : MonoBehaviour
{
    [SerializeField] private BoardCharacterController m_self;
    [SerializeField] private BoardCharacterController m_matched;
    [SerializeField] private GameManager m_gameManager;

    public SkinnedMeshRenderer m_myMesh;
    public SkinnedMeshRenderer m_detectedMesh;

    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = FindObjectOfType<GameManager>();
        m_self = this.GetComponentInParent<BoardCharacterController>();
        m_myMesh = this.gameObject.transform.parent.GetComponentInChildren<SkinnedMeshRenderer>();

    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag == "BoardCharacter")
        {
            case true:
                m_matched = other.gameObject.GetComponent<BoardCharacterController>();
                m_detectedMesh = other.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();

                switch (m_myMesh.materials[0].name == m_detectedMesh.materials[0].name)
                {
                    case true:
                        m_gameManager.isMatched = true;
                        m_matched.isMatched = true;
                        switch (!m_gameManager.m_matchedBoardCharacters.Contains(m_self))
                        {
                            case true:
                                m_gameManager.m_matchedBoardCharacters.Add(m_self);
                                break;
                            case false:
                                break;
                        }
                        switch(!m_gameManager.m_matchedBoardCharacters.Contains(m_matched)){
                            case true:
                                m_gameManager.m_matchedBoardCharacters.Add(m_matched);
                                break;
                            case false:
                                break;
                        }
                        m_matched.isMatched = false;
                        m_matched = null;
                        break;
                    case false: break;
                }
                break;
            case false:
                break;
        }
    }
}
