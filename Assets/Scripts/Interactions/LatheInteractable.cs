using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheInteractable : MonoBehaviour, IInteractable
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


    public void Interact()
    {
        if(doorController.isDoorOpen && !isMachineActive)
        {
            if(!isUncutObjectInCuttingPosition)
            {
                uncutObject.Translate(-20f, 0, 0);
                cutObject.Translate(-20f, 0, 0);
                isUncutObjectInCuttingPosition = true;
                isCutObjectInCuttingPosition = true;
            }
            else 
            {
                uncutObject.position = new Vector3(10f, -7-93f, -49.28f);
                cutObject.Translate(20f, 0, 0);
                isUncutObjectInCuttingPosition = false;
                isCutObjectInCuttingPosition = false;
            }
        }
    }
}
