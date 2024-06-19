using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlPanelInteractable : MonoBehaviour
{
    [Header("References to other scripts")]
    public MachineScript machineScript;
    public DrillController drillController;
    public EscapeMenu escapeMenu;
    public LayerMask controlPanelLayer;
    public Camera controlPanelCamera;
    public ControlpanelController controlpanelController;
    public HandleJog handleJog;
    public CleaningFeature cleaningFeature;
    public ObjectiveManager objectiveManager;
    public TextInformation textInformation;
    public ScrapInteraction scrapInteraction;
    public GameController gameController;

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
    public bool isStartUpSequenceDone = false;
    public bool hasStartUpBegun = false;
    public bool canPressHandleJogButton = true;
    public bool isProgramSelected = false;

    [Header("References to objects and files")]
    public Transform notes;
    public AudioSource source;
    public AudioClip buttonPressClip;

    [Header("Variables")]
    public int programCount = 1; 
    public int handleJogPosition = 1;
    public string whichCutItemWasLathed = " ";

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

                                case "btnALL":                                                          // Pressing "Zero Return" + "All" does the same exact thing as "Power Up Restart"
                                    
                                    if(isZeroReturnClicked && isLatheOn) 
                                    {
                                        isAllClicked = true;
                                        controlpanelController.showHomeScreen2 = true;
                                        controlpanelController.showHomeScreen1 = false;
                                        controlpanelController.UpdateScreenImage();
                                        objectiveManager.CompleteObjective("Initialize the lathe");
                                    }
                                    PlayAudioClip();
                                    break;

                                case "btnPowerUpRestart":                                               // Pressing this button once does the same exact thing as Zero Return + ALL
                                    if(isLatheOn)
                                    {
                                        isZeroReturnClicked = true;
                                        isAllClicked = true;
                                        controlpanelController.showHomeScreen2 = true;
                                        controlpanelController.showHomeScreen1 = false;
                                        controlpanelController.UpdateScreenImage();
                                        objectiveManager.CompleteObjective("Initialize the lathe");
                                    }
                                    PlayAudioClip();
                                    break;

                                case "btn_CycleStart":
                                    if (machineScript.isUncutObjectInCuttingPosition && !DoorController.instance.isDoorOpen && isLatheOn && isAllClicked && isProgramSelected && !machineScript.isMachineActive)
                                    {
                                        if(drillController.selectedProgram >= 0 && drillController.selectedProgram <= programCount) // Checking if a valid program is selected
                                        {
                                            machineScript.isMachineActive = true;
                                            machineScript.moveSupport = true;
                                            isLathingActive = true;
                                            scrapInteraction.isPile1Cleaned = false;
                                            scrapInteraction.isPile2Cleaned = false;
                                            scrapInteraction.isPile3Cleaned = false;

                                            objectiveManager.CompleteObjective("Run a program");

                                            if(cleaningFeature.isPile1Visible || cleaningFeature.isPile2Visible || cleaningFeature.isPile3Visible)
                                            {
                                                objectiveManager.DeductPoints(200);
                                                textInformation.UpdateText("-200 points for not cleaning up after lathing!");
                                            }

                                        } else {
                                            textInformation.UpdateText("Invalid program selected!");
                                        }

                                        switch(drillController.selectedProgram)
                                        {
                                            case 0: // Program #0
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
                                    } else 
                                    {
                                        textInformation.UpdateText("Not all conditions are met to run program!");
                                    }
                                    PlayAudioClip();
                                    break;

                                case "btn_FeedHold":
                                    // Handle interaction for btn_FeedHold, dont know how this button behaves IRL
                                    PlayAudioClip();
                                    break;

                                case "btnPowerON":
                                    isPowerONClicked = true;
                                    isPowerOFFClicked = false;
                                    PlayAudioClip();

                                    if(!hasStartUpBegun)
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
                                        isResetClicked = true;
                                        isLatheOn = true;
                                        objectiveManager.CompleteObjective("Turn on the lathe");
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
                                        isStartUpSequenceDone = false;
                                        hasStartUpBegun = false;
                                        controlpanelController.isProgramSelectionActive = false;

                                        controlpanelController.showHomeScreen1 = false;
                                        controlpanelController.showHomeScreen2 = false;
                                        controlpanelController.UpdateScreenImage();

                                        objectiveManager.CompleteObjective("Turn the lathe off");
                                    }
                                    PlayAudioClip();
                                    break;

                                case "HELP":
                                    if(gameController.canDisplayNotes)
                                    {
                                        if (areNotesShown) 
                                        {
                                            notes.Translate(0, -200f, 0);
                                            areNotesShown = false;
                                        } else if(!areNotesShown) 
                                        {
                                            notes.Translate(0, 200f, 0);
                                            areNotesShown = true;
                                        }
                                    } else {
                                        textInformation.UpdateText("Cannot display help in test mode!");
                                    }
                                    PlayAudioClip();
                                break;

                                case "btnListProgram":
                                    if(isLatheOn && isAllClicked)
                                    {
                                        controlpanelController.isProgramSelectionActive = true;
                                        controlpanelController.showHomeScreen2 = false;
                                        controlpanelController.UpdateScreenImage();
                                    }
                                    PlayAudioClip();
                                break;

                                case "btnSelectProgram":
                                    if(isLatheOn && isAllClicked)
                                    {
                                        isProgramSelected = true;
                                        objectiveManager.CompleteObjective("Select a program");
                                    }
                                    PlayAudioClip();
                                break;

                                case "btnHandleJogPlus":
                                    if(canPressHandleJogButton)
                                    {
                                        canPressHandleJogButton = false;
                                        StartCoroutine(handleJogButtonPressDelay());

                                        if(handleJogPosition == 8){
                                            handleJogPosition = 1;
                                        } else {
                                            handleJogPosition++;
                                        }

                                        if(drillController.selectedProgram < programCount && !isProgramSelected)
                                        {
                                            drillController.selectedProgram++;
                                        }

                                        controlpanelController.UpdateScreenImage();
                                        handleJog.updateJogPosition();
                                    }
                                break;

                                case "btnHandleJogMinus":
                                    if(canPressHandleJogButton)
                                    {
                                        canPressHandleJogButton = false;
                                        StartCoroutine(handleJogButtonPressDelay());

                                        if(handleJogPosition == 1){
                                            handleJogPosition = 8;
                                        } else {
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
                }
            }
        }
    }

    public void PlayAudioClip()                                     // Function for playing button press audio clip
    {
        if(isAudioClipPlaying == false) {                           // Making sure audio clip isnt already playing
            source.PlayOneShot(buttonPressClip);                    // Playing audioclip once
            isAudioClipPlaying = true;                              // Setting isAudioClipPlaying to true, to prevent multiple audio clips from playing at once
            StartCoroutine(soundEffectDelay());
        }
    }

    public IEnumerator soundEffectDelay()                           // Delay function for button press audio
    {
        yield return new WaitForSeconds(0.4f);                      // Waiting 0.4 seconds, then allowing audio to be played again
        isAudioClipPlaying = false;
    }

    public IEnumerator handleJogButtonPressDelay()                  // Delay function for handle jog button presses
    {
        yield return new WaitForSeconds(0.4f);                      // Waiting 0.4 seconds, then allowing audio to be played again
        canPressHandleJogButton = true;
    }

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
    }
}
