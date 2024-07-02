using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportController : MonoBehaviour
{
    [Header("Objects")]
    public Transform supportObject1;

    [Header("References to other scripts")]
    public MachineScript machineScript;
    ControlPanelInteractable controlPanelInteractable;

    [Header("Movement Values")]
    public float maxLeftXPosition = 47.5f;
    public float maxRightXPosition = 0f;
    public float moveTime = 10f;
    public float waitTime = 31f;
    public float speed = 5f;

    [Header("Boolean Variables")]
    public bool moveSupportLeft = false;
    public bool moveSupportRight = false;
    public bool isTheSupportBeingMoved = false;
    public bool isSupportInLeftSpot = false;
    public bool isSupportInRightSpot = false;

    void Start()
    {
        controlPanelInteractable = FindObjectOfType<ControlPanelInteractable>();           // Getting a reference to the controlpanel interactable script
    }

    void Update()
    {
        // Checking if certain conditions meet before starting to move the support left
        if(machineScript.moveSupport && !isTheSupportBeingMoved && !isSupportInLeftSpot && machineScript.isMachineActive)
        {
            moveSupportLeft = true;
            isTheSupportBeingMoved = true;
        }

        // Checking if certain conditions meet before starting to move the support right
        if(machineScript.isAnimationComplete && !isTheSupportBeingMoved && !isSupportInRightSpot) {
            isTheSupportBeingMoved = true;
            moveSupportRight = true;
        }

        if(moveSupportLeft)                                                                     // Checking if its time to move the support left
        {
            supportObject1.transform.Translate(speed * Time.deltaTime, 0f, 0f);                 // Translating the support´s X position to the left
        }

        if(moveSupportRight)                                                                    // Checking if its time to move the support right
        {
            supportObject1.transform.Translate(-speed * Time.deltaTime, 0f, 0f);                // Translating the support´s X position to the right
        }
    }
}
