using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource source;
    public AudioClip openingClip;
    public AudioClip closingClip;

    public DoorController doorController;

    public bool isClipPlaying = false;

    void Update()
    {
        if (doorController.playOpeningClip == true) {
            source.PlayOneShot(openingClip);
        }
    }
}
