using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheBottomTrigger : MonoBehaviour
{
    [Header("References to other scripts")]
    public DrillController drillController;

    private void OnTriggerEnter(Collider other)                     //Checking for collisions
    {
        if(other.gameObject.tag == "LatheTrigger")                  //Making sure we collided with the correct object
        {
            drillController.isLatheAtBottomPosition = true;
        }
    }

    private void OnTriggerExit(Collider other)                      //Checking for collisions
    {
        if(other.gameObject.tag == "LatheTrigger")                  //Making sure we collided with the correct object
        {
            drillController.isLatheAtBottomPosition = false;
        }
    }
}
