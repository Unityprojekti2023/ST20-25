using UnityEngine;


public class DoorInteractable : MonoBehaviour, IInteractable
{
    public DoorController doorController;
    public EscapeMenu escapeMenu;

    public void Interact()
    {
        if (doorController != null)
        {
            if (!doorController.isDoorOpen && !doorController.isDoorOpeningActive && !escapeMenu.isGamePaused)
            {
                StartCoroutine(doorController.OpenDoor());
            }
            else if (doorController.isDoorOpen && !doorController.isDoorClosingActive && !escapeMenu.isGamePaused)
            {
                StartCoroutine(doorController.CloseDoor());
            }
        }
    }
}
