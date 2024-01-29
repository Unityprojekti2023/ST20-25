using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillController : MonoBehaviour
{
    [Header("Drill Objects")]
    public Transform drillObject1;

    [Header("References to other scripts")]
    MachineScript machineScript;

    [Header("Values and Variables")]
    public float speed = 1.8f;
    public int activeCounter = 0;
    public int animation1Counter = 5;

    [Header("Boolean Variables")]
    public bool moveDrillLeft = false;
    public bool moveDrillRight = false;

    void Start()
    {
        machineScript = FindObjectOfType<MachineScript>();                                              
    }

    void Update()
    {
        if (machineScript.moveDrill)     
        {
            if (moveDrillLeft == true && activeCounter < animation1Counter)
            {
                drillObject1.transform.Translate(speed * Time.deltaTime, 0f, 0f);                           
            }
        }

        if (moveDrillRight == true)
        {
            drillObject1.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);                      
        }
    }
}
