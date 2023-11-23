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
                    switch(targetName)
                    {
                        case "ST20-25 Luukku":
                            ShowInteractText("Open Door : [Mouse 1]");
                            break;
                        case "ControlpanelTrigger":
                            ShowInteractText("Inspect panel : [Mouse 1]");
                            break;
                        case "ST20-25 Puristin":
                            ShowInteractText("Place/Remove piece : [Mouse 1]");
                            break;
                        case "ItemPile":
                            ShowInteractText("Pickup item : [Mouse 1]");
                            break;
                        case "ItemPlacementSpot":
                            ShowInteractText("Place item : [Mouse 1]");
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
