using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelPickup : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    private readonly TextInformation textInfo;
    public ShovelController shovelController;

    public void Interact()
    {
        textInfo.UpdateText("Shovel equipped");

    }
}
