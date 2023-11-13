using UnityEngine;
using TMPro;

public class RayInteractor : MonoBehaviour
{
    public TextMeshProUGUI interactText;
    public float interactDistance = 50f;

    void Update()
    { 
        if (CameraController.Instance != null && CameraController.Instance.isMainCamActive)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
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
