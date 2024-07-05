// ShoesHandler.cs
using UnityEngine;

public class GlassesPickup : MonoBehaviour, IInteractable
{
    public GameObject glasses;
    public bool areGlassesEquipped = false;
    
    public void Interact()
    {
        if (areGlassesEquipped)
        {
            RemoveGlasses();
        }
        else
        {
            EquipGlasses();
        }
    }

    private void RemoveGlasses()
    {
        ObjectiveManager.Instance.CompleteObjective("Remove safety shoes");
        // Update the text information
        RayInteractor.instance.UpdateInteractionText(transform.name, "Put on glasses: [LMB] or [E]", InteractableType.HandleHoldInteraction);
        areGlassesEquipped = false;
        glasses.SetActive(true);
    }

    private void EquipGlasses()
    {
        ObjectiveManager.Instance.CompleteObjective("Put on safety shoes");
        // Update the text information
        RayInteractor.instance.UpdateInteractionText(transform.name, "Remove glasses: [LMB] or [E]", InteractableType.HandleHoldInteraction);
        areGlassesEquipped = true;
        glasses.SetActive(false);
    }
}
