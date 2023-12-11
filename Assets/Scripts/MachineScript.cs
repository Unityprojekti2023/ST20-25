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
        
        uncutObject.position = new Vector3(-100f, 102.4f, 182.7f);
        cutObject.position = new Vector3(-100f, 102.4f, 182.7f);
    }

    void Update()
    {
        if (supportController.isSupportInPlace && !moveDrill)
        {
            moveDrill = true;
            StartCoroutine(MoveUncutObjectLeft());
        }

        // Moving uncut object
        if (isMachineActive && uncutObject.transform.position.x < maxLeftXPosition && moveObject)
        {
            uncutObject.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
        }
    }

    public void MoveObjectsToCuttingPosition()
    {
        uncutObject.position = new Vector3(-130f, 102.4f, 182.7f);
        cutObject.position = new Vector3(-131.2f, 102.4f, 182.7f);
        isUncutObjectInCuttingPosition = true;
        isCutObjectInCuttingPosition = true;
        isAnimationComplete = false;
    }

    public void RemoveObjectsFromCuttingPosition()
    {
        uncutObject.position = new Vector3(-100f, 102.4f, 182.7f);
        cutObject.position = new Vector3(-100f, 102.4f, 182.7f);
        isUncutObjectInCuttingPosition = false;
        isCutObjectInCuttingPosition = false;
        isAnimationComplete = false;
    }

    IEnumerator MoveUncutObjectLeft()
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
