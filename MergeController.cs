using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeController : MonoBehaviour
{
    public bool merge = false;

    public float maxSize;
    public float growFactor;
    public float waitTime;

    public List<GameObject> m_characters;

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag  == "BoardCharacter")
        {
            case true:
                switch (!m_characters.Contains(other.gameObject))
                {
                    case true:
                        m_characters.Add(other.gameObject);
                        break;
                    case false: break;
                }
                break;
            case false: break;
        }
    }

    private void Update()
    {
        switch (m_characters.Count != 0)
        {
            case true:
                switch (merge)
                {
                    case true:
                        m_characters[0].SetActive(false);
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
        StartCoroutine(Scale());
    }

    IEnumerator Scale()
    {
        switch (m_characters.Count != 0)
        {
            case true:
                float timer = 0;


                while (merge) // this could also be a condition indicating "alive or dead"
                {
                    // we scale all axis, so they will have the same value, 
                    // so we can work with a float instead of comparing vectors
                    while (maxSize > m_characters[1].transform.localScale.x)
                    {
                        timer += Time.deltaTime;
                        m_characters[1].transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;

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
}
