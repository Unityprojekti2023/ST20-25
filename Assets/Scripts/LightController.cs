using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Transform whiteLight;
    public Transform greenLight;
    public Transform redLight;

    public bool isGreenLightAlreadyInPosition = false;

    public MachineScript machineScript;
    
    void Start()
    {
        greenLight.Translate(0, -20f, 0);
        redLight.Translate(0, -20f, 0);
    }

    void Update()
    {
        if (machineScript.isMachineActive && !isGreenLightAlreadyInPosition)  {
            isGreenLightAlreadyInPosition = true;
            greenLight.Translate(0, 20f, 0);
            whiteLight.Translate(0, -20f, 0);
        }

        if (!machineScript.isMachineActive && isGreenLightAlreadyInPosition) {
            isGreenLightAlreadyInPosition = false;
            greenLight.Translate(0, -20f, 0);
            whiteLight.Translate(0, 20f, 0);
        }
    }
}
