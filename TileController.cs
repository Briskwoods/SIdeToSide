using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public bool isTaken;

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag == "BoardCharacter")
        {
            case true:
                isTaken = true;
                break;
            case false:
                isTaken = false;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag == "BoardCharacter")
        {
            case true:
                isTaken = false;
                break;
            case false:
                break;
        }
    }
}
