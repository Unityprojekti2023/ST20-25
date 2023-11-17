using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Transform whiteLight;
    public Transform greenLight;
    public Transform redLight;

    public bool isGreenLightAlreadyInPosition = false;
    public bool isRedLightAlreadyInPosition = false;

    public MachineScript machineScript;
    public MouseControlPanelInteractable mouseControlPanelInteractable;
    
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

        if (mouseControlPanelInteractable.isResetClicked && !isRedLightAlreadyInPosition) {
            isRedLightAlreadyInPosition = true;
            redLight.Translate(0, 20f, 0);
            whiteLight.Translate(0, -20f, 0);
        }

        if (mouseControlPanelInteractable.isPowerOFFClicked && isRedLightAlreadyInPosition) {
            isRedLightAlreadyInPosition = false;
            redLight.Translate(0, -20f, 0);
            whiteLight.Translate(0, 20f, 0);
        }
    }
}
