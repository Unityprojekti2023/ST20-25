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
        greenLight.Translate(0, -20f, 0);                                                           // Moving the green light down so it is "hidden"
        redLight.Translate(0, -20f, 0);                                                             // Moving the red light down so it is "hidden"
    }

    void Update()
    {
        if (machineScript.isMachineActive && !isGreenLightAlreadyInPosition)  {                     // Checking if machine is active and making sure green light isn't in position yet
            isGreenLightAlreadyInPosition = true;                                                   // Making "isGreenLightAlreadyInPosition" true so the green light isn't moved more than once
            greenLight.Translate(0, 20f, 0);                                                        // Moving the green light into position
            whiteLight.Translate(0, -20f, 0);                                                       // "Hiding" the white light while green light is active
        }

        if (!machineScript.isMachineActive && isGreenLightAlreadyInPosition) {                      // Checking if machine is inactive and checking if green light is in position
            isGreenLightAlreadyInPosition = false;                                                  // Making "isGreenLightAlreadyInPosition" false, so it can be moved again next time the machine is turned on
            greenLight.Translate(0, -20f, 0);                                                       // "Hiding" the green light
            whiteLight.Translate(0, 20f, 0);                                                        // Moving the white light back to position
        }

        if (mouseControlPanelInteractable.isResetClicked && !isRedLightAlreadyInPosition) {         // Checking if reset button was clicked (Last button in the PowerON procedure) and making sure red light isn't already in position
            isRedLightAlreadyInPosition = true;                                                     // Making "isRedLightAlreadyInPosition" true so the red light isn't moved more than once
            redLight.Translate(0, 20f, 0);                                                          // Moving the red light into position
            whiteLight.Translate(0, -20f, 0);                                                       // "Hiding" the white light while red light is active
        }

        if (mouseControlPanelInteractable.isPowerOFFClicked && isRedLightAlreadyInPosition) {       // Checking if powerOFF was clicked (Last button in the PowerOFF procedure) and making sure red light is in position
            isRedLightAlreadyInPosition = false;                                                    // Making "isRedLightAlreadyInPosition" false, so it can be moved again next time the machine is powered on
            redLight.Translate(0, -20f, 0);                                                         // "Hiding" the red light
            whiteLight.Translate(0, 20f, 0);                                                        // Moving the white light back to position
        }
    }
}
