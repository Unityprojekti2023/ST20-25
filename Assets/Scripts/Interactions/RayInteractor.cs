using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class RayInteractor : MonoBehaviour
{
    
    public Camera tempCamera;


    public GameObject GameController;
    public CameraController CameraController;

    public TextMeshProUGUI interactText;
    public ScrapInteraction scrapInteraction;
    public CleaningFeature cleaningFeature;
    public ObjectiveManager objectiveManager;
    public TextInformation textInformation;
    public float interactDistance = 80f;

    float holdDuration = 1.5f; // Adjust the duration as needed
    float currentHoldTime = 0f;
    bool carcassInteracted = false;
    bool glassesInteracted = false;
    public bool shovelEquipped = false;
    public bool LockerDoorOpen = false;
    public bool caliperEquipped = false;
    bool canInteractAgain = true;
    public int scrapPilesThrownIntoCorrectTrashbin = 0;
    public int scrapPilesThrownIntoWrongTrashbin = 0;

    private void Start()
    {
        //GameController = GameObject.FindGameObjectWithTag("GameController");
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

    private void Update()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null && mainCamera.CompareTag("MainCamera"))
        {
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    HandleInteraction(hit.collider.gameObject.name, interactable);
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

        if (shovelEquipped && scrapInteraction.isPlayerNearScrapPiles && !scrapInteraction.isShovelFull && DoorController.Instance.isDoorOpen)
        {
            if (cleaningFeature.isPile1Visible || cleaningFeature.isPile2Visible || cleaningFeature.isPile3Visible)
            {
                ShowInteractText($"Press to pick up scraps: [LMB] or [E]");
            }
        }
    }

    private void HandleInteraction(string targetName, IInteractable interactable)
    {
        switch (targetName)
        {
            case "ST20-25 Luukku":
                HandleDoorInteraction(interactable);
                break;
            case "ControlpanelTrigger":
                HandleGenericInteraction(interactable, "Inspect panel: [LMB] or [E]");
                break;
            case "ST20-25 Puristin":
                HandlePlaceRemovePieceInteraction(interactable);
                break;
            case "AluminumBlankPile":
                HandleGenericInteraction(interactable, "Pick up Item: [LMB] or [E]");
                break;
            case "SteelBlankPile":
                HandleGenericInteraction(interactable, "Pick up Item: [LMB] or [E]");
                break;
            case "PlacementMat":
                HandleGenericInteraction(interactable, "Place Item: [LMB] or [E]");
                break;
            case "Clipboard":
                HandleGenericInteraction(interactable, "Pick up Clipboard: [LMB] or [E]");
                break;
            case "Measurements":
                HandleMeasurementInteraction(interactable);
                break;
            case "Carcass":
                HandleHoldInteraction(interactable, ref carcassInteracted, "Hold to put shoes on: [LMB] or [E]");
                break;
            case "Safetyglasses":
                HandleHoldInteraction(interactable, ref glassesInteracted, "Hold to put safetyglasses on: [LMB] or [E]");
                break;
            case "Shovel":
                HandleEquipInteraction(interactable, ref shovelEquipped, "Hold to equip shovel: [LMB] or [E]", "Hold to unequip shovel: [LMB] or [E]");
                break;
            case "CaliperBox":
                HandleEquipInteraction(interactable, ref caliperEquipped, "Hold to equip caliber: [LMB] or [E]", "Hold to unequip caliber: [LMB] or [E]");
                break;
            case "SteelTrashCan":
                HandleScrapThrowInteraction(interactable, ref scrapPilesThrownIntoWrongTrashbin, "Wrong trash bin! -50 points", "Wrong trash bin! -100 points", 3, 100, 50, false);
                break;
            case "AluminiumTrashCan":
                HandleScrapThrowInteraction(interactable, ref scrapPilesThrownIntoCorrectTrashbin, null, null, 3, 0, 0, true);
                break;
            default:
                HideInteractText();
                break;
        }
    }

    private void HandleDoorInteraction(IInteractable interactable)
    {
        if (!shovelEquipped && !caliperEquipped)
        {
            if (!DoorController.Instance.isDoorOpen)
            {
                ShowInteractText($"Open door: [LMB] or [E]");
            }
            else
            {
                ShowInteractText($"Close door: [LMB] or [E]");
            }
            CheckAndInteract(interactable);
        }
    }

    private void HandleGenericInteraction(IInteractable interactable, string text)
    {
        if (!shovelEquipped && !caliperEquipped)
        {
            ShowInteractText(text);
            CheckAndInteract(interactable);
        }
    }

    private void HandlePlaceRemovePieceInteraction(IInteractable interactable)
    {
        if (!shovelEquipped && !caliperEquipped)
        {
            ShowInteractText($"Place/Remove piece: [LMB] or [E]");
            CheckAndInteract(interactable);
        }
    }

    private void HandleMeasurementInteraction(IInteractable interactable)
    {
        if (caliperEquipped)
        {
            ShowInteractText($"Measure Item: [LMB] or [E]");
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
            {
                interactable.Interact();
                CameraController.ActivateCaliperCamera(tempCamera);
            }
        }
    }

    private void HandleHoldInteraction(IInteractable interactable, ref bool interacted, string text)
    {
        if (!interacted && !shovelEquipped && !caliperEquipped)
        {
            ShowInteractText(text);
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
            {
                ShowInteractText($"Time Left: {holdDuration - currentHoldTime:F1}s");
                currentHoldTime += Time.deltaTime;
                if (currentHoldTime >= holdDuration)
                {
                    interactable.Interact();
                    interacted = true;
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
    }

    private void HandleEquipInteraction(IInteractable interactable, ref bool equipped, string equipText, string unequipText)
    {
        if (!equipped && canInteractAgain)
        {
            ShowInteractText(equipText);
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
            {
                interactable.Interact();
                equipped = true;
                canInteractAgain = false;
                StartCoroutine(InteractionDelay());
                ResetHoldTimer();
            }
            else
            {
                ResetHoldTimer();
            }
        }
        else if (equipped && canInteractAgain)
        {
            ShowInteractText(unequipText);
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
            {
                ShowInteractText($"Time Left: {holdDuration - currentHoldTime:F1}s");
                currentHoldTime += Time.deltaTime;
                if (currentHoldTime >= holdDuration)
                {
                    interactable.Interact();
                    equipped = false;
                    canInteractAgain = false;
                    StartCoroutine(InteractionDelay());
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
    }

    private void HandleScrapThrowInteraction(IInteractable interactable, ref int scrapCount, string singleThrowText, string multipleThrowText, int threshold, int multipleThrowPenalty, int singleThrowPenalty, bool correctBin)
    {
        if (scrapInteraction.isShovelFull && shovelEquipped)
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
                    scrapCount++;
                    ResetHoldTimer();

                    if (!correctBin)
                    {
                        if (scrapCount == threshold)
                        {
                            objectiveManager.DeductPoints(multipleThrowPenalty);
                            textInformation.UpdateText(multipleThrowText);
                        }
                        else
                        {
                            objectiveManager.DeductPoints(singleThrowPenalty);
                            textInformation.UpdateText(singleThrowText);
                        }
                    }
                    else if (scrapCount == threshold)
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
    }

    private void CheckAndInteract(IInteractable interactable)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.E))
        {
            interactable.Interact();
        }
    }

    private void ResetHoldTimer()
    {
        currentHoldTime = 0f;
    }

    private void ShowInteractText(string text)
    {
        interactText.text = text;
        interactText.gameObject.SetActive(true);
    }

    private void HideInteractText()
    {
        ResetHoldTimer(); // Reset the timer when hiding the text
        interactText.gameObject.SetActive(false);
    }

    private IEnumerator InteractionDelay()
    {
        yield return new WaitForSeconds(1f);
        canInteractAgain = true;
    }
}
