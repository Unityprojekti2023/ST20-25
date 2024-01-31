using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillController : MonoBehaviour
{
    [Header("Drill Objects")]
    public Transform drillObject1;
    public Transform drillObject2;

    [Header("References to other scripts")]
    MachineScript machineScript;

    [Header("Values and Variables")]
    public float speed = 1.8f;
    public int activeCounter = 0;
    public int animation1Counter = 5;

    [Header("Boolean Variables")]
    public bool moveDrillLeft = false;
    public bool moveDrillRight = false;
    public bool isLatheAtTopPosition = false;
    public bool isLatheAtBottomPosition = false;
    public bool moveLatheUp = false;
    public bool moveLatheDown = false;
    public bool forceLatheDown = false;

    void Start()
    {
        // Getting a reference to MachineScript
        machineScript = FindObjectOfType<MachineScript>();                                              
    }

    void Update()
    {
        if (machineScript.moveDrill)     
        {
            // Checking if its time to move the drill left, and making sure activeCounter is less than animation1counter
            if (moveDrillLeft == true && activeCounter < animation1Counter)
            {
                // Translating the drill´s X position
                drillObject1.transform.Translate(speed * Time.deltaTime, 0f, 0f);
                drillObject2.transform.Translate(speed * Time.deltaTime, 0f, 0f);                           
            }
        }

        // Checking if its time to move drill right
        if (moveDrillRight == true)
        {
            // Translating the drill´s X position
            drillObject1.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);
            drillObject2.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);                      
        }

        if(moveLatheDown && !isLatheAtBottomPosition)
        {
            drillObject1.transform.Translate(0f, 1 * Time.deltaTime, 1.5f * Time.deltaTime);
        }

        if(moveLatheUp && !isLatheAtTopPosition)
        {
            drillObject1.transform.Translate(0f, -1 * Time.deltaTime, -1.5f * Time.deltaTime);
        }

        if(forceLatheDown)
        {
            drillObject1.transform.Translate(0f, 1 * Time.deltaTime, 1.5f * Time.deltaTime);
        }
    }
}
