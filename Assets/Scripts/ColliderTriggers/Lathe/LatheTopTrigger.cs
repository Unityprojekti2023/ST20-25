using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheTopTrigger : MonoBehaviour
{
    [Header("References to other scripts")]
    public DrillController drillController;
    
    private void OnTriggerEnter(Collider other)             // Checking for collisions
    {
        if(other.gameObject.tag == "LatheTrigger")          // Making sure we collided with the correct object
        {
            drillController.isLatheAtTopPosition = true;    //Setting "isLatheAtTopPosition" true when the trigger enters the top position
        }
    }

    private void OnTriggerExit(Collider other)              // Checking for collisions
    {
        if(other.gameObject.tag == "LatheTrigger")          // Making sure we collided with the correct object
        {
            drillController.isLatheAtTopPosition = false;   //Setting "isLatheAtTopPosition" false when the trigger exits the top position
        }
    }
}
