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

public class MouseControlPanelInteractable : MonoBehaviour
{
    private ControlPanelState currentState = ControlPanelState.Idle;
    public GameObject cutItemSH1001;
    public Transform attachmentPoint;
    public AudioClip buttonSoundClip;
    private AudioSource latheAudioSource;
    public Camera cameraControlPanel;

    public ControlPanelAnimations controlPanelAnimations;
    public LatheController latheController;
    public TextInformation textInformation;

    public bool isComputerOn = false;
    public bool isLatheInitialized = false;


/*
    [Header("References to other scripts")]
    public MachineScript machineScript;
    public DrillController drillController;
    public LayerMask controlPanelLayer;
    public ControlpanelController controlpanelController;
    public HandleJog handleJog;
    public CleaningFeature cleaningFeature;
    public ScrapInteraction scrapInteraction;
    public GameController gameController;
    public DoorInteractable doorInteractable;
    public LatheTimelineController timelineController;*/

    [Header("Boolean variables")]
    public bool isPowerONClicked = false;
    public bool isPowerOFFClicked = false;
    public bool isEmergencyStopClicked = false;
    public bool isEmergencyStopClicked2 = false;
    public bool isLatheOn = false;
    public bool isZeroReturnClicked = false;
    public bool isAllClicked = false;
    public bool isLathingActive = false;
    public bool areNotesShown = false;
    public bool isAudioClipPlaying = false;
    public bool isStartUpSequenceDone = false;
    public bool hasStartUpBegun = false;
    public bool canPressHandleJogButton = true;
    public bool isProgramSelected = false;

    [Header("References to objects and files")]
    public GameObject notes;
    public AudioSource source;
    public AudioClip buttonPressClip;

    [Header("Variables")]
    public int programCount = 1;
    public int handleJogPosition = 1;
    public string whichCutItemWasLathed = " ";


