using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheAudioTrigger : MonoBehaviour
{
    [Header("References to other scripts")]
    public MouseControlPanelInteractable mouseControlPanelInteractable;
    public LatheSoundFX latheSoundFX;

    [Header("Other values")]
    public int counter = 0;

    private void OnTriggerEnter(Collider other)                                 // Checking when this trigger collides with the audio collider
    {
        if (other.gameObject.tag == "AudioTrigger")
        {
            counter++;                                                          // Incrementing the counter

            if (counter%2 == 0) 
            {
                mouseControlPanelInteractable.isLathingActive = false;          // Setting "isLathingActive" to false when the lathe is returning back to starting position
            } else {
                latheSoundFX.playBeginningClip = true;                          // Setting "playBeginningClip" to true so that the beginning audio clip will be played
            }
        }
    } 

    private void OnTriggerExit(Collider other) {
        latheSoundFX.playBeginningClip = false;                                 // When the trigger leaves the collider setting "playBeginningClip" back to false 
    }
}