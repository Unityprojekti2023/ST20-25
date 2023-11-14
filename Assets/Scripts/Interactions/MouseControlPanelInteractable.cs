using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlPanelInteractable : MonoBehaviour
{
    [Header("Objects")]
    public Transform uncutObject;
    public Transform cutObject;

    [Header("References to other scripts")]
    public DoorController doorController;
    public SupportController supportController;

    LatheInteractable latheInteractable;

    [Header("Movement Values")]
    public float maxLeftXPosition = -113.5f;
    public float movementSpeed = 1.3f;
    public float waitTime = 20;

    [Header("Boolean Variables")]
    public bool isUncutObjectInCuttingPosition = false;
    public bool isCutObjectInCuttingPosition = false;
    public bool isMachineActive = false;
    public bool moveSupport = false;
    public bool moveDrill = false;
    public bool moveObject = false;

    public LayerMask controlPanelLayer;

    public Camera controlPanelCamera;

    void Start()
    {
        latheInteractable = FindObjectOfType<LatheInteractable>();
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
                            if (buttonName == "btn_CycleStart")
                            {
                                Debug.Log("Interacted with ControlPanel: " + hit.collider.gameObject.name);
                                //Calling object moving Coroutine
                                if (latheInteractable.isUncutObjectInCuttingPosition && !doorController.isDoorOpen && latheInteractable.isCutObjectInCuttingPosition)
                                {
                                    moveSupport = true;
                                }

                                if (supportController.isSupportInPlace == true)
                                {
                                    moveDrill = true;
                                    StartCoroutine(MoveUncutObjectLeft());
                                }

                                //Moving uncut object
                                if (isMachineActive && uncutObject.transform.position.x < maxLeftXPosition && moveObject == true)
                                {
                                    uncutObject.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
                                }
                            }
                            else if (buttonName == "btn_FeedHold")
                            {
                                Debug.Log("Interacted with ControlPanel: " + hit.collider.gameObject.name);
                            }
                        }
                    }
                }
            }
        }
    }

    IEnumerator MoveUncutObjectLeft()
    {
        moveDrill = true;
        isMachineActive = true;
        yield return new WaitForSeconds(14f);
        moveObject = true;
        yield return new WaitForSeconds(waitTime);
        isMachineActive = false;
        moveSupport = false;
        moveDrill = false;
        moveObject = false;
    }
}