    void Start()
    {
        //Hide notes
        notes.SetActive(false);
        //Move notes under the floor (Y Axis)
        source.volume = 0.05f;

        latheAudioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        // Check if controlPanelCamera is active before proceeding
        if (CameraController.Instance.IsCameraActive(1))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse button pressed on control panel!");
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

                /*
                Collider[] colliders = GetComponentsInChildren<Collider>(); //TODO: Anyway to optimize this?
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
                                    if (isLatheOn)
                                    {
                                        isZeroReturnClicked = true;
                                    }
                                    PlayAudioClip();
                                    break;

                                case "btnALL":                                                          // Pressing "Zero Return" + "All" does the same exact thing as "Power Up Restart"

                                    if (isZeroReturnClicked && isLatheOn)
                                    {
                                        isAllClicked = true;
                                        controlpanelController.showHomeScreen2 = true;
                                        controlpanelController.showHomeScreen1 = false;
                                        controlpanelController.UpdateScreenImage();
                                        ObjectiveManager.Instance.CompleteObjective("Initialize the lathe");
                                    }
                                    PlayAudioClip();
                                    break;

                                case "btnPowerUpRestart":                                               // Pressing this button once does the same exact thing as Zero Return + ALL
                                    if (isLatheOn)
                                    {
                                        isZeroReturnClicked = true;
                                        isAllClicked = true;
                                        controlpanelController.showHomeScreen2 = true;
                                        controlpanelController.showHomeScreen1 = false;
                                        controlpanelController.UpdateScreenImage();
                                        ObjectiveManager.Instance.CompleteObjective("Initialize the lathe");
                                    }
                                    PlayAudioClip();
                                    break;

                                case "btnCycleStart":
                                    if (machineScript.isUncutObjectInCuttingPosition && !doorInteractable.isDoorOpen && isLatheOn && isAllClicked && isProgramSelected && !machineScript.isMachineActive)
                                    {
                                        if (drillController.selectedProgram >= 0 && drillController.selectedProgram <= programCount) // Checking if a valid program is selected
                                        {
                                            machineScript.isMachineActive = true;
                                            machineScript.moveSupport = true;
                                            isLathingActive = true;
                                            scrapInteraction.isPile1Cleaned = false;
                                            scrapInteraction.isPile2Cleaned = false;
                                            scrapInteraction.isPile3Cleaned = false;

                                            ObjectiveManager.Instance.CompleteObjective("Run a program");

                                            if (cleaningFeature.isPile1Visible || cleaningFeature.isPile2Visible || cleaningFeature.isPile3Visible)
                                            {
                                                ObjectiveManager.Instance.DeductPoints(200);
                                                textInformation.UpdateText("-200 points for not cleaning up after lathing!");
                                            }

                                        }
                                        else
                                        {
                                            textInformation.UpdateText("Invalid program selected!");
                                        }

                                        switch (drillController.selectedProgram)
                                        {
                                            case 0: // Program #0
                                                // Start the lathing animation for program #0
                                                timelineController.PlayTimeline();

                                                drillController.targetCounter = 6;                      // targetCounter indicates how many round trips the lathe will do in the animation
                                                machineScript.moveCutObject1ToCuttingPosition();        // Moving the program #0 cut object into place (Inside uncut object)
                                                whichCutItemWasLathed = "CutObject1";
                                                break;

                                            case 1: // Program #1
                                                drillController.targetCounter = 4;                      // targetCounter indicates how many round trips the lathe will do in the animation
                                                machineScript.moveCutObject2ToCuttingPosition();        // Moving the program #1 cut object into place (Inside uncut object)
                                                whichCutItemWasLathed = "CutObject2";
                                                break;

                                                // Add more cases for new programs
                                        }
                                    }
                                    else
                                    {
                                        textInformation.UpdateText("Not all conditions are met to run program!");
                                    }
                                    PlayAudioClip();
                                    break;*/
                                /*
                                case "btn_FeedHold":
                                    // Handle interaction for btn_FeedHold, dont know how this button behaves IRL
                                    PlayAudioClip();
                                    break;                                                                                            
                                case "btnPowerON":
                                    isPowerONClicked = true;
                                    isPowerOFFClicked = false;
                                    PlayAudioClip();

                                    if (!hasStartUpBegun)
                                    {
                                        StartCoroutine(startupSequence());
                                        hasStartUpBegun = true;
                                    }
                                    break;

                                case "btnEmergencyStop":
                                    if (isPowerONClicked && isStartUpSequenceDone)
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
                                        isLatheOn = true;
                                        ObjectiveManager.Instance.CompleteObjective("Turn on the lathe");
                                    }
                                    PlayAudioClip();
                                    break;
                                case "btnPowerOFF":
                                    if (isEmergencyStopClicked2 && isLatheOn)
                                    {
                                        isPowerOFFClicked = true;
                                        isLatheOn = false;
                                        isPowerONClicked = false;
                                        isStartUpSequenceDone = false;
                                        hasStartUpBegun = false;
                                        controlpanelController.isProgramSelectionActive = false;

                                        controlpanelController.showHomeScreen1 = false;
                                        controlpanelController.showHomeScreen2 = false;
                                        controlpanelController.UpdateScreenImage();

                                        ObjectiveManager.Instance.CompleteObjective("Turn the lathe off");
                                    }
                                    PlayAudioClip();
                                    break;
                                    case "btnHelp":
                                        if (gameController.canDisplayNotes)
                                        {
                                            if (areNotesShown)
                                            {
                                                notes.Translate(0, -200f, 0);
                                                areNotesShown = false;
                                            }
                                            else if (!areNotesShown)
                                            {
                                                notes.Translate(0, 200f, 0);
                                                areNotesShown = true;
                                            }
                                        }
                                        else
                                        {
                                            textInformation.UpdateText("Cannot display help in test mode!");
                                        }
                                        PlayAudioClip();
                                        break;

                                case "btnListProgram":
                                    if (isLatheOn && isAllClicked)
                                    {
                                        controlpanelController.isProgramSelectionActive = true;
                                        controlpanelController.showHomeScreen2 = false;
                                        controlpanelController.UpdateScreenImage();
                                    }
                                    PlayAudioClip();
                                    break;
                                
                                case "btnSelectProgram":
                                    if (isLatheOn && isAllClicked)
                                    {
                                        isProgramSelected = true;
                                        ObjectiveManager.Instance.CompleteObjective("Select a program");
                                    }
                                    PlayAudioClip();
                                    break;

                                case "btnHandleJogPlus":
                                    if (canPressHandleJogButton)
                                    {
                                        canPressHandleJogButton = false;
                                        StartCoroutine(handleJogButtonPressDelay());

                                        if (handleJogPosition == 8)
                                        {
                                            handleJogPosition = 1;
                                        }
                                        else
                                        {
                                            handleJogPosition++;
                                        }

                                        if (drillController.selectedProgram < programCount && !isProgramSelected)
                                        {
                                            drillController.selectedProgram++;
                                        }

                                        controlpanelController.UpdateScreenImage();
                                        handleJog.updateJogPosition();
                                    }
                                    break;

                                case "btnHandleJogMinus":
                                    if (canPressHandleJogButton)
                                    {
                                        canPressHandleJogButton = false;
                                        StartCoroutine(handleJogButtonPressDelay());

                                        if (handleJogPosition == 1)
                                        {
                                            handleJogPosition = 8;
                                        }
                                        else
                                        {
                                            handleJogPosition--;
                                        }

                                        if (drillController.selectedProgram > 0 && !isProgramSelected)
                                        {
                                            drillController.selectedProgram--;
                                        }

                                        controlpanelController.UpdateScreenImage();
                                        handleJog.updateJogPosition();
                                    }
                                    break;

                                    // Add more cases for other button names as needed
                                    // Rename button into btn_"BUTTON NAME" and collider after which add case with that btn name
                            }
                        }
                    }
                }*/
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
                            latheController.SetSelectedProgram("SH_1001", controlPanelAnimations.GetProgramSpriteIndex());
                            isProgramSelected = true;   //TODO: Is there any otherway to check if program is selected?

                            ObjectiveManager.Instance.CompleteObjective("Select a program");
                        }
                        break;

