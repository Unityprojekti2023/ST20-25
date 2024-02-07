using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheMiddleTrigger : MonoBehaviour
{
    [Header("References to other scripts")]
    public DrillController drillController;
    public MachineScript machineScript;
    public LatheSoundFX latheSoundFX;
    public MouseControlPanelInteractable mouseControlPanelInteractable;

    [Header("Variables")]
    float slowSpeed = 1.5f;
    public bool moveUncutObject = false;
    public bool movePartiallyCutObject1 = false;
    public bool movePartiallyCutObject2 = false;
    public bool movePartiallyCutObject3 = false;
    public bool movePartiallyCutObject4 = false;
    public bool movePartiallyCutObject5 = false;

    private void OnTriggerEnter(Collider other)                                             // Checking for collisions
    {
        if(other.gameObject.tag == "LatheTrigger")                                          // Making sure we collided with the correct object                             
        {
            latheSoundFX.isBeginningClipPlaying = false;

            if(drillController.activeCounter < drillController.animation1Counter)           // Checking if activeCounter is less than animationCounter (Meaning that the drill hasnt done enough loops yet)
            {
                drillController.moveDrillLeft = true;                                       // Setting "moveDrillLeft" to true to begin movement to the left
                drillController.moveDrillRight = false;
                drillController.speed = slowSpeed;                                          // Slowing the drill´s movement speed

                latheSoundFX.playLathingClip = true;                                        // Playing lathing audio clip, and stopping idling audio clip
                latheSoundFX.playIdlingclip = false;

                latheSoundFX.isIdlingClipPlaying = false;
            }

            if(drillController.activeCounter == drillController.animation1Counter)          // Checking when activeCounter matches animationCounter (meaning the drill has done enough loops and can return to starting location)
            {   
                latheSoundFX.playEndingClip = true;                                         // Playing ending audio clip and stopping idling audio clip
                latheSoundFX.playIdlingclip = false;

                latheSoundFX.isIdlingClipPlaying = false;

                mouseControlPanelInteractable.isLathingActive = false;
                machineScript.isMachineActive = false;
                machineScript.isAnimationComplete = true;
            }

            switch(drillController.activeCounter)                                           // Switch cases for each activeCounter value (each loop the drill takes between LatheLeftTrigger and LatheRightTrigger)
            {
                case 0:
                    moveUncutObject = true;
                    break;

                case 1:
                    movePartiallyCutObject1 = true;
                    break;

                case 2:
                    movePartiallyCutObject2 = true;
                    break;

                case 3:
                    movePartiallyCutObject3 = true;
                    break;

                case 4:
                    movePartiallyCutObject4 = true;
                    break;
                
                case 5:
                    movePartiallyCutObject5 = true;
                    break;
            }
        }
    } 
}