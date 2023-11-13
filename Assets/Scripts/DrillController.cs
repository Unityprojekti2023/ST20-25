using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillController : MonoBehaviour
{
    [Header("Drill Objects")]
    public Transform drillObject1;
    public Transform drillObject2;
    public Transform drillObject3;
    public Transform drillObject4;
    public Transform drillObject5;
    public Transform drillObject6;

    [Header("References to other scripts")]
    public MachineScript machineScript;

    [Header("Movement Values")]
    public float maxLeftXPosition = 100f; 
    public float maxRightXPosition = 0f;
    public float waitTime = 28f;
    public float speed = 2f;

    [Header("Boolean Variables")]
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

        if (moveDrillLeft == true && drillObject1.transform.position.x < maxLeftXPosition) {
            drillObject1.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject2.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject3.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject4.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject5.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject6.transform.Translate(0f, speed * Time.deltaTime, 0f);
        }

        if (moveDrillRight == true && drillObject1.transform.position.x > maxRightXPosition) {
            drillObject1.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject2.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject3.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject4.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject5.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject6.transform.Translate(0f, -speed * Time.deltaTime * 3, 0f);
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
