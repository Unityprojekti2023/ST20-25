using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineScript : MonoBehaviour
{

    public Transform uncutObject;
    public Transform cutObject;

    public float maxLeftXPosition = -125f;
    public float waitTime = 20;
    public float movementSpeed = 5f; 

    public bool isUncutObjectInCuttingPosition = false;
    public bool isCutObjectInCuttingPosition = false;
    public bool isMachineActive = false;
    public bool isCuttingProcessActive = false;

    public DoorController doorController;

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
        }

        //Calling object moving Coroutine
        if(Input.GetKeyDown(KeyCode.T) && isUncutObjectInCuttingPosition == true && doorController.isDoorOpen == false && isCutObjectInCuttingPosition == false) {
            StartCoroutine(MoveUncutObjectLeft());
            isCutObjectInCuttingPosition = true;
        }

        //Moving uncut object
        if (isMachineActive == true && uncutObject.transform.position.x < maxLeftXPosition) {
            uncutObject.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
        }

        //Removing cut object
        if (Input.GetKeyDown(KeyCode.Y) && isMachineActive == false && doorController.isDoorOpen == true && isCutObjectInCuttingPosition == true) {
            cutObject.Translate(20f, 0, 0);
            isUncutObjectInCuttingPosition = false;
            isCutObjectInCuttingPosition = false;
        }
    }

    IEnumerator MoveUncutObjectLeft() {
        isMachineActive = true;
        yield return new WaitForSeconds(waitTime);
        isMachineActive = false;
    }
}