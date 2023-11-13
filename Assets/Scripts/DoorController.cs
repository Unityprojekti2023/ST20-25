using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform door;

    public float maxOpening = 72f;
    public float minOpening = 0f;

    public float movementSpeed = 25f;    

    public bool isDoorOpen = false;

    public bool isDoorOpeningActive = false;
    public bool isDoorClosingActive = false;

    public float waitTime = 1.2f;

    public MachineScript machineScript;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isDoorOpeningActive == false && isDoorOpen == false && machineScript.isMachineActive == false /* Check if player is near the door */) {
            StartCoroutine(OpenDoor());
        }

        if (Input.GetKeyDown(KeyCode.E) && isDoorClosingActive == false && isDoorOpen == true /* Check if player is near the door */) {
            StartCoroutine(CloseDoor());
        }

        if (isDoorOpeningActive == true && door.transform.position.x < maxOpening && isDoorOpen == false) {
            door.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
        }

        if (isDoorClosingActive == true && door.transform.position.x > minOpening && isDoorOpen == true)
        {
            door.transform.Translate(-movementSpeed * Time.deltaTime, 0f, 0f);
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
