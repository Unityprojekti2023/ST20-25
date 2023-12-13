using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportController : MonoBehaviour
{
    [Header("Objects")]
    public Transform supportObject1;

    [Header("References to other scripts")]
    public MachineScript machineScript;

    MouseControlPanelInteractable controlPanelInteractable;

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
    public bool isSupportInPlace = false;

    void Start()
    {
        // Getting a reference to the controlpanel interactable script
        controlPanelInteractable = FindObjectOfType<MouseControlPanelInteractable>();
    }

    void Update()
    {
        if(machineScript.moveSupport&& !isTheSupportBeingMoved)                                 // Checking if machineScript has given the ok to move support and making sure the support isnt moving already
        {
            StartCoroutine(MoveSupport());                                                      // Starting the coroutine to move the support
            isTheSupportBeingMoved = true;
        }

        if (moveSupportLeft && supportObject1.transform.position.x < maxLeftXPosition)          // Checking if its time to move support left and the support is within allowed X position
        {
            supportObject1.transform.Translate(speed * Time.deltaTime, 0f, 0f);                 // Translating the support´s X position to the left
        }

        if (moveSupportRight && supportObject1.transform.position.x > maxRightXPosition)        // Checking if its time to move support right and the support is within allowed X position
        {
            supportObject1.transform.Translate(-speed * Time.deltaTime, 0f, 0f);                // Translating the support´s X position to the right
        }
    }

    IEnumerator MoveSupport()                           // Coroutine responsible for making sure that everything happens at the right time
    {
        moveSupportLeft = true;
        yield return new WaitForSeconds(moveTime);
        moveSupportLeft = false;
        isSupportInPlace = true;
        yield return new WaitForSeconds(waitTime);
        moveSupportRight = true;
        isSupportInPlace = false;
        yield return new WaitForSeconds(moveTime);
        moveSupportRight = false;
        isTheSupportBeingMoved = false;
    }
}
