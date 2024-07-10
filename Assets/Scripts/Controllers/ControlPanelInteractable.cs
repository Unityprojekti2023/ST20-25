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
    public LightController lightController;
    private HelpControlPanelManager helpControlPanelManager;

    [Header("References to objects and files")]
    public GameObject notes;
    public Camera cameraControlPanel;
    public AudioClip buttonSoundClip;
    private AudioSource latheAudioSource;
    public AudioSource source;

    [Header("Variables")]
    public bool isDoorClosed = true;
    bool isComputerOn = false;
    bool isLatheInitialized = false;
    bool isEmergencyStopClicked = false;
    bool isZeroReturnClicked = false;
    bool isProgramSelected = false;

    private Dictionary<string, ControlPanelState> buttonToStateMap;

    void Start()
    {
        source.volume = 0.05f;
        notes.SetActive(false);

        latheAudioSource = GetComponent<AudioSource>();
        helpControlPanelManager = GetComponent<HelpControlPanelManager>();

        InitializeButtonToStateMap();
    }

    void Update()
    {
        if (CameraController.Instance.IsCameraActive(1))
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleMouseClick();
            }
        }
    }

    private void InitializeButtonToStateMap()
    {
        buttonToStateMap = new Dictionary<string, ControlPanelState>
        {
            { "btnPowerON", ControlPanelState.PowerOnPressed },
            { "btnPowerOFF", ControlPanelState.PowerOffPressed },
            { "btnEmergencyStop", ControlPanelState.EmergencyStopPressed },
            { "btnReset", ControlPanelState.ResetPressed },
            { "btnHelp", ControlPanelState.HelpPressed },
            { "btnZeroReturn", ControlPanelState.ZeroReturnPressed },
            { "btnALL", ControlPanelState.AllPressed },
            { "btnPowerUpRestart", ControlPanelState.PowerUpRestartPressed },
            { "btnListProgram", ControlPanelState.ListProgramPressed },
            { "btnSelectProgram", ControlPanelState.SelectProgramPressed },
            { "btnCycleStart", ControlPanelState.CycleStartPressed },
        };
    }

    private void HandleMouseClick()
    {
        Ray ray = cameraControlPanel.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("ControlPanelLayer")))
        {
            string hitButton = hit.collider.gameObject.name;
            if (buttonToStateMap.TryGetValue(hitButton, out ControlPanelState newState))
            {
                currentState = newState;
                if (!controlPanelAnimations.isPlaying)
                {
                    helpControlPanelManager.NextHelpHighlight(hitButton);
                }
                HandleStateChange(currentState);
            }
            else
            {
                currentState = ControlPanelState.Idle;
            }
        }
    }

    public void OnDialRotated(bool isPositive)
    {
        currentState = isPositive ? ControlPanelState.HandlePlusPressed : ControlPanelState.HandleMinusPressed;
        HandleStateChange(currentState);
    }

    void HandleStateChange(ControlPanelState state)
    {
        PlayAudioClip();

        if (state == ControlPanelState.HelpPressed)
        {
            ToggleHelpAndNotes();
        }
        else
        {
            if (!controlPanelAnimations.isPlaying)
            {
                if (!isComputerOn)
                {
                    HandleStateWhenComputerIsOff(state);
                }
                else
                {
                    HandleStateWhenComputerIsOn(state);
                }
            }
        }
    }

    private void HandleStateWhenComputerIsOff(ControlPanelState state)
    {
        switch (state)
        {
            case ControlPanelState.PowerOnPressed:
                TurnOnComputer();
                break;
            case ControlPanelState.EmergencyStopPressed:
                isEmergencyStopClicked = true;
                break;
            case ControlPanelState.ResetPressed:
                ResetAfterEmergencyStop();
                break;
        }
    }

    private void HandleStateWhenComputerIsOn(ControlPanelState state)
    {
        switch (state)
        {
            case ControlPanelState.ZeroReturnPressed:
                isZeroReturnClicked = true;
                break;
            case ControlPanelState.AllPressed:
                InitializeLathe();
                break;
            case ControlPanelState.PowerUpRestartPressed:
                isZeroReturnClicked = true;
                InitializeLathe();
                break;
            case ControlPanelState.ListProgramPressed:
                ListPrograms();
                break;
            case ControlPanelState.SelectProgramPressed:
                SelectProgram();
                break;
            case ControlPanelState.CycleStartPressed:
                StartProgramCycle();
                break;
            case ControlPanelState.EmergencyStopPressed:
                isEmergencyStopClicked = true;
                break;
            case ControlPanelState.PowerOffPressed:
                TurnOffComputer();
                break;
            case ControlPanelState.HandlePlusPressed:
            case ControlPanelState.HandleMinusPressed:
                ChangeProgram(state == ControlPanelState.HandlePlusPressed);
                break;
        }
    }

    private void TurnOnComputer()
    {
        lightController.TurnOnLight(2);
        controlPanelAnimations.PlayStartupAnimation();
    }

    private void ResetAfterEmergencyStop()
    {
        if (isEmergencyStopClicked)
        {
            isProgramSelected = false;
            isComputerOn = true;
            isEmergencyStopClicked = false;
            lightController.TurnOnLight(1);
            controlPanelAnimations.SetSprite("HomeScreen1");
            ObjectiveManager.Instance.CompleteObjective("Turn on the lathe");
        }
    }

    private void InitializeLathe()
    {
        if (isZeroReturnClicked)
        {
            controlPanelAnimations.SetSprite("HomeScreen2");
            isLatheInitialized = true;
            ObjectiveManager.Instance.CompleteObjective("Initialize the lathe");
        }
    }

    private void ListPrograms()
    {
        if (isLatheInitialized)
        {
            controlPanelAnimations.SetSprite("Program0");
        }
    }

    private void SelectProgram()
    {
        if (isLatheInitialized && controlPanelAnimations.DoesRendererContainString("Program"))
        {
            latheController.SetSelectedProgram(controlPanelAnimations.GetProgramSpriteIndex());
            isProgramSelected = true;
            ObjectiveManager.Instance.CompleteObjective("Select a program");
        }
    }

    private void StartProgramCycle()
    {
        if (isProgramSelected && !latheController.timelineController.IsPlaying() && isDoorClosed)
        {
            latheController.PlayTimeline();
            ObjectiveManager.Instance.CompleteObjective("Run a program");
        }
        else
        {
            textInformation.UpdateText("Not all conditions are met to run program!");
        }
    }

    private void TurnOffComputer()
    {
        if (isEmergencyStopClicked)
        {
            isComputerOn = false;
            lightController.TurnOnLight(0);
            controlPanelAnimations.HideSpriteRenderer();
            ObjectiveManager.Instance.CompleteObjective("Turn the lathe off");
        }
    }

    private void ChangeProgram(bool isPositive)
    {
        if (controlPanelAnimations.DoesRendererContainString("Program"))
        {
            controlPanelAnimations.SetProgramSprite(isPositive);
        }
    }

    private void ToggleHelpAndNotes()
    {
        if (!notes.activeSelf)
        {
            helpControlPanelManager.ToggleHelpHighlight(true);
            notes.SetActive(true);
        }
        else
        {
            helpControlPanelManager.ToggleHelpHighlight(false);
            notes.SetActive(false);
        }
    }

    public void PlayAudioClip()
    {
        if (!latheAudioSource.isPlaying)
        {
            latheAudioSource.PlayOneShot(buttonSoundClip);
        }
    }
}
