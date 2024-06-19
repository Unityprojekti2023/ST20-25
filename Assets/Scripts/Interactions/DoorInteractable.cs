using UnityEngine;


public class DoorInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        // Making sure door is not open, door opening isnt already active and the game isnt paused
        if (!DoorController.Instance.isDoorOpen && !DoorController.Instance.isDoorOpeningActive)
        {
            // Starting door opening coroutine
            StartCoroutine(DoorController.Instance.OpenDoor());
        }
        // Making sure the door is open, door opening isnt already active and the game isnt paused
        else if (DoorController.Instance.isDoorOpen && !DoorController.Instance.isDoorClosingActive)
        {
            // Starting door closing coroutine
            StartCoroutine(DoorController.Instance.CloseDoor());
        }
    }
}
