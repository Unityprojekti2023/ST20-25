using UnityEngine;


public class DoorInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        // Making sure door is not open, door opening isnt already active and the game isnt paused
        if (!DoorController.instance.isDoorOpen && !DoorController.instance.isDoorOpeningActive)
        {
            // Starting door opening coroutine
            StartCoroutine(DoorController.instance.OpenDoor());
        }
        // Making sure the door is open, door opening isnt already active and the game isnt paused
        else if (DoorController.instance.isDoorOpen && !DoorController.instance.isDoorClosingActive)
        {
            // Starting door closing coroutine
            StartCoroutine(DoorController.instance.CloseDoor());
        }
    }
}
