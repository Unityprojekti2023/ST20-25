using UnityEngine;
using TMPro;
using System.Collections;

public class RayInteractor : MonoBehaviour
{
    public TextMeshProUGUI interactText;
    public ScrapInteraction scrapInteraction;
    public CleaningFeature cleaningFeature;
    public DoorController doorController;
    public float interactDistance = 80f;

    float holdDuration = 1.5f; // Adjust the duration as needed
    float currentHoldTime = 0f;
    bool carcassInteracted = false;
    bool glassesInteracted = false;
    public bool shovelEquipped = false;
    bool canInteractAgain = true;
    public int scrapPilesThrownIntoCorrectTrashbin = 0;

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
                            if(!shovelEquipped && !doorController.isDoorOpen)
                            {
                                ShowInteractText($"Open door: [LMB] or [E]");
                                CheckAndInteract(interactable);
                            } 

                            if(!shovelEquipped && doorController.isDoorOpen)
                            {
                                ShowInteractText($"Close door: [LMB] or [E]");
                                CheckAndInteract(interactable);
                            }
                            break;

                        case "ControlpanelTrigger":
                            if(!shovelEquipped)
                            {
                                ShowInteractText($"Inspect panel: [LMB] or [E]");
                                CheckAndInteract(interactable);
                            }

                            break;

                        case "ST20-25 Puristin":
                            if(!shovelEquipped)
                            {
                                ShowInteractText($"Place/Remove piece: [LMB] or [E]");
                                CheckAndInteract(interactable);
                            }
                            break;

                        case "ItemPile":
                            if(!shovelEquipped)
                            {
                                ShowInteractText($"Pick up Item: [LMB] or [E]");
                                CheckAndInteract(interactable);
                            }
                            break;

                        case "ItemPlacementSpot":
                            if(!shovelEquipped)
                            {
                                ShowInteractText($"Place Item: [LMB] or [E]");
                                CheckAndInteract(interactable);
                            }
                            break;

                        case "Carcass":
                            if (!carcassInteracted && !shovelEquipped)
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
                            if (!glassesInteracted && !shovelEquipped)
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
                            if(!shovelEquipped && canInteractAgain)
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
                            else if (shovelEquipped && canInteractAgain)
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

        if(shovelEquipped && scrapInteraction.isPlayerNearScrapPiles && !scrapInteraction.isShovelFull)
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