                    case ControlPanelState.CycleStartPressed:
                        if (isProgramSelected && !latheController.timelineController.IsPlaying())
                        {
                            latheController.PlayTimeline();
                            ObjectiveManager.Instance.CompleteObjective("Run a program");
                        }
                        else
                        {
                            textInformation.UpdateText("Not all conditions are met to run program!");
                        }
                        break;

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

    public IEnumerator handleJogButtonPressDelay()                  // Delay function for handle jog button presses
    {
        yield return new WaitForSeconds(0.4f);                      // Waiting 0.4 seconds, then allowing audio to be played again
        canPressHandleJogButton = true;
    }
/*
    public IEnumerator startupSequence()                            // Coroutine responsible for the startup sequence
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));      // Wait 1-3 seconds before showing first screen
        controlpanelController.showBlackScreen = true;
        controlpanelController.UpdateScreenImage();
        yield return new WaitForSeconds(Random.Range(1f, 3f));      // Wait 1-3 seconds before updating to "attention" screen
        controlpanelController.showAttentionScreen = true;
        controlpanelController.showBlackScreen = false;
        controlpanelController.UpdateScreenImage();
        yield return new WaitForSeconds(Random.Range(5f, 10f));     // Wait 5-10 seconds before showing "home screen 1"
        controlpanelController.showHomeScreen1 = true;
        controlpanelController.showAttentionScreen = false;
        controlpanelController.UpdateScreenImage();
        isStartUpSequenceDone = true;                               // Marking startup sequence as completed
    }*/
}
