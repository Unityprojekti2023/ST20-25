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
        if (isDoorOpen)
        {
            Debug.Log("Closing door");
            CloseDoor();
        }
        else
        {
            Debug.Log("Opening door");
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        doorAnimator.SetTrigger("OpenDoor");
        audioSource.PlayOneShot(openingSound);
        isDoorOpen = true;
    }

    void CloseDoor()
    {
        doorAnimator.SetTrigger("CloseDoor");
        audioSource.PlayOneShot(closingSound);
        isDoorOpen = false;
    }
}
