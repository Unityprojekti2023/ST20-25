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
    public bool isAnimationComplete = false;

    void Start()
    {
        uncutObject.Translate(20f, 0, 0);
        cutObject.Translate(20f, 0, 0);
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
        uncutObject.Translate(-20f, 0, 0);
        cutObject.Translate(-20f, 0, 0);
        isUncutObjectInCuttingPosition = true;
        isCutObjectInCuttingPosition = true;
        isAnimationComplete = false;
    }

    public void RemoveObjectsFromCuttingPosition()
    {
        uncutObject.Translate(20f, 0, 0);
        cutObject.Translate(20f, 0, 0);
        isUncutObjectInCuttingPosition = false;
        isCutObjectInCuttingPosition = false;
        isAnimationComplete = false;
    }

    IEnumerator MoveUncutObjectLeft()
    {
        moveDrill = true;
        isMachineActive = true;
        yield return new WaitForSeconds(18f); // old = 14
        moveObject = true;
        yield return new WaitForSeconds(waitTime);
        isMachineActive = false;
        moveSupport = false;
        moveDrill = false;
        moveObject = false;
        isAnimationComplete = true;
    }
}
