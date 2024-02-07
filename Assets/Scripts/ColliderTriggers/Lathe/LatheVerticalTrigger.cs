using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheVerticalTrigger : MonoBehaviour
{
    public DrillController drillController;
    public int counter = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "LatheTrigger")
        {
            counter++;

            if(counter%2 == 0)
            {
                
            } else {
                drillController.moveLatheDown = true;
                drillController.moveLatheUp = false;
            }
        }
    }
}
