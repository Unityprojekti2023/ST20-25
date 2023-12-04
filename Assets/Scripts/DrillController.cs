using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillController : MonoBehaviour
{
    [Header("Drill Objects")]
    public Transform drillObject1;

    [Header("References to other scripts")]
    MachineScript machineScript;

    [Header("Movement Values")]
    float maxLeftXPosition = 100f; 
    float maxRightXPosition = 0f;
    float waitTime = 30f; //old value 28
    float speed = 1.8f; //old value 2

    [Header("Boolean Variables")]
    bool moveDrillLeft = false;
    bool moveDrillRight = false;
    bool wasTheDrillAlreadyStarted = false;

    void Start()
    {
        machineScript = FindObjectOfType<MachineScript>();                                              // Getting a reference to machineScript
    }

    void Update()
    {
        if (machineScript.isMachineActive && !wasTheDrillAlreadyStarted && machineScript.moveDrill)     // Checking if the machine is active, making sure the drill wasn't already started and getting confirmation from machineScript to move the drill.
        {
            StartCoroutine(moveDrill());                                                                // Calling moveDrill coroutine
            wasTheDrillAlreadyStarted = true;                                                           // Setting "wasTheDrillAlreadyStarted" to true so the coroutine isn't ran multiple times
        }

        if (moveDrillLeft == true && drillObject1.transform.position.x < maxLeftXPosition)              //Checking if it's time to move drill left and making sure the drill doesnt exceed it's max left X position.
        {
            drillObject1.transform.Translate(speed * Time.deltaTime, 0f, 0f);                           // Translating the drill's X position
        }

        if (moveDrillRight == true && drillObject1.transform.position.x > maxRightXPosition)            //Checking if it's time to move drill right and making sure the drill doesnt exceed it's max right X position.
        {
            drillObject1.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);                      // Translating the drill's X position
        }
    }

    IEnumerator moveDrill() {                                                                           //Coroutine responsible for setting boolean variables after certain delays
        moveDrillLeft = true;                                                                           // Setting "moveDrillLeft" to true so the drill can be moved left
        yield return new WaitForSeconds(waitTime);      
        moveDrillLeft = false;                                                                          // Setting "moveDrillLeft" back to false after the cutting animation has finished.
        yield return new WaitForSeconds(1f);            
        moveDrillRight = true;                                                                          // Setting "moveDrillRight" to true so the drill can be moved right.
        yield return new WaitForSeconds(10f);           
        moveDrillRight = false;                                                                         // Setting "moveDrillRight" back to false after the drill has been moved back to its starting position
        wasTheDrillAlreadyStarted = false;                                                              // Setting "wasTheDrillAlreadyStarted" back to false, so coroutine can be ran again.
    }
}
