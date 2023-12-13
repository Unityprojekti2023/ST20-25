using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheSoundFX : MonoBehaviour
{
    [Header("Audio sources & clips")]
    public AudioSource source;
    public AudioClip latheCuttingClip;

    [Header("References to other scripts")]
    public MachineScript machineScript;
    public EscapeMenu escapeMenu;
    public LatheAudioTrigger latheAudioTrigger;

    [Header("Values & variables")]
    public bool isLatheCuttingClipAlreadyPlaying = false;
    public float CuttingClipStartDelay = 12f;
    public float volume = 0.2f;

    void Start() 
    {
        source.clip = latheCuttingClip;
        source.volume = volume;                                                         // Setting volume level for the audio source
    }

    void Update()
    {
        if (machineScript.isMachineActive && !isLatheCuttingClipAlreadyPlaying)         // Checking if the machine is active and making sure an audio clip isn't playing already
        {       
            if (latheAudioTrigger.playAudioClip) 
            {
                source.Play();                                                          // Playing the audio clip
                isLatheCuttingClipAlreadyPlaying = true;                                // Setting "isLatheCuttingClipAlreadyPlaying" to prevent multiple audio clips from playing at once
            }   
        }

        if (machineScript.isAnimationComplete) 
        {                                                                               // Checking if the cutting animation is complete
            isLatheCuttingClipAlreadyPlaying = false;                                   // Making "isLatheCuttingClipAlreadyPlaying" false so the audio clip can be played again next time an item is being cut
        }

        if (escapeMenu.isGamePaused) 
        {
            Time.timeScale = 0;                                                         // Changing timescale to 0 when game is paused to stop coroutine timers
        }
        else
        {
            Time.timeScale = 1.0f;                                                      // Changing timescale back to 1 when game is not paused anymore to continue coroutine timers
        }
    }
}