using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportLeftTrigger : MonoBehaviour
{
    public SupportController supportController;

    private void OnTriggerEnter(Collider other)                     // Checking for collisions
    {
        if(other.gameObject.tag == "SupportTrigger")                // Making sure we collided with the correct object
        {
            supportController.isSupportInLeftSpot = true;           // When the support enters the left position, setting "isSupportInLeftSpot" to true
            supportController.moveSupportLeft = false;              // Setting "moveSupportLeft" to false, to stop movement to the left
            supportController.isTheSupportBeingMoved = false;       // Setting "isTheSupportBeingMoved" to false since the support is no longer moving
        }
    }

    private void OnTriggerExit(Collider other)                      // Checking for collisions
    {
        if(other.gameObject.tag == "SupportTrigger")                // Making sure we collided with the correct object
        {
            supportController.isSupportInLeftSpot = false;          // When the support leaves the left position, setting "isSupportInLeftSpot" to false
        }
    }
}
