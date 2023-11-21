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
    public float movementSpeed = 75f;    
    public float waitTime = 1.2f;

    [Header("Boolean Variables")]
    public bool isDoorOpen = false;
    public bool isDoorOpeningActive = false;
    public bool isDoorClosingActive = false;
    public bool playOpeningClip = false;

    void Start()
    {
         
    }

    void Update()
    {
        if (isDoorOpeningActive == true && door.transform.position.x < maxOpening && isDoorOpen == false) {
            door.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
            doorHandle.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
            playOpeningClip = true;
        }

        if (isDoorClosingActive == true && door.transform.position.x > minOpening && isDoorOpen == true)
        {
            door.transform.Translate(-movementSpeed * Time.deltaTime, 0f, 0f);
            doorHandle.transform.Translate(-movementSpeed * Time.deltaTime, 0f, 0f);
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
