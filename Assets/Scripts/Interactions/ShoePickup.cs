// ShoesHandler.cs
using UnityEngine;

public class ShoesPickup : MonoBehaviour, IInteractable
{
    public TextInformation textInfo;
    public ObjectiveManager objectiveManager;
    public GameObject shoes;
    public bool areShoesEquipped = false;

    public void Interact()
    {
        // Logic for handling Shoes interaction
        InventoryManager.Instance.AddItem("Shoes");
        textInfo.UpdateText("Shoes added");
        objectiveManager.CompleteObjective("Put on safety shoes");
        areShoesEquipped = true;
        shoes.SetActive(false);
    }
}