using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlPanelState
{
    Idle,
    PowerOnPressed,
    PowerOffPressed,
    EmergencyStopPressed,
    ResetPressed,
    HelpPressed,
    ZeroReturnPressed,
    AllPressed,
    PowerUpRestartPressed,
    ListProgramPressed,
    SelectProgramPressed,
    CycleStartPressed,
    HandlePlusPressed,
    HandleMinusPressed,
}

public class ControlPanelInteractable : MonoBehaviour
{
    private ControlPanelState currentState = ControlPanelState.Idle;

    [Header("References to other scripts")]
    public ControlPanelAnimations controlPanelAnimations;
    public LatheController latheController;
    public TextInformation textInformation;

    [Header("References to objects and files")]
    public GameObject notes;
    public Camera cameraControlPanel;
    public AudioClip buttonSoundClip;
    private AudioSource latheAudioSource;
    public AudioSource source;

    [Header("Variables")]
    public bool isDoorClosed = true;
    public bool isComputerOn = false;
    public bool isLatheInitialized = false;
    public bool isEmergencyStopClicked = false;
    public bool isZeroReturnClicked = false;
    public bool isProgramSelected = false;

    [Header("Check if needed")]
    //TODO: Check if these are needed
    public bool isPowerONClicked = false;
    public bool isPowerOFFClicked = false;
    public bool isLathingActive = false;
    public int handleJogPosition = 1;


