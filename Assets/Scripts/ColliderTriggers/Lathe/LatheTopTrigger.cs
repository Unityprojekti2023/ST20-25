using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheTopTrigger : MonoBehaviour
{
    public DrillController drillController;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "LatheTrigger")
        {
            drillController.isLatheAtTopPosition = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "LatheTrigger")
        {
            drillController.isLatheAtTopPosition = false;
        }
    }
}
