using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlPanelInteractable : MonoBehaviour
{
    [Header("References to other scripts")]
    public DoorController doorController;
    public SupportController supportController;
    public MachineScript machineScript;

    public LayerMask controlPanelLayer;

    public Camera controlPanelCamera;

    void Start()
    {
    }
    void Update()
    {
        // Check if controlPanelCamera is active before proceeding
        if (controlPanelCamera != null && controlPanelCamera.gameObject.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = controlPanelCamera.ScreenPointToRay(Input.mousePosition);

                Collider[] colliders = GetComponentsInChildren<Collider>();
                foreach (Collider collider in colliders)
                {
                    if (collider.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
                    {
                        string buttonName = collider.gameObject.name;
                        // Check if the collider belongs to the specified layer
                        if (((1 << collider.gameObject.layer) & controlPanelLayer) != 0)
                        {
                            switch (buttonName)
                            {
                                case "btn_CycleStart":
                                    Debug.Log("Interacted with ControlPanel: " + hit.collider.gameObject.name);

                                    if (machineScript.isUncutObjectInCuttingPosition && machineScript.isCutObjectInCuttingPosition && !doorController.isDoorOpen)
                                    {
                                        machineScript.moveSupport = true;
                                    }
                                    break;

                                case "btn_FeedHold":
                                    Debug.Log("Interacted with ControlPanel: " + hit.collider.gameObject.name);
                                    // Handle interaction for btn_FeedHold
                                    break;

                                    // Add more cases for other button names as needed
                            }
                        }
                    }
                }
            }
        }
    }
}
