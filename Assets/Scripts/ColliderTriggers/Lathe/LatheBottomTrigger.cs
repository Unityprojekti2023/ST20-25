using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheBottomTrigger : MonoBehaviour
{
    public DrillController drillController;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "LatheTrigger")
        {
            drillController.isLatheAtBottomPosition = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "LatheTrigger")
        {
            drillController.isLatheAtBottomPosition = false;
        }
    }
}
