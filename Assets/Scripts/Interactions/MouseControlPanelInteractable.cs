using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlPanelInteractable : MonoBehaviour
{
    [Header("References to other scripts")]
    public DoorController doorController;
    public SupportController supportController;
    public MachineScript machineScript;

    public LayerMask controlPanelLayer;

    public Camera controlPanelCamera;

    public bool isPowerONClicked = false;
    public bool isPowerOFFClicked = false;
    public bool isEmergencyStopClicked = false;
    public bool isEmergencyStopClicked2 = false;
    public bool isResetClicked = false;
    public bool isLatheOn = false;
    public bool isZeroReturnClicked = false;
    public bool isAllClicked = false;

    public bool isAudioClipPlaying = false;

    public AudioSource source;
    public AudioClip buttonPressClip;

    void Start()
    {
        source.volume = 0.05f;
    }
    void Update()
    {
        // Check if controlPanelCamera is active before proceeding
        if (controlPanelCamera != null && controlPanelCamera.gameObject.activeInHierarchy)
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
                                    //Debug.Log("Interacted with ControlPanel: " + hit.collider.gameObject.name);
                                    isZeroReturnClicked = true;
                                    PlayAudioClip();
                                    break;

                                case "btnALL":
                                    //Debug.Log("Interacted with ControlPanel: " + hit.collider.gameObject.name);
                                    if(isZeroReturnClicked) {
                                        isAllClicked = true;
                                    }
                                    PlayAudioClip();
                                    break;

                                case "btn_CycleStart":
                                    //Debug.Log("Interacted with ControlPanel: " + hit.collider.gameObject.name);
                                    if (machineScript.isUncutObjectInCuttingPosition && machineScript.isCutObjectInCuttingPosition && !doorController.isDoorOpen && isLatheOn && isAllClicked)
                                    {
                                        machineScript.moveSupport = true;
                                        isZeroReturnClicked = false;
                                        isAllClicked = false;
                                    }
                                    PlayAudioClip();
                                    break;

                                case "btn_FeedHold":
                                    //Debug.Log("Interacted with ControlPanel: " + hit.collider.gameObject.name);
                                    // Handle interaction for btn_FeedHold
                                    PlayAudioClip();
                                    break;

                                case "btnPowerON":
                                    //Debug.Log("Interacted with ControlPanel: " + hit.collider.gameObject.name);
                                    isPowerONClicked = true;
                                    isPowerOFFClicked = false;
                                    PlayAudioClip();
                                    break;

                                case "btnEmergencyStop":
                                    //Debug.Log("Interacted with ControlPanel: " + hit.collider.gameObject.name);
                                    if (isPowerONClicked) {
                                        isEmergencyStopClicked = true;
                                    } 

                                    if (isEmergencyStopClicked) {
                                        isEmergencyStopClicked2 = true;
                                    }
                                    break;

                                case "btnReset":
                                    //Debug.Log("Interacted with ControlPanel: " + hit.collider.gameObject.name);
                                    if (isEmergencyStopClicked) {
                                        isResetClicked = true;
                                        isLatheOn = true;
                                    }
                                    PlayAudioClip();
                                    break;

                                case "btnPowerOFF":
                                    //Debug.Log("Interacted with ControlPanel: " + hit.collider.gameObject.name);
                                    if (isEmergencyStopClicked2 && isLatheOn) {
                                        isPowerOFFClicked = true;
                                        isLatheOn = false;
                                        isPowerONClicked = false;
                                        isResetClicked = false;
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
