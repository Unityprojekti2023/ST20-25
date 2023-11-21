using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Objects")]
    public Transform door;
    public Transform doorHandle;

    [Header("References to other scripts")]
    public MachineScript machineScript;
    public DoorInteractable doorTrigger;

    [Header("Movement Values")]
    public float maxOpening = 72f;
    public float minOpening = 0f;
    public float movementSpeed = 65f; //Old value 75 before sound effects, changing value to match audio clips
    public float waitTime = 1.2f;

    [Header("Boolean Variables")]
    public bool isDoorOpen = false;
    public bool isDoorOpeningActive = false;
    public bool isDoorClosingActive = false;
    public bool playOpeningClip = false;
    public bool playClosingClip = false;

    void Start()
    {
         
    }

    void Update()
    {
        if (isDoorOpeningActive == true && door.transform.position.x < maxOpening && isDoorOpen == false) {
            door.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
            doorHandle.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
            playOpeningClip = true;
            playClosingClip = false;
        }

        if (isDoorClosingActive == true && door.transform.position.x > minOpening && isDoorOpen == true)
        {
            door.transform.Translate(-movementSpeed * Time.deltaTime, 0f, 0f);
            doorHandle.transform.Translate(-movementSpeed * Time.deltaTime, 0f, 0f);
            playClosingClip = true;
            playOpeningClip = false;
        }
    }

    public IEnumerator OpenDoor()
    {
        isDoorOpeningActive = true;
        yield return new WaitForSeconds(waitTime);
        isDoorOpen = true;
        isDoorOpeningActive = false;
    }

    public IEnumerator CloseDoor()
    {
        isDoorClosingActive = true;
        yield return new WaitForSeconds(waitTime);
        isDoorOpen = false;
        isDoorClosingActive = false;
    }
}
