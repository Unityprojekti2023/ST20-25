using UnityEngine;


public class DoorInteractable : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    public DoorController doorController;
    public EscapeMenu escapeMenu;

    public void Interact()                                                                                              // Door interaction function
    {
        if (doorController != null)                                                                                     // Making sure doorController isnt null
        {
            if (!doorController.isDoorOpen && !doorController.isDoorOpeningActive && !escapeMenu.isGamePaused)          // Making sure door is not open, door opening isnt already active and the game isnt paused
            {
                StartCoroutine(doorController.OpenDoor());                                                              // Starting door opening coroutine
            }
            else if (doorController.isDoorOpen && !doorController.isDoorClosingActive && !escapeMenu.isGamePaused)      // Making sure the door is open, door opening isnt already active and the game isnt paused
            {
                StartCoroutine(doorController.CloseDoor());                                                             // Starting door closing coroutine
            }
        }
    }
}
