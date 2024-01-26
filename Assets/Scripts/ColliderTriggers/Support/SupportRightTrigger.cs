using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportRightTrigger : MonoBehaviour
{
    public SupportController supportController;

    private void OnTriggerEnter(Collider other)                     // Checking for collisions
    {
        if(other.gameObject.tag == "SupportTrigger")                // Making sure the trigger collided with the correct object
        {
            supportController.isSupportInRightSpot = true;          // When the support enters the right position, setting "isSupportInRightSpot" to true
            supportController.moveSupportRight = false;             // Setting "moveSupportRight" to false, to stop movement to the right
            supportController.isTheSupportBeingMoved = false;       // Setting "isTheSupportBeingMoved" to false, since the support is no longer moving
        }
    }

    private void OnTriggerExit(Collider other)                      // Checking for collisions
    {
        if(other.gameObject.tag == "SupportTrigger")                // Making sure the trigger collided with the correct object
        {
            supportController.isSupportInRightSpot = false;         // When the support leaves the right position, setting "isSupoportInRightSpot" to false
        }
    }
}
