// ShoesHandler.cs
using UnityEngine;

public class GlassesPickup : MonoBehaviour, IInteractable
{
    public TextInformation textInfo;
    public GameObject glasses;
    public bool areGlassesEquipped = false;
    
    public void Interact()
    {
        // Logic for handling Shoes interaction
        InventoryManager.Instance.AddItem("Safety glasses");
        textInfo.UpdateText("Safety glasses added");
        ObjectiveManager.Instance.CompleteObjective("Put on safety glasses");
        areGlassesEquipped = true;
        glasses.SetActive(false);

    }
}
