using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTransformController : MonoBehaviour
{
    [SerializeField] private GameManager m_gameManager;

    public bool isTaken = false;

    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (isTaken)
        {
            case true:
                m_gameManager.m_playerSide.Remove(this.gameObject.transform);
                break;
            case false:
                break;
        }
    }
}