    void Start()
    {
        //Move notes under the floor (Y Axis)
        source.volume = 0.05f;
        //Hide notes
        notes.SetActive(false);

        latheAudioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        // Check if controlPanelCamera is active before proceeding
        if (CameraController.Instance.IsCameraActive(1))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cameraControlPanel.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                int layer_mask = LayerMask.GetMask("ControlPanelLayer");

                //Perform a raycast to detect the button that was clicked
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer_mask))
                {
                    string hitButton = hit.collider.gameObject.name;
                    Debug.Log("Hit button: " + hitButton);

                    switch (hitButton)
                    {
                        case "btnPowerON":
                            currentState = ControlPanelState.PowerOnPressed;
                            break;
                        case "btnPowerOFF":
                            currentState = ControlPanelState.PowerOffPressed;
                            break;
                        case "btnEmergencyStop":
                            currentState = ControlPanelState.EmergencyStopPressed;
                            break;
                        case "btnReset":
                            currentState = ControlPanelState.ResetPressed;
                            break;
                        case "btnHelp":
                            currentState = ControlPanelState.HelpPressed;
                            break;
                        case "btnZeroReturn":
                            currentState = ControlPanelState.ZeroReturnPressed;
                            break;
                        case "btnALL":
                            currentState = ControlPanelState.AllPressed;
                            break;
                        case "btnPowerUpRestart":
                            currentState = ControlPanelState.PowerUpRestartPressed;
                            break;
                        case "btnListProgram":
                            currentState = ControlPanelState.ListProgramPressed;
                            break;
                        case "btnSelectProgram":
                            currentState = ControlPanelState.SelectProgramPressed;
                            break;
                        case "btnCycleStart":
                            currentState = ControlPanelState.CycleStartPressed;
                            break;
                        case "btnHandleJogPlus":
                            currentState = ControlPanelState.HandlePlusPressed;     //TODO: Make the jog functional
                            break;
                        case "btnHandleJogMinus":
                            currentState = ControlPanelState.HandleMinusPressed;    //TODO: Make the jog functional
                            break;
                        default:
                            currentState = ControlPanelState.Idle;
                            break;
                    }
                    // Handle the button press on currentState
                    HandleStateChange(currentState);
                }
            }
        }
    }

    void HandleStateChange(ControlPanelState state)
    {
        // Play button press audio clip
        PlayAudioClip();

        // Handle help button press and notes display
        if (state == ControlPanelState.HelpPressed && !notes.activeSelf)
        {
            notes.SetActive(true);
        }
        else if (state == ControlPanelState.HelpPressed && notes.activeSelf)
        {
            notes.SetActive(false);
        }

        // If animation is playing do not allow state changes
        if (!controlPanelAnimations.isPlaying)
        {
            // Perform actions based on the current state
            if (!isComputerOn)
            {
                switch (state)
                {
                    case ControlPanelState.PowerOnPressed:
                        controlPanelAnimations.PlayStartupAnimation();
                        break;

                    case ControlPanelState.EmergencyStopPressed:
                        isEmergencyStopClicked = true;
                        break;

                    case ControlPanelState.ResetPressed:
                        if (isEmergencyStopClicked)
                        {
                            isProgramSelected = false;
                            isComputerOn = true;
                            isEmergencyStopClicked = false;
                            controlPanelAnimations.SetSprite("HomeScreen1");
                            ObjectiveManager.Instance.CompleteObjective("Turn on the lathe");
                        }
                        break;
                }
            }
            else if (isComputerOn)  //TODO: Does this need to be else if?
            {
                // Switch case when computer is on
                switch (state)
                {
                    case ControlPanelState.ZeroReturnPressed:
                        isZeroReturnClicked = true;
                        break;

                    case ControlPanelState.AllPressed:  // Pressing "Zero Return" + "All" does the same exact thing as "Power Up Restart"
                        if (isZeroReturnClicked)
                        {
                            controlPanelAnimations.SetSprite("HomeScreen2");
                            isLatheInitialized = true;
                        }
                        //TODO: Is there possibility of this and PowerUpRestartPressed being called at the same time or otherwise bugged?
                        ObjectiveManager.Instance.CompleteObjective("Initialize the lathe");
                        break;

                    case ControlPanelState.PowerUpRestartPressed:   // Pressing this button once does the same exact thing as Zero Return + ALL
                        controlPanelAnimations.SetSprite("HomeScreen2");
                        isLatheInitialized = true;
                        ObjectiveManager.Instance.CompleteObjective("Initialize the lathe");
                        break;

                    case ControlPanelState.ListProgramPressed:
                        if (isLatheInitialized)
                            controlPanelAnimations.SetSprite("Program0");
                        //TODO: Logic for when different programs are selected
                        break;

                    case ControlPanelState.HandlePlusPressed:
                        if (controlPanelAnimations.DoesRendererContainString("Program"))
                        {
                            controlPanelAnimations.SetNextProgramSprite();
                        }
                        break;

                    case ControlPanelState.HandleMinusPressed:
                        if (controlPanelAnimations.DoesRendererContainString("Program"))
                        {
                            controlPanelAnimations.SetNextProgramSprite();
                        }
                        break;

                    case ControlPanelState.SelectProgramPressed:
                        if (isLatheInitialized && controlPanelAnimations.DoesRendererContainString("Program"))
                        {
                            // Set the selected animation, object and material to the lathe
                            latheController.SetSelectedProgram(controlPanelAnimations.GetProgramSpriteIndex());
                            isProgramSelected = true;   //TODO: Is there any otherway to check if program is selected?

                            ObjectiveManager.Instance.CompleteObjective("Select a program");
                        }
                        break;

                    case ControlPanelState.CycleStartPressed:
                        if (isProgramSelected && !latheController.timelineController.IsPlaying() && isDoorClosed)
                        {
                            latheController.PlayTimeline();
                            ObjectiveManager.Instance.CompleteObjective("Run a program");
                        }
                        else
                        {
                            textInformation.UpdateText("Not all conditions are met to run program!");
                        }
                        break;

                    case ControlPanelState.EmergencyStopPressed:
                        isEmergencyStopClicked = true;
                        break;

                    case ControlPanelState.PowerOffPressed:
                        if (isEmergencyStopClicked)
                        {
                            isComputerOn = false;
                            // Hide control panel screens
                            controlPanelAnimations.HideSpriteRenderer();
                            ObjectiveManager.Instance.CompleteObjective("Turn the lathe off");
                        }
                        break;
                }

            }

        }

    }

    public void PlayAudioClip()                                     // Function for playing button press audio clip
    {
        // Check if audio clip is already playing
        if (!latheAudioSource.isPlaying)
        {
            latheAudioSource.PlayOneShot(buttonSoundClip);
        }
    }
}
