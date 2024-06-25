using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemPlacementSpot : MonoBehaviour, IInteractable
{
    public string requiredItemID = "CutItem"; // The item ID required for placement
    public TextInformation textInfo;
    public EscapeMenu escapeMenu;
    public MouseControlPanelInteractable mouseControlPanelInteractable;
    public RawPiecePickup itemPickup;

    public GameObject hiddenItem;
    public GameObject hiddenItem2;
    public GameObject hiddenItemMalfunction;
    public GameObject hiddenItem2Malfunction;

    public MistakeGenerator mistakeGenerator;

    private void Start()
    {
        hiddenItem.SetActive(false);
        hiddenItem2.SetActive(false);
        hiddenItemMalfunction.SetActive(false);
        hiddenItem2Malfunction.SetActive(false);
    }

    public void Interact()
    {
        //TODO: Takes this to lathe controller
        // This will generate mistake always when the item is placed on the spot
        hiddenItem.SetActive(true);
        Transform[] allParts = hiddenItem.transform.GetComponentsInChildren<Transform>();
        List<Transform> parts = new();
        foreach (Transform part in allParts)
        {
            if (part.name.Contains("Cylinder"))
            {
                parts.Add(part);
            }
        }

        mistakeGenerator.GenerateMistakes(parts.ToArray());

        textInfo.UpdateText("Item [Cut item 1] removed");
        //InventoryManager.Instance.RemoveItem("CutItem", "Item [Cut item 1] removed");
        mouseControlPanelInteractable.whichCutItemWasLathed = " ";
        //itemPickup.isUncutItemAlreadyInInventory = false;
        ObjectiveManager.Instance.CompleteObjective("Place cut piece on the table");
    }
}
