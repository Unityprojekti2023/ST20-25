using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Objects")]
    public Transform door;

    [Header("Boolean Variables")]
    public bool isDoorOpen = false;
    public bool isDoorOpeningActive = false;
    public bool isDoorClosingActive = false;
    public bool playOpeningClip = false;
    public bool playClosingClip = false;

    [Header("Other Values")]
    public float maxOpening = 72f;
    public float minOpening = 0f;
    public float movementSpeed = 60f;    
    public float waitTime = 1.2f;

    public MouseControlPanelInteractable mouseControlPanelInteractable;

    void Update()
    {
        if (isDoorOpeningActive == true && door.transform.position.x < maxOpening && isDoorOpen == false)   // Checking if door opening procedure is active, making sure the door isnt at its max opening value,
        {                                                                                                   // and checking that the door is closed before attempting to open the door.
            if (!mouseControlPanelInteractable.isLathingActive) {                                           // Making sure Lathing isnt active
                door.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);                           // Translating the door's X value
                playOpeningClip = true;                                                                     // Setting "playOpeningClip" to "true" so door opening audio clip can be played
                playClosingClip = false; 
            }                                                                                                                                        
        }

        if (isDoorClosingActive == true && door.transform.position.x > minOpening && isDoorOpen == true)    // Checking if the door closing procedure is active, making sure the door isnt at its min opening value,
        {                                                                                                   // and checking that the door is opened before attempting to close the door.            
            door.transform.Translate(-movementSpeed * Time.deltaTime, 0f, 0f);                              // Translating the door's X-value
            playClosingClip = true;                                                                         // Setting "playClosingClip" to "true" so door opening audio clip can be played
            playOpeningClip = false;
        }
    }

    public IEnumerator OpenDoor()                                                                           // Door opening function, setting the door opening procedure active for 1.2 seconds so the door can be moved on line 29.
    {
        isDoorOpeningActive = true;                                                                         // Setting the door opening procedure as "active"
        yield return new WaitForSeconds(waitTime);                                                          // Adding a delay before next line is executed
        isDoorOpen = true;                                                                                  // Marking the door as "opened"
        isDoorOpeningActive = false;                                                                        // Door opening procedure is set back to "inactive" after the delay
    }

    public IEnumerator CloseDoor()                                                                          // Door closing function, setting the door closing procedure active for 1.2 seconds so the door can be moved on line 35.
    {
        isDoorClosingActive = true;                                                                         // Setting door closing procedure as "active"
        yield return new WaitForSeconds(waitTime);                                                          // Adding a delay and marking the door as closed after the delay
        isDoorOpen = false;                                                                                 // Marking the door as closed
        isDoorClosingActive = false;                                                                        // Door closing procedure is set back to "inactive" after the delay
    }
}
