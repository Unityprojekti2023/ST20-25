using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlPanelInteractable : MonoBehaviour
{
    [Header("References to other scripts")]
    public DoorController doorController;
    public SupportController supportController;
    public MachineScript machineScript;
    public EscapeMenu escapeMenu;
    public LayerMask controlPanelLayer;
    public Camera controlPanelCamera;

    [Header("Boolean variables")]
    public bool isPowerONClicked = false;
    public bool isPowerOFFClicked = false;
    public bool isEmergencyStopClicked = false;
    public bool isEmergencyStopClicked2 = false;
    public bool isResetClicked = false;
    public bool isLatheOn = false;
    public bool isZeroReturnClicked = false;
    public bool isAllClicked = false;
    public bool isLathingActive = false;
    public bool areNotesShown = false;
    public bool isAudioClipPlaying = false;

    [Header("References to objects and files")]
    public Transform notes;
    public AudioSource source;
    public AudioClip buttonPressClip;

    void Start()
    {
        notes.Translate(0, -200f, 0);
        //Move notes under the floor (Y Axis)
        source.volume = 0.05f;
    }
    void Update()
    {
        // Check if controlPanelCamera is active before proceeding
        if (controlPanelCamera != null && controlPanelCamera.gameObject.activeInHierarchy && !escapeMenu.isGamePaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = controlPanelCamera.ScreenPointToRay(Input.mousePosition);

                Collider[] colliders = GetComponentsInChildren<Collider>();
                foreach (Collider collider in colliders)
                {
                    if (collider.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
                    {
                        string buttonName = collider.gameObject.name;
                        // Check if the collider belongs to the specified layer
                        if (((1 << collider.gameObject.layer) & controlPanelLayer) != 0)
                        {
                            switch (buttonName)
                            {
                                case "btnZeroReturn":
                                    if(isLatheOn) 
                                    {
                                        isZeroReturnClicked = true;
                                        
                                    }
                                    PlayAudioClip();
                                    break;

                                case "btnALL":
                                    
                                    if(isZeroReturnClicked && isLatheOn) 
                                    {
                                        isAllClicked = true;
                                    }
                                    PlayAudioClip();
                                    break;

                                case "btn_CycleStart":
                                    
                                    if (machineScript.isUncutObjectInCuttingPosition && machineScript.isCutObjectInCuttingPosition && !doorController.isDoorOpen && isLatheOn && isAllClicked)
                                    {
                                        machineScript.moveSupport = true;
                                        isZeroReturnClicked = false;
                                        isAllClicked = false;
                                        isLathingActive = true;
                                    }
                                    PlayAudioClip();
                                    break;

                                case "btn_FeedHold":
                                    // Handle interaction for btn_FeedHold
                                    PlayAudioClip();
                                    break;

                                case "btnPowerON":
                                    isPowerONClicked = true;
                                    isPowerOFFClicked = false;
                                    PlayAudioClip();
                                    break;

                                case "btnEmergencyStop":
                                    if (isPowerONClicked) 
                                    {
                                        isEmergencyStopClicked = true;
                                    } 

                                    if (isEmergencyStopClicked) 
                                    {
                                        isEmergencyStopClicked2 = true;
                                    }
                                    PlayAudioClip();
                                    break;

                                case "btnReset":
                                    if (isEmergencyStopClicked) 
                                    {
                                        isResetClicked = true;
                                        isLatheOn = true;
                                    }
                                    PlayAudioClip();
                                    break;

                                case "btnPowerOFF":
                                    if (isEmergencyStopClicked2 && isLatheOn) 
                                    {
                                        isPowerOFFClicked = true;
                                        isLatheOn = false;
                                        isPowerONClicked = false;
                                        isResetClicked = false;
                                    }
                                    PlayAudioClip();
                                    break;

                                case "HELP":
                                if (areNotesShown) 
                                {
                                    notes.Translate(0, -200f, 0);
                                    areNotesShown = false;
                                } else if(!areNotesShown) 
                                {
                                    notes.Translate(0, 200f, 0);
                                    areNotesShown = true;
                                }
                                PlayAudioClip();
                                break;

                                    // Add more cases for other button names as needed
                                    // Rename button into btn_"BUTTON NAME" and collider after which add case with that btn name
                            }
                        }
                    }
                }
            }
        }
    }

    public void PlayAudioClip()
    {
        if(isAudioClipPlaying == false) {
            source.PlayOneShot(buttonPressClip);
            isAudioClipPlaying = true;
            StartCoroutine(soundEffectDelay());
        }
    }

    public IEnumerator soundEffectDelay(){
        yield return new WaitForSeconds(0.4f);
        isAudioClipPlaying = false;
    }
}
