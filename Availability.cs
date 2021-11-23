using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Availability : MonoBehaviour
{
    public bool SlotAvailable = true;
  

    public void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "BoardCharacter")
        {
            SlotAvailable = false;
        }
    }


    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "BoardCharacter")
        {
            SlotAvailable = true;
        }
    }
}
