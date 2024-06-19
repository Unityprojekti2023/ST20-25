using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSoundFX : MonoBehaviour
{
    [Header("Audio source & clips")]
    public AudioSource openingSource;
    public AudioSource closingSource;
    public AudioClip openingClip;
    public AudioClip closingClip;

    [Header("References to other scripts")]
    public MouseControlPanelInteractable mouseControlPanelInteractable;

    [Header("Variables")]
    public bool isOpeningClipPlaying = false;
    public bool isClosingClipPlaying = false;
    public float volume = 0.1f;

    void Start()
    {
        openingSource.clip = openingClip;
        closingSource.clip = closingClip;

        openingSource.volume = volume;                                             //Setting the audio volume
        closingSource.volume = volume;
    }

    void Update()
    {
        if (DoorController.instance.playOpeningClip && !isOpeningClipPlaying && !mouseControlPanelInteractable.isLathingActive) 
        {                                                                   // Checking if the "playOpeningClip" has been set to true in DoorController and making sure a clip isn't already playing
            openingSource.Play();                                           // Playing door opening audio clip
            isOpeningClipPlaying = true;                                    // Setting "isOpeningClipPlaying" to true so multiple audio clips dont play at once
            isClosingClipPlaying = false;                                      
        }

        if (DoorController.instance.playClosingClip && !isClosingClipPlaying && !mouseControlPanelInteractable.isLathingActive) 
        {                                                                   // Checking if "playClosingClip" has been set to true in DoorController and making sure a clip isn't already playing
            closingSource.Play();                                           // Playing door closing audio clip
            isClosingClipPlaying = true;                                    // Setting "isClosingClipPlaying" to true so multiple audio clips dont play at once
            isOpeningClipPlaying = false;
        }
    }
}
