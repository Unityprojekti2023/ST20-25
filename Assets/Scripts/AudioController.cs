using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource source;
    public AudioClip openingClip;
    public AudioClip closingClip;

    public DoorController doorController;

    public bool isOpeningClipPlaying = false;
    public bool isClosingClipPlaying = false;

    void Update()
    {
        if (doorController.playOpeningClip == true && isOpeningClipPlaying == false) {
            source.PlayOneShot(openingClip);
            isOpeningClipPlaying = true;
            isClosingClipPlaying = false;
        }

        if (doorController.playClosingClip == true && isClosingClipPlaying == false) {
            source.PlayOneShot(closingClip);
            isClosingClipPlaying = true;
            isOpeningClipPlaying = false;
        }
    }
}
