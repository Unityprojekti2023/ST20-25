using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class RayInteractor : MonoBehaviour
{
    public TextMeshProUGUI interactText;
    public TextInformation textInformation;
    public float interactDistance = 100f;

    private float holdDuration = 1.5f; // Adjust the duration as needed
    private float currentHoldTime = 0f;

    private Dictionary<string, System.Action<IInteractable>> interactableActions;

    void Start()
    {
        // Initialize the dictionary with object names and corresponding actions
        interactableActions = new Dictionary<string, System.Action<IInteractable>>
        {
            { "ST20-25 Luukku", interactable => HandleInteraction(interactable, "Open door: [LMB] or [E]")},
            { "ControlpanelTrigger", interactable => HandleInteraction(interactable, "Inspect panel: [LMB] or [E]") },
            { "AttachmentPointLathe", interactable => HandleInteraction(interactable, "Place item: [LMB] or [E]")},
            { "AluminumBlank", interactable => HandleInteraction(interactable, "Pick up Item: [LMB] or [E]") },
            { "SteelBlank", interactable => HandleInteraction(interactable, "Pick up Item: [LMB] or [E]") },
            { "PlacementMat", interactable => HandleInteraction(interactable, "Place Item: [LMB] or [E]") },
            { "Clipboard", interactable => HandleInteraction(interactable, "Pick up Clipboard: [LMB] or [E]") },
            { "ClipboardPlacementPosition", interactable => HandleInteraction(interactable, "Place Clipboard: [LMB] or [E]") },
            { "Measurements", interactable => HandleInteraction(interactable, "Inspect Measurements: [LMB] or [E]")},
            { "Carcass", interactable => HandleHoldInteraction(interactable, "Hold to put shoes on: [LMB] or [E]") },
            { "Safetyglasses", interactable => HandleHoldInteraction(interactable, "Hold to put safetyglasses on: [LMB] or [E]") },
            { "Shovel", interactable => HandleHoldInteraction(interactable, "Hold to equip shovel: [LMB] or [E]") },
            { "CaliperBox", interactable => HandleHoldInteraction(interactable, "Hold to equip caliper: [LMB] or [E]") }
        };
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
                    HandleInteractionRays(hit.collider.gameObject.name, interactable);
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
    }

    private void HandleInteractionRays(string targetName, IInteractable interactable)
    {
        if (interactableActions.ContainsKey(targetName))
        {
            interactableActions[targetName](interactable);
        }
        else
        {
            HideInteractText();
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
        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
        {
            ShowInteractText($"Time Left: {holdDuration - currentHoldTime:F1}s");
            currentHoldTime += Time.deltaTime;
            if (currentHoldTime >= holdDuration)
            {
                interactable.Interact();
                ResetHoldTimer();
            }
        }
        else
        {
            ResetHoldTimer();
        }
    }

    //Update interaction text of specific string Key in the dictionary
    public void UpdateInteractionText(string key, string text)
    {
        if (interactableActions.ContainsKey(key))
        {
            interactableActions[key] = interactable => HandleInteraction(interactable, text);
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
}
