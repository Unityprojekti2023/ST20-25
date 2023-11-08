using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportController : MonoBehaviour
{
    public Transform supportObject;
    public MachineScript machineScript;

    public float maxLeftXPosition = 47.5f;
    public float maxRightXPosition = 0f;
    public float moveTime = 9f;
    public float waitTime = 18f;
    public float speed = 5f;

    public bool moveSupportLeft = false;
    public bool moveSupportRight = false;
    public bool isTheSupportBeingMoved = false;
    public bool isSupportInPlace = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(machineScript.moveSupport == true && isTheSupportBeingMoved == false) {
            StartCoroutine(moveSupport());
            isTheSupportBeingMoved = true;
        }

        if (moveSupportLeft == true && supportObject.transform.position.x < maxLeftXPosition) {
            supportObject.transform.Translate(speed * Time.deltaTime, 0f, 0f);
        }

        if (moveSupportRight == true && supportObject.transform.position.x > maxRightXPosition) {
            supportObject.transform.Translate(-speed * Time.deltaTime, 0f, 0f);
        }
    }

    IEnumerator moveSupport() {
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
