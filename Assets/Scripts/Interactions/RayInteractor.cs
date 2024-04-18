using UnityEngine;
using TMPro;
using System.Collections;

public class RayInteractor : MonoBehaviour
{
    public GameObject GameController;
    public CameraController CameraController;

    public TextMeshProUGUI interactText;
    public ScrapInteraction scrapInteraction;
    public CleaningFeature cleaningFeature;
    public DoorController doorController;
    public ObjectiveManager objectiveManager;
    public TextInformation textInformation;
    public LockerController lockerController;
    public float interactDistance = 80f;
    
    float holdDuration = 1.5f; // Adjust the duration as needed
    float currentHoldTime = 0f;
    bool carcassInteracted = false;
    bool glassesInteracted = false;
    public bool shovelEquipped = false;
    public bool LockerDoorOpen = false;
    public bool caliperEquipped = false;
    public bool itemPlaced = false;
    bool canInteractAgain = true;
    public int scrapPilesThrownIntoCorrectTrashbin = 0;
    public int scrapPilesThrownIntoWrongTrashbin = 0;

    private void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController");
        if (GameController != null)
        {
            // Get the CameraController script attached to the GameController object
            CameraController = GameController.GetComponent<CameraController>();
        }
        else
        {
            Debug.LogError("GameController object not found!");
        }
    }
    void Update()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null && mainCamera.CompareTag("MainCamera"))
        {
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                string targetName = hit.collider.gameObject.name;
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    //ShowInteractText(targetName);
                    switch (targetName)
                    {
                        case "ST20-25 Luukku":
                            if(!shovelEquipped && !doorController.isDoorOpen && !caliperEquipped)
                            {
                                ShowInteractText($"Open door: [LMB] or [E]");
                                CheckAndInteract(interactable);
                            } 

                            if(!shovelEquipped && doorController.isDoorOpen && !caliperEquipped)
                            {
                                ShowInteractText($"Close door: [LMB] or [E]");
                                CheckAndInteract(interactable);
                            }
                            break;
                        
                        case "ControlpanelTrigger":
                            if(!shovelEquipped && !caliperEquipped)
                            {
                                ShowInteractText($"Inspect panel: [LMB] or [E]");
                                CheckAndInteract(interactable);
                            }

                            break;

                        case "ST20-25 Puristin":
                            if(!shovelEquipped && !caliperEquipped)
                            {
                                ShowInteractText($"Place/Remove piece: [LMB] or [E]");
                                CheckAndInteract(interactable);
                                itemPlaced = true;
                            }
                            break;

                        case "ItemPile":
                            if(!shovelEquipped && !caliperEquipped)
                            {
                                ShowInteractText($"Pick up Item: [LMB] or [E]");
                                CheckAndInteract(interactable);
                            }
                            
                            break;

                        case "ItemPlacementSpot":
                            if(!shovelEquipped && !caliperEquipped)
                            {
                                ShowInteractText($"Place Item: [LMB] or [E]");
                                CheckAndInteract(interactable);
                            }
                            break;

                        case "Measurements":
                            if (caliperEquipped)
                            {
                                ShowInteractText($"Measure Item: [LMB] or [E]");
                                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
                                {
                                    interactable.Interact();
                                    CameraController.CaliperCameraActive();
                                }

                            }
                            break;
                        //case "LockerDoor":
                        //    if (!LockerDoorOpen)
                        //    {
                        //        ShowInteractText($"Hold to open door: [LMB] or [E]");

                        //        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
                        //        {
                        //            ShowInteractText($"Time Left: {holdDuration - currentHoldTime:F1}s");
                        //            currentHoldTime += Time.deltaTime;

                        //            if (currentHoldTime >= holdDuration)
                        //            {
                        //                interactable.Interact();
                        //                LockerDoorOpen = true;
                        //                ResetHoldTimer();
                        //            }
                        //        }
                        //        else
                        //        {
                        //            ResetHoldTimer();
                        //        }
                        //    }
                        //    if (LockerDoorOpen)
                        //    {
                        //        ShowInteractText($"Hold to close door: [LMB] or [E]");

                        //        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
                        //        {
                        //            ShowInteractText($"Time Left: {holdDuration - currentHoldTime:F1}s");
                        //            currentHoldTime += Time.deltaTime;

                        //            if (currentHoldTime >= holdDuration)
                        //            {
                        //                interactable.Interact();
                        //                LockerDoorOpen = false;
                        //                ResetHoldTimer();
                        //            }
                        //        }
                        //        else
                        //        {
                        //            ResetHoldTimer();
                        //        }
                        //    }
                        //    break;

                        case "Carcass":
                            if (!carcassInteracted && !shovelEquipped && !caliperEquipped)
                            {
                                ShowInteractText($"Hold to put shoes on: [LMB] or [E]");

                                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
                                {
                                    ShowInteractText($"Time Left: {holdDuration - currentHoldTime:F1}s");
                                    currentHoldTime += Time.deltaTime;

                                    if (currentHoldTime >= holdDuration)
                                    {
                                        interactable.Interact();
                                        carcassInteracted = true; // Set the flag to true after successful interaction
                                        ResetHoldTimer();
                                    }
                                }
                                else
                                {
                                    ResetHoldTimer();
                                }
                            }
                            else
                            {
                                HideInteractText();
                            }
                            break;

                        case "Safetyglasses":
                            if (!glassesInteracted && !shovelEquipped && !caliperEquipped)
                            {
                                ShowInteractText($"Hold to put safetyglasses on: [LMB] or [E]");

                                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
                                {
                                    ShowInteractText($"Time Left: {holdDuration - currentHoldTime:F1}s");
                                    currentHoldTime += Time.deltaTime;

                                    if (currentHoldTime >= holdDuration)
                                    {
                                        interactable.Interact();
                                        glassesInteracted = true; // Set the flag to true after successful interaction
                                        ResetHoldTimer();
                                    }
                                }
                                else
                                {
                                    ResetHoldTimer();
                                }
                            }
                            else
                            {
                                HideInteractText();
                            }
                            break;

                        case "Shovel":
                            if(!shovelEquipped && canInteractAgain && !caliperEquipped)
                            {
                                ShowInteractText($"Hold to equip shovel: [LMB] or [E]");

                                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
                                {
                                    ShowInteractText($"Time Left: {holdDuration - currentHoldTime:F1}s");
                                    currentHoldTime += Time.deltaTime;

                                    if (currentHoldTime >= holdDuration)
                                    {
                                        interactable.Interact();
                                        shovelEquipped = true;
                                        canInteractAgain = false;
                                        StartCoroutine(interactionDelay());
                                        ResetHoldTimer();
                                    }
                                }
                                else
                                {
                                    ResetHoldTimer();
                                }
                            }
                            else if (shovelEquipped && canInteractAgain && !caliperEquipped)
                            {
                                ShowInteractText($"Hold to unequip shovel: [LMB] or [E]");

                                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
                                {
                                    ShowInteractText($"Time Left: {holdDuration - currentHoldTime:F1}s");
                                    currentHoldTime += Time.deltaTime;

                                    if (currentHoldTime >= holdDuration)
                                    {
                                        interactable.Interact();
                                        shovelEquipped = false;
                                        canInteractAgain = false;
                                        StartCoroutine(interactionDelay());
                                        ResetHoldTimer();
                                    }
                                }
                                else
                                {
                                    ResetHoldTimer();
                                }
                            } else {
                                HideInteractText();
                            }


                            
                        break;

                        case "CaliperBox":
                            if (!caliperEquipped && canInteractAgain)
                            {
                                ShowInteractText($"Hold to equip caliber: [LMB] or [E]");

                                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
                                {
                                    ShowInteractText($"Time Left: {holdDuration - currentHoldTime:F1}s");
                                    currentHoldTime += Time.deltaTime;

                                    if (currentHoldTime >= holdDuration)
                                    {
                                        interactable.Interact();
                                        caliperEquipped = true;
                                        canInteractAgain = false;
                                        StartCoroutine(interactionDelay());
                                        ResetHoldTimer();
                                    }
                                }
                                else
                                {
                                    ResetHoldTimer();
                                }
                            }
                            else if (caliperEquipped && canInteractAgain)
                            {
                                ShowInteractText($"Hold to unequip caliber: [LMB] or [E]");

                                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
                                {
                                    ShowInteractText($"Time Left: {holdDuration - currentHoldTime:F1}s");
                                    currentHoldTime += Time.deltaTime;

                                    if (currentHoldTime >= holdDuration)
                                    {
                                        interactable.Interact();
                                        caliperEquipped = false;
                                        canInteractAgain = false;
                                        StartCoroutine(interactionDelay());
                                        ResetHoldTimer();
                                    }
                                }
                                else
                                {
                                    ResetHoldTimer();
                                }
                            }
                            else
                            {
                                HideInteractText();
                            }



                            break;

                        case "SteelTrashCan":
                            if(scrapInteraction.isShovelFull && shovelEquipped)
                            {
                                ShowInteractText($"Hold to throw scraps: [LMB] or [E]");

                                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
                                {
                                    ShowInteractText($"Time Left: {holdDuration - currentHoldTime:F1}s");
                                    currentHoldTime += Time.deltaTime;

                                    if (currentHoldTime >= holdDuration)
                                    {
                                        interactable.Interact();
                                        scrapInteraction.isShovelFull = false;
                                        ResetHoldTimer();
                                        scrapPilesThrownIntoWrongTrashbin++;

                                        if(scrapPilesThrownIntoWrongTrashbin == 3)
                                        {
                                            objectiveManager.DeductPoints(100); //Removing another 100 points if all 3 scrap piles were thrown into the wrong trash bin (for a total of -200 points)
                                            textInformation.UpdateText("Wrong trash bin! -100 points");
                                        } 
                                        else {
                                            objectiveManager.DeductPoints(50); //Removing 50 points for throwing the metal scraps into the wrong trash bin
                                            textInformation.UpdateText("Wrong trash bin! -50 points");
                                        }
                                    }
                                }
                                else
                                {
                                    ResetHoldTimer();
                                }
                            }
                        break;

                        case "AluminiumTrashCan":
                            if(scrapInteraction.isShovelFull && shovelEquipped)
                            {
                                ShowInteractText($"Hold to throw scraps: [LMB] or [E]");

                                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
                                {
                                    ShowInteractText($"Time Left: {holdDuration - currentHoldTime:F1}s");
                                    currentHoldTime += Time.deltaTime;

                                    if (currentHoldTime >= holdDuration)
                                    {
                                        interactable.Interact();
                                        scrapInteraction.isShovelFull = false;
                                        ResetHoldTimer();
                                        scrapPilesThrownIntoCorrectTrashbin++;
                                        
                                        if(scrapPilesThrownIntoCorrectTrashbin == 3)
                                        {
                                            objectiveManager.CompleteObjective("Clean metal scraps");
                                        } 
                                    }
                                }
                                else
                                {
                                    ResetHoldTimer();
                                }
                            }
                        break;

                        default:
                            // For other cases, just hide the text
                            HideInteractText();
                        break;
                    }

                }
                else
                {
                    ResetHoldTimer();
                    HideInteractText();
                }
            }
            else
            {
                HideInteractText();
            }
        }
        else
        {
            HideInteractText();
        }

        if(shovelEquipped && scrapInteraction.isPlayerNearScrapPiles && !scrapInteraction.isShovelFull && doorController.isDoorOpen)
        {
            if(cleaningFeature.isPile1Visible || cleaningFeature.isPile2Visible || cleaningFeature.isPile3Visible)
            {
                ShowInteractText($"Press to pick up scraps: [LMB] or [E]");
            }
        }
    }

    private void CheckAndInteract(IInteractable interactable)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.E))
        {
            interactable.Interact();
        }
    }
    void ResetHoldTimer()
    {
        currentHoldTime = 0f;
    }

    void ShowInteractText(string text)
    {
        interactText.text = text;
        interactText.gameObject.SetActive(true);
    }

    void HideInteractText()
    {
        ResetHoldTimer(); // Reset the timer when hiding the text
        interactText.gameObject.SetActive(false);
    }

    IEnumerator interactionDelay()
    {
        yield return new WaitForSeconds(1f);
        canInteractAgain = true;
    }
}