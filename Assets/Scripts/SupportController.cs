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
    public float maxLeftXPosition = 47.5f; //Old value 47.5
    public float maxRightXPosition = 0f;
    public float moveTime = 10f;
    public float waitTime = 31f; //Old value 29
    public float speed = 5f;

    [Header("Boolean Variables")]
    public bool moveSupportLeft = false;
    public bool moveSupportRight = false;
    public bool isTheSupportBeingMoved = false;
    public bool isSupportInPlace = false;

    void Start()
    {
        controlPanelInteractable = FindObjectOfType<MouseControlPanelInteractable>();
    }

    void Update()
    {
        if(machineScript.moveSupport&& !isTheSupportBeingMoved) {
            StartCoroutine(MoveSupport());
            isTheSupportBeingMoved = true;
        }

        if (moveSupportLeft && supportObject1.transform.position.x < maxLeftXPosition) {
            supportObject1.transform.Translate(speed * Time.deltaTime, 0f, 0f);
        }

        if (moveSupportRight && supportObject1.transform.position.x > maxRightXPosition) {
            supportObject1.transform.Translate(-speed * Time.deltaTime, 0f, 0f);
        }
    }

    IEnumerator MoveSupport() {
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
