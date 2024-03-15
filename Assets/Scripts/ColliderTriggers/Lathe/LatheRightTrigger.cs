using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheRightTrigger : MonoBehaviour
{
    [Header("References to other scripts")]
    public DrillController drillController;
    public MachineScript machineScript;
    public MouseControlPanelInteractable mouseControlPanelInteractable;
    public RayInteractor rayInteractor;
    public LatheSoundFX latheSoundFX;

    [Header("Variables")]
    float fastSpeed = 5f;
    public int counter = 0;
    public bool isDrillMovingAlready = false;

    void Update()
    {
        if(machineScript.moveDrill && !isDrillMovingAlready)            // Checking when its time to move the drill and making sure drill isnt already moving
        {
            drillController.moveDrillLeft = true;                       // Setting "moveDrillLeft" to true, to begin movement to the left
            drillController.moveDrillRight = false;
            drillController.speed = fastSpeed;
            isDrillMovingAlready = true;
        }
    }

    private void OnTriggerEnter(Collider other)                         // Checking for collisions
    {
        if(other.gameObject.tag== "LatheTrigger")                       // Making sure we collided with the correct object
        {
            if(counter < 1)                                             // When counter is less than 1 meaning the drill has not yet completed an animation cycle
            {
                drillController.moveDrillLeft = true;                   // Setting "moveDrillLeft" to true, to begin movement to the left
                drillController.moveDrillRight = false;
                drillController.speed = fastSpeed;
            }

            if(counter <= 1)                                            // Checking if counter is 1 (Meaning the drill has completed 1 full animation cycle)
            {
                machineScript.isMachineActive = false;                  // Resetting different values to reset the system for next animation cycle
                mouseControlPanelInteractable.isLathingActive = false;
                drillController.moveDrillRight = false;
                isDrillMovingAlready = false;
                machineScript.moveDrill = false;
                mouseControlPanelInteractable.isProgramSelected = false;
            }
        }
    }   

    private void OnTriggerExit(Collider other)                          // Checking for collisions
    {
        if(other.gameObject.tag== "LatheTrigger")                       // Making sure we collided with the correct object
        {
            counter++;                                                  //Incrementing the counter each time the trigger leaves the collider
            rayInteractor.scrapPilesThrownIntoCorrectTrashbin = 0;      //Resetting scrapPilesThrownIntoCorrectTrashbin to 0 for next animation cycle
            latheSoundFX.endingClipPlayCounter = 0;                     //Resetting endingClipPlayCounter to 0 for next animation cycle (value used to stop ending clip from playing multiple times)
        }
    }
}
