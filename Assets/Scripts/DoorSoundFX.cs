using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSoundFX : MonoBehaviour
{
    [Header("Audio source & clips")]
    public AudioSource source;
    public AudioClip openingClip;
    public AudioClip closingClip;

    [Header("Reference to DoorController script")]
    public DoorController doorController;

    [Header("Variables")]
    public bool isOpeningClipPlaying = false;
    public bool isClosingClipPlaying = false;
    public float volume = 0.5f;

    void Start()
    {
        
        source.volume = volume;                                             //Setting the audio volume
    }

    void Update()
    {
        if (doorController.playOpeningClip && !isOpeningClipPlaying) {      // Checking if the "playOpeningClip" has been set to true in doorController and making sure a clip isn't already playing
            source.PlayOneShot(openingClip);                                // Playing door opening audio clip
            isOpeningClipPlaying = true;                                    // Setting "isOpeningClipPlaying" to true so multiple audio clips dont play at once
            isClosingClipPlaying = false;                                      
        }

        if (doorController.playClosingClip && !isClosingClipPlaying) {      // Checking if "playClosingClip" has been set to true in doorController and making sure a clip isn't already playing
            source.PlayOneShot(closingClip);                                // Playing door closing audio clip
            isClosingClipPlaying = true;                                    // Setting "isClosingClipPlaying" to true so multiple audio clips dont play at once
            isOpeningClipPlaying = false;
        }
    }
}
