using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaliperPickup : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    public TextInformation textInfo;
    public CaliperController caliperController;
    public RayInteractor rayInteractor;

    public void Interact()
    {
        // TODO: Check for caliper equipped status
        textInfo.UpdateText("Caliper equipped");

        //if (rayInteractor.caliperEquipped)
        //{
        //    textInfo.UpdateText("Caliber unequipped");
        //}
    }
}
