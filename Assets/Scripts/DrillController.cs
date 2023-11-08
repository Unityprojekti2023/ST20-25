using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillController : MonoBehaviour
{
    public Transform drillObject;

    public MachineScript machineScript;

    public float maxLeftXPosition = 50f; //vanha 30
    public float maxRightXPosition = 0f;
    public float waitTime = 18f; //vanha 15
    public float speed = 2f;

    public bool moveDrillLeft = false;
    public bool moveDrillRight = false;
    public bool wasTheDrillAlreadyStarted = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (machineScript.isMachineActive == true && wasTheDrillAlreadyStarted == false && machineScript.moveDrill == true) {
            StartCoroutine(moveDrill());
            wasTheDrillAlreadyStarted = true;
        }

        if (moveDrillLeft == true && drillObject.transform.position.x < maxLeftXPosition) {
            drillObject.transform.Translate(speed * Time.deltaTime, 0f, 0f);
        }

        if (moveDrillRight == true && drillObject.transform.position.x > maxRightXPosition) {
            drillObject.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
        }
    }

    IEnumerator moveDrill() {
        moveDrillLeft = true;
        yield return new WaitForSeconds(waitTime);
        moveDrillLeft = false;
        yield return new WaitForSeconds(1f);
        moveDrillRight = true;
        yield return new WaitForSeconds(10f);
        moveDrillRight = false;
        wasTheDrillAlreadyStarted = false;
    }
}
