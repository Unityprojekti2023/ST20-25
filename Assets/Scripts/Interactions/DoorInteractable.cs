using UnityEngine;


public class DoorInteractable : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    public DoorController doorController;

    private void Start()
    {
        if (doorController == null)                                                                                      // Making sure doorController isnt null
        {
            Debug.LogError("DoorController reference not set in DoorInteractable!");
            return;
        }

    }

    public void Interact()                                                                                          // Door interaction function
    {
        if (!doorController.isDoorOpen && !doorController.isDoorOpeningActive)                                      // Making sure door is not open, door opening isnt already active and the game isnt paused
        {
            StartCoroutine(doorController.OpenDoor());                                                              // Starting door opening coroutine
        }
        else if (doorController.isDoorOpen && !doorController.isDoorClosingActive)                                  // Making sure the door is open, door opening isnt already active and the game isnt paused
        {
            StartCoroutine(doorController.CloseDoor());                                                             // Starting door closing coroutine
        }
    }
}
