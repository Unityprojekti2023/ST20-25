using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheSoundFX : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource beginningSource;
    public AudioSource endingSource;
    public AudioSource lathingSource;
    public AudioSource idlingSource;

    [Header("Audio Clips")]
    public AudioClip beginningClip;
    public AudioClip endingClip;
    public AudioClip lathingClip;
    public AudioClip idlingClip;

    [Header("Boolean Variables")]
    public bool playBeginningClip = false;
    public bool playEndingClip = false;
    public bool playLathingClip = false;
    public bool playIdlingclip = false;

    public bool isBeginningClipPlaying = false;
    public bool isEndingClipPlaying = false;
    public bool isLathingClipPlaying = false;
    public bool isIdlingClipPlaying = false;

    public float volume = 0.5f;

    void Start() 
    {
        beginningSource.clip = beginningClip;
        endingSource.clip = endingClip;
        lathingSource.clip = lathingClip;
        idlingSource.clip = idlingClip;

        beginningSource.volume = volume;
        endingSource.volume = volume;
        lathingSource.volume = volume;
        idlingSource.volume = volume;
    }

    void Update()
    {
        // Checking if its time to play "Beginning" audio clip, and making sure the clip isnt already playing
        if(playBeginningClip && !isBeginningClipPlaying)
        {
            beginningSource.Play();
            isBeginningClipPlaying = true;
        }

        // Checking if its time to play "Ending" audio clip, and making sure the clip isnt already playing
        if(playEndingClip && !isEndingClipPlaying)
        {
            endingSource.Play();
            isEndingClipPlaying = true;
        }

        // Checking if its time to play "Lathing" audio clip, and making sure the clip isnt already playing
        if(playLathingClip && !isLathingClipPlaying)
        {
            lathingSource.Play();
            isLathingClipPlaying = true;
        }

        // Checking if its time to play "Idling" audio clip, and making sure the clip isnt already playing
        if(playIdlingclip && !isIdlingClipPlaying)
        {
            idlingSource.Play();
            isIdlingClipPlaying = true;
        }
    }
}