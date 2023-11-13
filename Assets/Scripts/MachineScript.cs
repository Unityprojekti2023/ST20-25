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

    void Start()
    {
        uncutObject.Translate(20f, 0, 0);
        cutObject.Translate(20f, 0, 0);
    }

    void Update()
    {
        //Moving objects to cutting position
        if(Input.GetKeyDown(KeyCode.R) && doorController.isDoorOpen == true && isUncutObjectInCuttingPosition == false && isMachineActive == false) {
            uncutObject.Translate(-20f, 0, 0);
            cutObject.Translate(-20f, 0, 0);
            isUncutObjectInCuttingPosition = true;
            isCutObjectInCuttingPosition = true;
        }

        //Calling object moving Coroutine
        if(Input.GetKeyDown(KeyCode.T) && isUncutObjectInCuttingPosition == true && doorController.isDoorOpen == false && isCutObjectInCuttingPosition == true) {
            moveSupport = true;
        }

        if (supportController.isSupportInPlace == true) {
                moveDrill = true;
                StartCoroutine(MoveUncutObjectLeft());
            }

        //Moving uncut object
        if (isMachineActive == true && uncutObject.transform.position.x < maxLeftXPosition && moveObject == true) {
            uncutObject.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
        }

        //Removing cut object
        if (Input.GetKeyDown(KeyCode.Y) && isMachineActive == false && doorController.isDoorOpen == true && isCutObjectInCuttingPosition == true) {
            uncutObject.position = new Vector3(10f, -7-93f, -49.28f);
            cutObject.Translate(20f, 0, 0);
            isUncutObjectInCuttingPosition = false;
            isCutObjectInCuttingPosition = false;
        }
    }

    IEnumerator MoveUncutObjectLeft() {
        moveDrill = true;
        isMachineActive = true;
        yield return new WaitForSeconds(14f);
        moveObject = true;
        yield return new WaitForSeconds(waitTime);
        isMachineActive = false;
        moveSupport = false;
        moveDrill = false;
        moveObject = false;
    }
}