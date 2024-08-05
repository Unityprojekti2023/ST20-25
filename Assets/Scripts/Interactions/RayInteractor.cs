using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using System.Collections;
using Unity.VisualScripting;
public enum InteractableType
{
    HandleInteraction,
    HandleHoldInteraction
}
public class RayInteractor : MonoBehaviour
{
    public static RayInteractor instance;

    [Header("Cleaning Controller")]
    public CleaningController cleaningController;

    [Header("Interact Text")]
    public TextMeshProUGUI interactText;
    private readonly float interactDistance = 120f;
    private readonly float holdDuration = 1.5f; // Adjust the duration as needed
    private float currentHoldTime = 0f;

    private Dictionary<string, System.Action<IInteractable>> interactableActions;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        if (cleaningController == null)
        {
            cleaningController = FindObjectOfType<CleaningController>();
        }

        // Initialize the dictionary with object names and corresponding actions
        interactableActions = new Dictionary<string, System.Action<IInteractable>>
        {
            { "ST20-25 Luukku", interactable => HandleInteraction(interactable, "Open lathe door: [LMB] or [E]")},
            { "ControlpanelTrigger", interactable => HandleInteraction(interactable, "Inspect panel: [LMB] or [E]") },
            { "AttachmentPointLathe", interactable => HandleInteraction(interactable, "Place held item: [LMB] or [E]")},
            { "AluminumBlank", interactable => HandleInteraction(interactable, "Pick up blank: [LMB] or [E]") },
            { "SteelBlank", interactable => HandleInteraction(interactable, "Pick up blank: [LMB] or [E]") },
            { "PlacementMat", interactable => HandleInteraction(interactable, "Place cut piece: [LMB] or [E]") },
            { "TurnInMat", interactable => HandleInteraction(interactable, "Turn in cut piece: [LMB] or [E]") },
            { "Clipboard", interactable => HandleInteraction(interactable, "Pick up clipboard: [LMB] or [E]") },
            { "ClipboardPlacementPosition", interactable => HandleInteraction(interactable, "Place clipboard: [LMB] or [E]") },
            { "Measurements", interactable => HandleInteraction(interactable, "Measure: [LMB] or [E]")},
            { "Locker", interactable => HandleInteraction(interactable, "Open locker door: [LMB] or [E]")},
            { "Shoes", interactable => HandleHoldInteraction(interactable, "Hold to equip shoes: [LMB] or [E]") },
            { "Safetyglasses", interactable => HandleHoldInteraction(interactable, "Hold to equip safetyglasses: [LMB] or [E]") },
            { "ShovelOrigin", interactable => HandleHoldInteraction(interactable, "Hold to equip shovel: [LMB] or [E]") },
            { "CaliperBox", interactable => HandleHoldInteraction(interactable, "Hold to pickup caliper: [LMB] or [E]") },
            { "ExitDoor", interactable => HandleInteraction(interactable, "End run: [LMB] or [E]") }
        };
    }

    private void Update()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null && mainCamera.CompareTag("MainCamera") && Time.timeScale > 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    HandleInteractionRays(hit.collider.gameObject.name, interactable);
                }
                else if (hit.collider.CompareTag("Cleanable"))
                {
                    ShowInteractText("Clean scrap pile: [LMB]");
                    cleaningController.HandleCleaning(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("TrashCan"))
                {
                    if (InventoryManager.Instance.IsItemInInventory("Shovel"))
                    {
                        ShowInteractText("Throw away scrap pile: [LMB]");
                    }
                    else
                    {
                        ShowInteractText("Throw away held piece: [LMB]");
                    }
                    cleaningController.HandleCleaning(hit.collider.gameObject);
                }
                else
                {
                    HideInteractText();
                }
            }
            else
            {
                HideInteractText();
                // Check if mouse button is down to cancel the hold interaction
                if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.E))
                {
                    // Break the hold interaction process
                    StopAllCoroutines();
                }
            }
        }
        else
        {
            HideInteractText();
        }
    }

    private void HandleInteractionRays(string targetName, IInteractable interactable)
    {
        // If key contains any part of the target name, show the interaction text
        if (interactableActions.ContainsKey(targetName))
        {
            interactableActions[targetName](interactable);
        }
        else
        {
            HideInteractText();
        }
    }

    //Update interaction text of specific string Key in the dictionary
    public void UpdateInteractionText(string key, string text, InteractableType interactableType = InteractableType.HandleInteraction)
    {
        if (interactableActions.ContainsKey(key))
        {
            switch (interactableType)
            {
                case InteractableType.HandleInteraction:
                    interactableActions[key] = interactable => HandleInteraction(interactable, text);
                    break;
                case InteractableType.HandleHoldInteraction:
                    interactableActions[key] = interactable => HandleHoldInteraction(interactable, text);
                    break;
                default:
                    Debug.LogError("Invalid InteractableType");
                    break;
            }
        }
    }

    private void HandleInteraction(IInteractable interactable, string text)
    {
        ShowInteractText(text);
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.E))
        {
            interactable.Interact();
        }
    }

    private void HandleHoldInteraction(IInteractable interactable, string text)
    {
        ShowInteractText(text);

        // Check if the mouse button or key is pressed to start holding
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Hold Interaction");
            // Start the hold interaction process
            StartCoroutine(HoldInteractionCoroutine(interactable));
        }
    }

    private IEnumerator HoldInteractionCoroutine(IInteractable interactable)
    {
        while (Input.GetMouseButton(0) || Input.GetKey(KeyCode.E))
        {
            // Increment the hold timer based on the elapsed time
            currentHoldTime += Time.deltaTime;
            ShowInteractText($"Time Left: {holdDuration - currentHoldTime:F1}s");

            if (currentHoldTime >= holdDuration)
            {
                interactable.Interact(); // Trigger the interaction
                break; // Exit the coroutine
            }

            yield return null; // Wait until the next frame
        }

        // Reset the hold timer once the interaction is complete or the button is released
        ResetHoldTimer();
        HideInteractText();
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
        interactText.gameObject.SetActive(false);
    }
}
