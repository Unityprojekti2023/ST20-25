// ShoesHandler.cs
using System;
using UnityEngine;

public class ShoesPickup : MonoBehaviour, IInteractable
{
    public GameObject shoes;
    public bool areShoesEquipped = false;

    public void Interact()
    {
        if (areShoesEquipped)
            RemoveShoes();
        else
            EquipShoes();
    }

    private void RemoveShoes()
    {
        ObjectiveManager.Instance.CompleteObjective("Remove safety shoes");
        // Update the text information
        RayInteractor.instance.UpdateInteractionText(transform.name, "Put on shoes: [LMB] or [E]", InteractableType.HandleHoldInteraction);
        areShoesEquipped = false;
        shoes.SetActive(true);
    }

    private void EquipShoes()
    {
        ObjectiveManager.Instance.CompleteObjective("Put on safety shoes");
        // Update the text information
        RayInteractor.instance.UpdateInteractionText(transform.name, "Remove shoes: [LMB] or [E]", InteractableType.HandleHoldInteraction);
        areShoesEquipped = true;
        shoes.SetActive(false);
    }
}