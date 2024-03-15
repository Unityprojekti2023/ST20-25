using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelPickup : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    public TextInformation textInfo;
    public ShovelController shovelController;
    public RayInteractor rayInteractor;

    public void Interact()
    {
        if(!rayInteractor.shovelEquipped)
        {
            textInfo.UpdateText("Shovel equipped");         
        }

        if(rayInteractor.shovelEquipped)
        {
            textInfo.UpdateText("Shovel unequipped");
        }
    }
}
