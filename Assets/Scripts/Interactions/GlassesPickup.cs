// ShoesHandler.cs
using UnityEngine;

public class GlassesPickup : MonoBehaviour, IInteractable
{
    public TextInformation textInfo;
    public GameObject glasses;
    public bool areGlassesEquipped = false;
    

    public ObjectiveManager objectiveManager;
    public void Interact()
    {
        // Logic for handling Shoes interaction
        InventoryManager.Instance.AddItem("Safety glasses");
        textInfo.UpdateText("Safety glasses added");
        objectiveManager.CompleteObjective("Put on safety glasses");
        areGlassesEquipped = true;
        glasses.SetActive(false);

    }
}
