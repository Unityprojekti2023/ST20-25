using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheSoundFX : MonoBehaviour
{
    public AudioSource source;
    public AudioClip latheOnClip;
    public AudioClip latheCuttingClip;
    public MachineScript machineScript;
    public MouseControlPanelInteractable mouseControlPanelInteractable;
    public bool isLatheOnClipAlreadyPlaying = false;
    public bool isLatheCuttingClipAlreadyPlaying = false;
    public float latheOnClipLength = 17.5f; //Lathe ON audio clip length in seconds
    public float latheCuttingClipLength = 0f; //Lathe cutting audio clip length in seconds

    void Start() 
    {
        source.volume = 0.2f;
    }

    void Update()
    {
        if(mouseControlPanelInteractable.isLatheOn && !isLatheOnClipAlreadyPlaying) 
        {
            source.PlayOneShot(latheOnClip);
            isLatheOnClipAlreadyPlaying = true;
            StartCoroutine(latheOnClipRestart());
        }

        if (machineScript.isMachineActive && !isLatheCuttingClipAlreadyPlaying) {
            source.PlayOneShot(latheCuttingClip);
            isLatheCuttingClipAlreadyPlaying = true;
            StartCoroutine(latheCuttingClipRestart());
        }
    }
    
    public IEnumerator latheOnClipRestart(){
        yield return new WaitForSeconds(latheOnClipLength);
        isLatheOnClipAlreadyPlaying = false;
    }

    public IEnumerator latheCuttingClipRestart(){
        yield return new WaitForSeconds(latheCuttingClipLength);
        isLatheCuttingClipAlreadyPlaying = false;
    }
}