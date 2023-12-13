using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineScript : MonoBehaviour
{
    [Header("Objects")]
    public Transform uncutObject;
    public Transform cutObject;

    [Header("References to other scripts")]
    public DoorController doorController;
    public SupportController supportController;
    public MouseControlPanelInteractable mouseControlPanelInteractable;

    [Header("Movement Values")]
    public float maxLeftXPosition = -113.5f;
    public float movementSpeed = 1.3f;
    public float waitTime = 20;

    [Header("Boolean Variables")]
    public bool isUncutObjectInCuttingPosition = false;
    public bool isCutObjectInCuttingPosition = false;
    public bool isMachineActive = false;
    public bool moveSupport = false;
    public bool moveDrill = false;
    public bool moveObject = false;
    public bool isAnimationComplete = false;

    void Start()
    {
        uncutObject.position = new Vector3(-100f, 102.4f, 182.7f);                                  //Hiding the uncut and cut objects on start
        cutObject.position = new Vector3(-100f, 102.4f, 182.7f);
    }

    void Update()
    {
        if (supportController.isSupportInPlace && !moveDrill)                                       // Checking if the support is in place and the drill isnt moving already
        {
            moveDrill = true;                                                                       // Setting moveDrill to true, so the drill can begin moving
            StartCoroutine(MoveUncutObjectLeft());                                                  // Starting the coroutine to move uncut object left (for the cutting animation)
        }

        if (isMachineActive && uncutObject.transform.position.x < maxLeftXPosition && moveObject)   // Checking if machine is active, making sure the object is within its allowed X position
        {
            uncutObject.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);                // Translating the uncut objects X position slowly to create the effect that the metal object is being cut
        }
    }

    public void MoveObjectsToCuttingPosition()                                                      // Function to move objects into cutting position
    {
        uncutObject.position = new Vector3(-130f, 102.4f, 182.7f);
        cutObject.position = new Vector3(-131.2f, 102.4f, 182.7f);
        isUncutObjectInCuttingPosition = true;
        isCutObjectInCuttingPosition = true;
        isAnimationComplete = false;
    }

    public void RemoveObjectsFromCuttingPosition()                                                  // Function to remove objects from the cutting position
    {
        uncutObject.position = new Vector3(-100f, 102.4f, 182.7f);
        cutObject.position = new Vector3(-100f, 102.4f, 182.7f);
        isUncutObjectInCuttingPosition = false;
        isCutObjectInCuttingPosition = false;
        isAnimationComplete = false;
    }

    IEnumerator MoveUncutObjectLeft()                                                               // Coroutine responsible for moving the drill and objects at the right time
    {
        moveDrill = true;
        isMachineActive = true;
        yield return new WaitForSeconds(18f);
        moveObject = true;
        yield return new WaitForSeconds(waitTime);
        isMachineActive = false;
        moveSupport = false;
        moveDrill = false;
        moveObject = false;
        isAnimationComplete = true;
    }
}
