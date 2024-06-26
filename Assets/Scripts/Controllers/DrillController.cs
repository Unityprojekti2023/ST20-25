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
    public int targetCounter = 0;
    public int selectedProgram = 0;

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
        machineScript = FindObjectOfType<MachineScript>();          // Getting a reference to MachineScript                                          
    }

    void Update()
    {
        if (machineScript.moveDrill)
        {
            if (moveDrillLeft == true && activeCounter < targetCounter)                             // Checking if its time to move the drill left, and making sure activeCounter is less than animation1counter
            {
                drillObject1.transform.Translate(speed * Time.deltaTime, 0f, 0f);                   // Translating the drill´s X position
                drillObject2.transform.Translate(speed * Time.deltaTime, 0f, 0f);                
            }
        }

        if (moveDrillRight == true)                                                                 // Checking if its time to move drill right
        {
            drillObject1.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);                  // Translating the drill´s X position
            drillObject2.transform.Translate(-speed * Time.deltaTime * 3, 0f, 0f);                      
        }

        if(moveLatheDown && !isLatheAtBottomPosition)                                               // Checking if its time to move lathe down, and the lathe isnt already at the bottom position
        {
            drillObject1.transform.Translate(0f, 1 * Time.deltaTime, 1.5f * Time.deltaTime);
        }

        if(moveLatheUp && !isLatheAtTopPosition)                                                    // Checking if its time to move lathe up, and the lathe isnt already at the bottom position
        {
            drillObject1.transform.Translate(0f, -1 * Time.deltaTime, -1.5f * Time.deltaTime);
        }

        if(forceLatheDown)
        {
            drillObject1.transform.Translate(0f, 1 * Time.deltaTime, 1.5f * Time.deltaTime);        // This is to "force" the lathe down more beyond the bottom position
        }
    }
}
