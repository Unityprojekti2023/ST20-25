using UnityEngine;
using TMPro;

public class RayInteractor : MonoBehaviour
{
    public TextMeshProUGUI interactText;
    public float interactDistance = 80f;

    float holdDuration = 2f; // Adjust the duration as needed
    float currentHoldTime = 0f;
    bool holdingButton = false;
    bool carcassInteracted = false;
    bool glassesInteracted = false;

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
                            ShowInteractText($"Open door: [LMB] or [E]");
                            CheckAndInteract(interactable);
                            break;

                        case "ControlpanelTrigger":
                            ShowInteractText($"Inspect panel: [LMB] or [E]");
                            CheckAndInteract(interactable);
                            break;

                        case "ST20-25 Puristin":
                            ShowInteractText($"Place/Remove piece: [LMB] or [E]");
                            CheckAndInteract(interactable);
                            break;

                        case "ItemPile":
                            ShowInteractText($"Pick up Item: [LMB] or [E]");
                            CheckAndInteract(interactable);
                            break;

                        case "ItemPlacementSpot":
                            ShowInteractText($"Place Item: [LMB] or [E]");
                            CheckAndInteract(interactable);
                            break;

                        case "Carcass":
                            if (!carcassInteracted)
                            {
                                ShowInteractText($"Hold to put shoes on: [LMB] or [E]");

                                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
                                {
                                    ShowInteractText($"Time Left: {holdDuration - currentHoldTime:F1}s");
                                    holdingButton = true;
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
                            if (!glassesInteracted)
                            {
                                ShowInteractText($"Hold to put shoes on: [LMB] or [E]");

                                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
                                {
                                    ShowInteractText($"Time Left: {holdDuration - currentHoldTime:F1}s");
                                    holdingButton = true;
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
        holdingButton = false;
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
}