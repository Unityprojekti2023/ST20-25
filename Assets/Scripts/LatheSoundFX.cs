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

    [Header("Values & variables")]
    public bool isLatheCuttingClipAlreadyPlaying = false;
    public float CuttingClipStartDelay = 12f;
    public float volume = 0.02f;

    void Start() 
    {
        source.volume = volume;                                                         // Setting volume level for the audio source
    }

    void Update()
    {
        if (machineScript.isMachineActive && !isLatheCuttingClipAlreadyPlaying) {       // Checking if the machine is active and making sure an audio clip isn't playing already
            StartCoroutine(latheCuttingStartDelay());                                   // Calling coroutine to play the audio clip
        }

        if (machineScript.isAnimationComplete) {                                        // Checking if the cutting animation is complete
            isLatheCuttingClipAlreadyPlaying = false;                                   // Making "isLatheCuttingClipAlreadyPlaying" false so the audio clip can be played again next time an item is being cut
        }
    }

    public IEnumerator latheCuttingStartDelay(){                                        // Function for playing lathe cutting audio clip
        yield return new WaitForSeconds(CuttingClipStartDelay);                         // Waiting for the drill to get close to the object to be cut, before starting audio clip
        source.PlayOneShot(latheCuttingClip);                                           // Playing the audio clip
        isLatheCuttingClipAlreadyPlaying = true;                                        // Setting "isLatheCuttingClipAlreadyPlaying" to prevent multiple audio clips from playing at once
    }
}