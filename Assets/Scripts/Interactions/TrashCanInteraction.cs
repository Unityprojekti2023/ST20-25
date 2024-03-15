using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanInteraction : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    public TextInformation textInfo;

    public void Interact()
    {
        textInfo.UpdateText("Scraps removed");
    }
}
