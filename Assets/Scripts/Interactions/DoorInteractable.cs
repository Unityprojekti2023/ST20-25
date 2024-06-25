using UnityEngine;


public class DoorInteractable : MonoBehaviour, IInteractable
{
    private Animator doorAnimator; // Reference to the door animator
    public AudioSource audioSource; // Reference to the audio source
    public AudioClip openingSound; // Reference to the door sound
    public AudioClip closingSound; // Reference to the door sound
    public bool isDoorOpen = false; // Boolean to check if the door is open

    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    public void Interact()
    {
        //Check if door is open and no animation is playing
        if (isDoorOpen && !IsAnimationPlaying())
        {
            Debug.Log("Closing door");
            CloseDoor();
        }
        else if (!isDoorOpen && !IsAnimationPlaying())
        {
            Debug.Log("Opening door");
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        doorAnimator.SetTrigger("OpenDoor");        // Set the trigger to open the door 
        audioSource.PlayOneShot(openingSound);      // Play the opening sound
        isDoorOpen = true;
    }

    void CloseDoor()
    {
        doorAnimator.SetTrigger("CloseDoor");       // Set the trigger to close the door
        audioSource.PlayOneShot(closingSound);      // Play the closing sound

        isDoorOpen = false;
    }

    bool IsAnimationPlaying()
    {
        // Get the current state of the Animator
        AnimatorStateInfo stateInfo = doorAnimator.GetCurrentAnimatorStateInfo(0);

        // Check if the Animator is in transition or playing any state
        return stateInfo.normalizedTime < 1f || doorAnimator.IsInTransition(0);
    }
}
