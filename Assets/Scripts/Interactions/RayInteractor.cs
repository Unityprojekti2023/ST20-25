using UnityEngine;
using TMPro;

public class RayInteractor : MonoBehaviour
{
    public TextMeshProUGUI interactText;
    public float interactDistance = 80f;

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
                            ShowInteractText("Open Door : [LMB] or [E]");
                            break;
                        case "ControlpanelTrigger":
                            ShowInteractText("Inspect panel : [LMB] or [E]");
                            break;
                        case "ST20-25 Puristin":
                            ShowInteractText("Place/Remove piece : [LMB] or [E]");
                            break;
                        case "ItemPile":
                            ShowInteractText("Pickup item : [LMB] or [E]");
                            break;
                        case "ItemPlacementSpot":
                            ShowInteractText("Place item : [LMB] or [E]");
                            break;
                        case "Door Frame.001":
                            ShowInteractText("Exit game : [LMB] or [E]");
                            break;

                    }

                    if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.E))
                    {
                        interactable.Interact();
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
        else
        {
            HideInteractText();
        }
    }

    void ShowInteractText(string text)
    {
        interactText.text = text;
        interactText.gameObject.SetActive(true);
    }

    void HideInteractText()
    {
        interactText.gameObject.SetActive(false);
    }
}
