using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineScript : MonoBehaviour
{
    [Header("Objects")]
    public Transform uncutObject;
    public Transform cutObject1;
    public Transform cutObject2;
    public Transform partiallyCutObject1;
    public Transform partiallyCutObject2;
    public Transform partiallyCutObject3;
    public Transform partiallyCutObject4;
    public Transform partiallyCutObject5;

    [Header("References to other scripts")]
    public SupportController supportController;
    public ControlPanelInteractable mouseControlPanelInteractable;
    public LatheMiddleTrigger latheMiddleTrigger;

    [Header("Movement Values")]
    public float maxLeftXPosition = -113.5f;
    public float movementSpeed = 1.3f;
    public float waitTime = 20;

    [Header("Boolean Variables")]
    public bool isUncutObjectInCuttingPosition = false;
    public bool isMachineActive = false;
    public bool moveSupport = false;
    public bool moveDrill = false;
    public bool moveObject = false;
    public bool isAnimationComplete = false;

    void Start()
    {
        uncutObject.position = new Vector3(-100f, 102.4f, 182.7f);                                  //Hiding the uncut and cut objects on start
        cutObject1.position = new Vector3(-100f, 102.4f, 182.7f);
        cutObject2.position = new Vector3(-100f, 102.4f, 182.7f);

        partiallyCutObject1.position = new Vector3(-100f, 102.4f, 182.7f);
        partiallyCutObject2.position = new Vector3(-100f, 102.4f, 182.7f);
        partiallyCutObject3.position = new Vector3(-100f, 102.4f, 182.7f);
        partiallyCutObject4.position = new Vector3(-100f, 102.4f, 182.7f);
        partiallyCutObject5.position = new Vector3(-100f, 102.4f, 182.7f);
    }

    void Update()
    {
        if (supportController.isSupportInLeftSpot && !moveDrill)                                    // Checking if the support is in place and the drill isnt moving already
        {
            moveDrill = true;                                                                       // Setting moveDrill to true, so the drill can begin moving
        }

        if(latheMiddleTrigger.moveUncutObject){
            uncutObject.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
        }

        if(latheMiddleTrigger.movePartiallyCutObject1){
            partiallyCutObject1.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
        }

        if(latheMiddleTrigger.movePartiallyCutObject2){
            partiallyCutObject2.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
        }

        if(latheMiddleTrigger.movePartiallyCutObject3){
            partiallyCutObject3.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
        }

        if(latheMiddleTrigger.movePartiallyCutObject4){
            partiallyCutObject4.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
        }

        if(latheMiddleTrigger.movePartiallyCutObject5){
            partiallyCutObject5.transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
        }

    }

    public void moveUncutObjectToCuttingPosition(){
        uncutObject.position = new Vector3(-130f, 102.4f, 182.7f);
        isUncutObjectInCuttingPosition = true;
    }

    public void removeUncutObjectFromCuttingPosition(){
        uncutObject.position = new Vector3(-100f, 102.4f, 182.7f);
        isUncutObjectInCuttingPosition = false;
    }

    public void moveCutObject1ToCuttingPosition(){
        cutObject1.position = new Vector3(-131.2f, 102.4f, 182.7f);

        partiallyCutObject1.position = new Vector3(-130f, 102.4f, 182.7f);
        partiallyCutObject2.position = new Vector3(-130f, 102.4f, 182.7f);
        partiallyCutObject3.position = new Vector3(-130f, 102.4f, 182.7f);
        partiallyCutObject4.position = new Vector3(-130f, 102.4f, 182.7f);
        partiallyCutObject5.position = new Vector3(-130f, 102.4f, 182.7f);
    }

    public void removeCutObject1FromCuttingPosition(){
        cutObject1.position = new Vector3(-100f, 102.4f, 182.7f);

        partiallyCutObject1.position = new Vector3(-100f, 102.4f, 182.7f);
        partiallyCutObject2.position = new Vector3(-100f, 102.4f, 182.7f);
        partiallyCutObject3.position = new Vector3(-100f, 102.4f, 182.7f);
        partiallyCutObject4.position = new Vector3(-100f, 102.4f, 182.7f);
        partiallyCutObject5.position = new Vector3(-100f, 102.4f, 182.7f);
    }

    public void moveCutObject2ToCuttingPosition(){
        cutObject2.position = new Vector3(-131.2f, 102.4f, 182.7f);

        partiallyCutObject1.position = new Vector3(-130f, 102.4f, 182.7f);
        partiallyCutObject3.position = new Vector3(-130f, 102.4f, 182.7f);
        partiallyCutObject5.position = new Vector3(-130f, 102.4f, 182.7f);
    }

    public void removeCutObject2FromCuttingPosition(){
        cutObject2.position = new Vector3(-100f, 102.4f, 182.7f);

        partiallyCutObject1.position = new Vector3(-100f, 102.4f, 182.7f);
        partiallyCutObject3.position = new Vector3(-100f, 102.4f, 182.7f);
        partiallyCutObject5.position = new Vector3(-100f, 102.4f, 182.7f);
    }
}
