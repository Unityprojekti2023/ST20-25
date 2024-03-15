using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheVerticalTrigger : MonoBehaviour
{
    [Header("References to other scripts")]
    public DrillController drillController;

    [Header("Variables")]
    public int counter = 0;

    private void OnTriggerEnter(Collider other)         // Checking for collisions
    {
        if(other.gameObject.tag == "LatheTrigger")      // Making sure we collided with the correct object
        {
            counter++;

            if(counter%2 == 0)
            {                                           //When counter is even doing nothing
            } else {                                    //When counter is odd, starting movement downwards
                drillController.moveLatheDown = true;
                drillController.moveLatheUp = false;
            }
        }
    }
}
