using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheRightTrigger : MonoBehaviour
{
    [Header("References to other scripts")]
    public DrillController drillController;
    public MachineScript machineScript;
    public MouseControlPanelInteractable mouseControlPanelInteractable;

    float fastSpeed = 5f;
    public int counter = 0;
    public bool isDrillMovingAlready = false;

    void Update()
    {
        if(machineScript.moveDrill && !isDrillMovingAlready)                        // Checking when its time to move the drill and making sure drill isnt already moving
        {
            drillController.moveDrillLeft = true;                                   // Setting "moveDrillLeft" to true, to begin movement to the left
            drillController.moveDrillRight = false;
            drillController.speed = fastSpeed;                                      // Setting the drill´s movement speed to "fast"
            isDrillMovingAlready = true;
        }
    }
    
    private void OnTriggerEnter(Collider other)                                     // Checking for collisions
    {
        if(other.gameObject.tag== "LatheTrigger")                                   // Making sure the trigger collided with the correct object
        {
            if(counter < 1) {                                                       // Checking if counter is less than 1
                drillController.moveDrillLeft = true;                               // Setting "moveDrillLeft" to true, to begin movement to the left
                drillController.moveDrillRight = false;
                drillController.speed = fastSpeed;                                  // Setting the drill´s movement speed to "fast"
            }

            if(counter <= 1)                                                        // Checking if counter is 1 (Meaning the drill has completed 1 full animation cycle)
            {
                machineScript.isMachineActive = false;                              // Resetting different values to "reset" the system for next animation cycle
                mouseControlPanelInteractable.isLathingActive = false;
                drillController.moveDrillRight = false;
                isDrillMovingAlready = false;
                machineScript.moveDrill = false;
            }
        }
    }   

    private void OnTriggerExit(Collider other)                                      // Checking for collisions
    {
        if(other.gameObject.tag== "LatheTrigger")                                   // Making sure trigger collided with the correct object
        {
            counter++;                                                              // Incrementing the counter
        }
    }
}
