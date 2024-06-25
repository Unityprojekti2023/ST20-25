using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LockerController : MonoBehaviour, IInteractable
{
    public RayInteractor rayInteractor;
    public TextInformation textInfo;

    public GameObject hinge;
    public Transform player;
    public Transform lockerDoor;


    public bool isLockerDoorOpen = false;

    private float totalRotation = 0f;
    private float maxRotation = 90f;

    //experimenting, would make much more sense to handle door opening with rays
    void Update()
    {
        float rotationAmount = 90f * Time.deltaTime;

        if ((lockerDoor.transform.position - player.position).magnitude < 120.0f)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }

            if (isLockerDoorOpen && totalRotation < maxRotation)
            {
                float remainingRotation = maxRotation - totalRotation;
                if (rotationAmount > remainingRotation)
                    rotationAmount = remainingRotation;

                transform.RotateAround(hinge.transform.position, Vector3.down, rotationAmount);
                totalRotation += rotationAmount;
            }
            //else if (!isLockerDoorOpen && totalRotation > 0)
            //{
            //    float remainingRotation = totalRotation;
            //    if (rotationAmount > remainingRotation)
            //        rotationAmount = remainingRotation;

            //    transform.RotateAround(hinge.transform.position, Vector3.up, rotationAmount);
            //    totalRotation -= rotationAmount;
            //}
        }
    }

    public void Interact()
    {
        //textInfo.UpdateText("door");
        isLockerDoorOpen = !isLockerDoorOpen;

    }

}
