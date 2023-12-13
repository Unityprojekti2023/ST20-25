using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheAudioTrigger : MonoBehaviour
{
    [Header("References to other scripts")]
    public MouseControlPanelInteractable mouseControlPanelInteractable;

    [Header("Other values")]
    public int counter = 0;
    public bool playAudioClip = false;

    //Checking when a trigger in the drill collides with this static collider
    private void OnTriggerEnter(Collider other)
    {
        //Making sure we collided with the correct collider
        if (other.gameObject.tag == "AudioTrigger")
        {
            //Increasing the collider counter (how many times have the objects collided)
            counter++; 
        }

        //Checking if the counter is even or odd
        if (counter%2 == 0) 
        {
            //When the counter is even, we dont want to play the audio
            playAudioClip = false;
            mouseControlPanelInteractable.isLathingActive = false;      
        } else 
        {
            //When the counter is odd, we want to play the audio 
            playAudioClip = true;       
        }
    }
}
