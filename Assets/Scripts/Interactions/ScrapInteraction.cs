using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapInteraction : MonoBehaviour, IInteractable
{
    [Header("References to other scripts")]
    public CleaningController cleaningController;

    [Header("Variables")]
    public bool isPlayerNearScrapPiles = false;
    
    public void Interact()
    {
        if (InventoryManager.Instance.HasItem("Shovel"))
        {
            
        }
        else
        {
            Debug.Log("You need a shovel to clean the scrap piles.");
        }

    }
}
