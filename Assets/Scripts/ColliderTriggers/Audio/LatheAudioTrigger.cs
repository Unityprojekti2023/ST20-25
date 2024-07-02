using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheAudioTrigger : MonoBehaviour
{
    [Header("References to other scripts")]
    public ControlPanelInteractable mouseControlPanelInteractable;
    public LatheSoundFX latheSoundFX;

    [Header("Other values")]
    public int counter = 0;

    private void OnTriggerEnter(Collider other)                                 //Checking for collisions
    {
        if (other.gameObject.tag == "AudioTrigger")                             //Making sure we collided with the correct object
        {
            counter++;

            if (counter%2 == 0)                                                 //Checking if counter is even or odd
            {
                mouseControlPanelInteractable.isLathingActive = false;          //When counter is even, lathe is returning back to starting position = Setting "isLathingActive" to fals
            } else {
                latheSoundFX.playBeginningClip = true;                          //When counter is odd, lathe is starting its animation = Setting "playBeginningClip" to true
            }                                                                   //so that the beginning audio clip will be played
        }
    } 

    private void OnTriggerExit(Collider other)                                  //Checking for when the object leaves the collider
    {
        latheSoundFX.playBeginningClip = false;                                 //Setting "playBeginningClip" back to false 
    }
}