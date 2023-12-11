using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheAudioTrigger : MonoBehaviour
{
    public int counter = 0;
    public bool playAudioClip = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AudioTrigger")
        {
            counter++;
        }

        if (counter%2 == 0) {
            playAudioClip = false;
        } else {
            playAudioClip = true;
        }
    }
}
