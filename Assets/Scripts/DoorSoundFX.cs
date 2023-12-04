using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSoundFX : MonoBehaviour
{
    public AudioSource source;
    public AudioClip openingClip;
    public AudioClip closingClip;

    public DoorController doorController;

    public bool isOpeningClipPlaying = false;
    public bool isClosingClipPlaying = false;

    void Update()
    {
        if (doorController.playOpeningClip && !isOpeningClipPlaying) {
            source.PlayOneShot(openingClip);
            isOpeningClipPlaying = true;
            isClosingClipPlaying = false;
        }

        if (doorController.playClosingClip && !isClosingClipPlaying) {
            source.PlayOneShot(closingClip);
            isClosingClipPlaying = true;
            isOpeningClipPlaying = false;
        }
    }
}
