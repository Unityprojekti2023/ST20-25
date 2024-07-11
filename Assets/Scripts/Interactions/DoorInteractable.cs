using UnityEngine;


public class DoorInteractable : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    public ControlPanelInteractable controlPanelInteractable;
    public LatheController latheController;

    [Header("References to Animator and Audio Source")]
    private Animator doorAnimator; // Reference to the door animator
    public AudioSource audioSource; // Reference to the audio source
    public AudioClip openingSound; // Reference to the door sound
    public AudioClip closingSound; // Reference to the door sound

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        // Get the Animator component
        doorAnimator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (!IsAnimationPlaying() && !latheController.isLatheRunning)
        {
            // Get the current state of the lathe door
            bool isDoorClosed = controlPanelInteractable.isDoorClosed;
            Debug.Log("Door is closed: " + isDoorClosed);

            //Check if door is open and no animation is playing
            if (!isDoorClosed)
            {
                controlPanelInteractable.isDoorClosed = true;
                CloseDoor();
                RayInteractor.instance.UpdateInteractionText(transform.name, "Open door: [LMB] or [E]");
            }
            else if (isDoorClosed)
            {
                controlPanelInteractable.isDoorClosed = false;
                OpenDoor();
                RayInteractor.instance.UpdateInteractionText(transform.name, "Close door: [LMB] or [E]");
            }
        }
    }

    void OpenDoor()
    {
        doorAnimator.SetTrigger("OpenDoor");        // Set the trigger to open the door 
        audioSource.PlayOneShot(openingSound);      // Play the opening sound
    }

    void CloseDoor()
    {
        doorAnimator.SetTrigger("CloseDoor");       // Set the trigger to close the door
        audioSource.PlayOneShot(closingSound);      // Play the closing sound
    }

    bool IsAnimationPlaying()
    {
        // Get the current state of the Animator
        AnimatorStateInfo stateInfo = doorAnimator.GetCurrentAnimatorStateInfo(0);

        // Check if the Animator is in transition or playing any state
        return stateInfo.normalizedTime < 1f || doorAnimator.IsInTransition(0);
    }
}
