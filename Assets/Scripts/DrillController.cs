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
    public Transform drillObject7;
    public Transform drillObject8;
    public Transform drillObject9;
    public Transform drillObject10;
    public Transform drillObject11;
    public Transform drillObject12;
    public Transform drillObject13;
    public Transform drillObject14;
    public Transform drillObject15;
    public Transform drillObject16;
    public Transform drillObject17;
    public Transform drillObject18;
    public Transform drillObject19;
    public Transform drillObject20;
    public Transform drillObject21;

    [Header("References to other scripts")]
    MachineScript machineScript;

    [Header("Movement Values")]
    float maxLeftXPosition = 100f; 
    float maxRightXPosition = 0f;
    float waitTime = 28f;
    float speed = 2f;

    [Header("Boolean Variables")]
    bool moveDrillLeft = false;
    bool moveDrillRight = false;
    bool wasTheDrillAlreadyStarted = false;

    void Start()
    {
        machineScript = FindObjectOfType<MachineScript>();
    }

    void Update()
    {
        if (machineScript.isMachineActive && !wasTheDrillAlreadyStarted && machineScript.moveDrill) {
            StartCoroutine(moveDrill());
            wasTheDrillAlreadyStarted = true;
        }

        if (moveDrillLeft == true && drillObject1.transform.position.x < maxLeftXPosition) {
            drillObject1.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject2.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject3.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject4.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject5.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject6.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject7.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject8.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject9.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject10.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject11.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject12.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject13.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject14.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject15.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject16.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject17.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject18.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject19.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject20.transform.Translate(speed * Time.deltaTime, 0f, 0f);
            drillObject21.transform.Translate(speed * Time.deltaTime, 0f, 0f);
        }

        if (moveDrillRight == true && drillObject1.transform.position.x > maxRightXPosition) {
            drillObject1.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject2.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject3.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject4.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject5.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject6.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject7.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject8.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject9.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject10.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject11.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject12.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject13.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject14.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject15.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject16.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject17.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject18.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject19.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject20.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject21.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
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
