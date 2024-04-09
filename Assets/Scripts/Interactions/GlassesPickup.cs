// ShoesHandler.cs
using UnityEngine;

public class GlassesPickup : MonoBehaviour, IInteractable
{
    public InventoryManager inventoryManager;
    public TextInformation textInfo;
    public Transform safetyGlasses;
    public bool areGlassesEquipped = false;
    

    public ObjectiveManager objectiveManager;
    public void Interact()
    {
        // Logic for handling Shoes interaction
        inventoryManager.AddItem("Safety glasses");
        textInfo.UpdateText("Safety glasses added");
        objectiveManager.CompleteObjective("Put on safety glasses");
        //safetyGlasses.SetActive(false);
        areGlassesEquipped = true;
        safetyGlasses.Translate(0, -100f, 0);

    }
}
