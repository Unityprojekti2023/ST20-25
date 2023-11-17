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
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    ShowInteractText();

                    if (Input.GetKeyDown(KeyCode.E))
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

    void ShowInteractText()
    {
        interactText.gameObject.SetActive(true);
    }

    void HideInteractText()
    {
        interactText.gameObject.SetActive(false);
    }
}
