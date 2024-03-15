using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlPanelInteractable : MonoBehaviour
{
    [Header("References to other scripts")]
    public DoorController doorController;
    public SupportController supportController;
    public MachineScript machineScript;
    public DrillController drillController;
    public EscapeMenu escapeMenu;
    public LayerMask controlPanelLayer;
    public Camera controlPanelCamera;
    public RayInteractor rayInteractor;
    public ControlpanelController controlpanelController;
    public HandleJog handleJog;
    public CleaningFeature cleaningFeature;
    public ObjectiveManager objectiveManager;
    public TextInformation textInformation;
    public ScrapInteraction scrapInteraction;

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

                                case "btnALL":
                                    
                                    if(isZeroReturnClicked && isLatheOn) 
                                    {
                                        isAllClicked = true;
                                        controlpanelController.showHomeScreen2 = true;
                                        controlpanelController.showHomeScreen1 = false;
                                        controlpanelController.updateScreenImage();
                                        objectiveManager.CompleteObjective("Initialize the lathe");
                                    }
                                    PlayAudioClip();
                                    break;

                                case "btnPowerUpRestart": // Pressing this button once does the same exact thing as Zero Return + ALL
                                    if(isLatheOn)
                                    {
                                        isZeroReturnClicked = true;
                                        isAllClicked = true;
                                        controlpanelController.showHomeScreen2 = true;
                                        controlpanelController.showHomeScreen1 = false;
                                        controlpanelController.updateScreenImage();
                                        objectiveManager.CompleteObjective("Initialize the lathe");
                                    }
                                    PlayAudioClip();
                                    break;

                                case "btn_CycleStart":
                                    if (machineScript.isUncutObjectInCuttingPosition /*&& machineScript.isCutObjectInCuttingPosition */ && !doorController.isDoorOpen && isLatheOn && isAllClicked && isProgramSelected && !machineScript.isMachineActive)
                                    {
                                        if(drillController.selectedProgram >= 0 && drillController.selectedProgram <= programCount) // Checking if a valid program is selected
                                        {
                                            machineScript.isMachineActive = true;
                                            machineScript.moveSupport = true;
                                            //isZeroReturnClicked = false;
                                            //isAllClicked = false;
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
                                                drillController.targetCounter = 6;
                                                machineScript.moveCutObject1ToCuttingPosition();
                                                whichCutItemWasLathed = "CutObject1";
                                            break;

                                            case 1: // Program #1
                                                drillController.targetCounter = 4;
                                                machineScript.moveCutObject2ToCuttingPosition();
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
                                    // Handle interaction for btn_FeedHold
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
                                        controlpanelController.updateScreenImage();

                                        objectiveManager.CompleteObjective("Turn the lathe off");
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

                                case "btnListProgram":
                                    if(isLatheOn && isAllClicked)
                                    {
                                        controlpanelController.isProgramSelectionActive = true;
                                        controlpanelController.showHomeScreen2 = false;
                                        controlpanelController.updateScreenImage();
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

                                        controlpanelController.updateScreenImage();
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

                                        controlpanelController.updateScreenImage();
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

    public IEnumerator handleJogButtonPressDelay()
    {
        yield return new WaitForSeconds(0.4f);
        canPressHandleJogButton = true;
    }

    public IEnumerator startupSequence()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        controlpanelController.showBlackScreen = true;
        controlpanelController.updateScreenImage();
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        controlpanelController.showAttentionScreen = true;
        controlpanelController.showBlackScreen = false;
        controlpanelController.updateScreenImage();
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        controlpanelController.showHomeScreen1 = true;
        controlpanelController.showAttentionScreen = false;
        controlpanelController.updateScreenImage();
        isStartUpSequenceDone = true;
    }
}
