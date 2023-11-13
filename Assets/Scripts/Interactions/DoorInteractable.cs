using UnityEngine;


public class DoorInteractable : MonoBehaviour, IInteractable
{
    public DoorController doorController;

    public void Interact()
    {
        if (doorController != null)
        {
            if (!doorController.isDoorOpen && !doorController.isDoorOpeningActive)
            {
                StartCoroutine(doorController.OpenDoor());
            }
            else if (doorController.isDoorOpen && !doorController.isDoorClosingActive)
            {
                StartCoroutine(doorController.CloseDoor());
            }
        }
    }
}
