using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;

public class VRRayInteractor : MonoBehaviour
{
    public static VRRayInteractor instance;

    [Header("VR Controller")]
    public ActionBasedController rightHandController; // Assign your right controller in the inspector
    public LineRenderer rayLine; // Optional for visualizing the ray

    [Header("Cleaning Controller")]
    public CleaningController cleaningController;

    [Header("Interact Text")]
    public TextMeshProUGUI interactText;
    private readonly float interactDistance = 500f;
    private readonly float holdDuration = 0f; // hold wouldn't continue
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
        if (rightHandController == null)
        {
            Debug.LogError("Right Hand Controller not assigned! Assign it in the Inspector.");
        }

        if (cleaningController == null)
        {
            cleaningController = FindObjectOfType<CleaningController>();
        }

        // Initialize the dictionary
        interactableActions = new Dictionary<string, System.Action<IInteractable>>
        {
            { "ST20-25 Luukku", interactable => HandleInteraction(interactable, "Open lathe door: Trigger") },
            { "ControlpanelTrigger", interactable => HandleInteraction(interactable, "Inspect panel: Trigger") },
            { "AttachmentPointLathe", interactable => HandleInteraction(interactable, "Place held item: Trigger") },
            { "AluminumBlank", interactable => HandleInteraction(interactable, "Pick up blank: Trigger") },
            { "SteelBlank", interactable => HandleInteraction(interactable, "Pick up blank: Trigger") },
            { "PlacementMat", interactable => HandleInteraction(interactable, "Place cut piece: Trigger") },
            { "TurnInMat", interactable => HandleInteraction(interactable, "Turn in cut piece: Trigger") },
            { "Clipboard", interactable => HandleInteraction(interactable, "Pick up clipboard: Trigger") },
            { "ClipboardPlacementPosition", interactable => HandleInteraction(interactable, "Place clipboard: Trigger") },
            { "Measurements", interactable => HandleInteraction(interactable, "Measure: Trigger") },
            { "Locker", interactable => HandleInteraction(interactable, "Open locker door: Trigger") },
            { "Shoes", interactable => HandleHoldInteraction(interactable, "Hold to equip shoes: Trigger") },
            { "Safetyglasses", interactable => HandleHoldInteraction(interactable, "Hold to equip safetyglasses: Trigger") },
            { "ShovelOrigin", interactable => HandleHoldInteraction(interactable, "Hold to equip shovel: Trigger") },
            { "CaliperBox", interactable => HandleHoldInteraction(interactable, "Hold to pickup caliper: Trigger") },
            { "ExitDoor", interactable => HandleInteraction(interactable, "End run: Trigger") }
        };
    }

    private void Update()
    {
        if (rightHandController != null && rightHandController.selectAction.action.triggered && Time.timeScale > 0)
        {
            // Get ray from controller
            Ray ray = new Ray(rightHandController.transform.position, rightHandController.transform.forward);

            if (rayLine != null)
            {
                // Visualize ray
                rayLine.SetPosition(0, ray.origin);
                rayLine.SetPosition(1, ray.origin + ray.direction * interactDistance);
            }

            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    HandleInteractionRays(hit.collider.gameObject.name, interactable);
                }
                else if (hit.collider.CompareTag("Cleanable"))
                {
                    ShowInteractText("Clean scrap pile: Trigger");
                    cleaningController.HandleCleaning(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("TrashCan"))
                {
                    ShowInteractText("Throw away scrap pile: Trigger");
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
                ResetHoldTimer();
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

        if (rightHandController.selectAction.action.triggered)
        {
            interactable.Interact();
        }
    }

    private void HandleHoldInteraction(IInteractable interactable, string text)
    {
        ShowInteractText(text);

        if (rightHandController.selectAction.action.triggered)
        {
            StartCoroutine(HoldInteractionCoroutine(interactable));
        }
    }

    private IEnumerator HoldInteractionCoroutine(IInteractable interactable)
    {
        while (rightHandController.selectAction.action.triggered)
        {
            currentHoldTime += Time.deltaTime;
            ShowInteractText($"Time Left: {holdDuration - currentHoldTime:F1}s");

            if (currentHoldTime >= holdDuration)
            {
                interactable.Interact();
                break;
            }

            yield return null;
        }

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
