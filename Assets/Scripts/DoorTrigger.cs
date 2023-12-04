using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [Header("Boolean Variables")]
    public bool isPlayerNearDoor = false;

    private void OnTriggerEnter(Collider other)     // This function is called when a collider is hit and "enters" another collider
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerNearDoor = true;                 // If the other collider's tag is "Player", setting "isPlayerNearDoor" to true.
        }
    }

    private void OnTriggerExit(Collider other)       // This function is called after a collider "exits" another collider.
    {
        if (other.gameObject.tag == "Player") 
        {
            isPlayerNearDoor = false;                // If the other collider's tag is "Player", setting "isPlayerNearDoor" to false
        }
    }
}
