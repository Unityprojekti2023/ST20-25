using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheLeftTrigger : MonoBehaviour
{
    [Header("References to other scripts")]
    public DrillController drillController;
    public LatheMiddleTrigger latheMiddleTrigger;
    public LatheSoundFX latheSoundFX;
    

    [Header("Other values")]
    public bool isLatheAtLeftPosition = false;

    private void OnTriggerEnter(Collider other)                                 // Checking for collisions
    {
        if(other.gameObject.tag== "LatheTrigger")                               // Making sure the trigger collided with the correct object
        {
            drillController.moveDrillLeft = false;                              // Setting "moveDrillLeft" to false to stop movement to the left
            drillController.moveDrillRight = true;                              // Setting "movDrillRight" to true to start movement to the right

            latheSoundFX.playLathingClip = false;                               // Setting "playLathingClip" to false to stop lathing audio clip from playing
            latheSoundFX.playIdlingclip = true;                                 // Setting "playIdlingClip" to true to start idling audio clip

            latheSoundFX.isLathingClipPlaying = false;

            switch(drillController.selectedProgram)
            {
                case 0: // Program #0 Case
                    switch(drillController.activeCounter)                               // Switch cases for each activeCounter value (each loop the drill takes between LatheLeftTrigger and LatheMiddleTrigger)
                    {
                        case 0:
                            latheMiddleTrigger.moveUncutObject = false;
                        break;

                        case 1:
                            latheMiddleTrigger.movePartiallyCutObject1 = false;
                        break;

                        case 2:
                            latheMiddleTrigger.movePartiallyCutObject2 = false;
                        break;

                        case 3:
                            latheMiddleTrigger.movePartiallyCutObject3 = false;
                        break;

                        case 4:
                            latheMiddleTrigger.movePartiallyCutObject4 = false;
                        break;

                        case 5:
                            latheMiddleTrigger.movePartiallyCutObject5 = false;
                            drillController.moveLatheUp = true;
                            drillController.moveLatheDown = false;
                        break;
                    }
                break;

                case 1: // Program #1 Case
                    switch(drillController.activeCounter)
                    {
                        case 0:
                            latheMiddleTrigger.moveUncutObject = false;
                        break;

                        case 1:
                            latheMiddleTrigger.movePartiallyCutObject1 = false;
                        break;

                        case 2:
                            latheMiddleTrigger.movePartiallyCutObject3 = false;
                        break;

                        case 3:
                            latheMiddleTrigger.movePartiallyCutObject5 = false;
                            drillController.moveLatheUp = true;
                            drillController.moveLatheDown = false;
                        break;
                    }
                break;
            }

            switch(drillController.activeCounter)                               // Switch cases for each activeCounter value (each loop the drill takes between LatheLeftTrigger and LatheMiddleTrigger)
            {
                case 0:
                    latheMiddleTrigger.moveUncutObject = false;
                    break;

                case 1:
                    latheMiddleTrigger.movePartiallyCutObject1 = false;
                    break;

                case 2:
                    latheMiddleTrigger.movePartiallyCutObject2 = false;
                    break;

                case 3:
                    latheMiddleTrigger.movePartiallyCutObject3 = false;
                    break;

                case 4:
                    latheMiddleTrigger.movePartiallyCutObject4 = false;
                    break;

                case 5:
                    latheMiddleTrigger.movePartiallyCutObject5 = false;
                    drillController.moveLatheUp = true;
                    drillController.moveLatheDown = false;
                    break;
            }

            drillController.activeCounter++;                                    // Incrementing activeCounter every time the lathe enters the left trigger
        }
    }   
}
