using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Objects")]
    public Transform door;
    public Transform doorHandle;

    [Header("References to other scripts")]
    public MachineScript machineScript;
    public DoorTrigger doorTrigger;

    [Header("Movement Values")]
    public float maxOpening = 72f;
    public float minOpening = 0f;
    public float movementSpeed = 75f;    
    public float waitTime = 1.2f;

    [Header("Boolean Variables")]
    public bool isDoorOpen = false;
    public bool isDoorOpeningActive = false;
    public bool isDoorClosingActive = false;

    void Start()
    {
         
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isDoorOpeningActive == false && isDoorOpen == false && machineScript.isMachineActive == false  && doorTrigger.isPlayerNearDoor == true)  {
            StartCoroutine(OpenDoor());
        }

        if (Input.GetKeyDown(KeyCode.E) && isDoorClosingActive == false && isDoorOpen == true && doorTrigger.isPlayerNearDoor == true) {
            StartCoroutine(CloseDoor());
        }

        if (isDoorOpeningActive == true && door.transform.position.x < maxOpening && isDoorOpen == false) {
            door.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
            doorHandle.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
        }

        if (isDoorClosingActive == true && door.transform.position.x > minOpening && isDoorOpen == true) {
            door.transform.Translate(-movementSpeed * Time.deltaTime, 0f, 0f);
            doorHandle.transform.Translate(-movementSpeed * Time.deltaTime, 0f, 0f);
        }
    }

    IEnumerator OpenDoor() {
        isDoorOpeningActive = true;
        yield return new WaitForSeconds(waitTime);
        isDoorOpen = true;
        isDoorOpeningActive = false;
    }

    IEnumerator CloseDoor() {
        isDoorClosingActive = true;
        yield return new WaitForSeconds(waitTime);
        isDoorOpen = false;
        isDoorClosingActive = false;
    }
}
