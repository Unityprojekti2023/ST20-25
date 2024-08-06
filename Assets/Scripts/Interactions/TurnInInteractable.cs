using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnInInteractable : MonoBehaviour, IInteractable
{
    private TaskManager taskManager;

    void Start()
    {
        taskManager = FindObjectOfType<TaskManager>();
    }

    public void Interact()
    {
        if (InventoryManager.Instance.IsItemInInventory("cut item"))
        {
            GameObject item = InventoryManager.Instance.heldItem;
            Renderer itemRenderer = item.GetComponentInChildren<Renderer>();

            // Check if material is correct
            string currentMaterial = taskManager.GetCurrentMaterialName();

            //TODO: Make it so game wont tell if piece was correct or not in testing mode.


            if (item.CompareTag("WronglyCutItem"))
            {
                if (!itemRenderer.material.name.Contains(taskManager.GetMaterialType(currentMaterial)))
                {
                    // Deduct points if the item is wrongly cut and wrong material
                    InventoryManager.Instance.RemoveItemFromInventory("cut item", "Piece with Wrong material with mistakes turned in", transform);
                    PointDeduction(200);
                }
                else
                {
                    // Deduct points if the item is wrongly cut but correct material
                    InventoryManager.Instance.RemoveItemFromInventory("cut item", "Piece with mistakes turned in", transform);
                    PointDeduction(100);
                }

            }
            else
            {
                if (itemRenderer.material.name.Contains(taskManager.GetMaterialType(currentMaterial)))
                {
                    // Add points if the item is correctly cut and correct material
                    InventoryManager.Instance.RemoveItemFromInventory("cut item", "Correct piece turned in", transform);
                    PointAddition(200);
                }
                else
                {
                    // Add points if the item is correctly cut but wrong material
                    InventoryManager.Instance.RemoveItemFromInventory("cut item", "Piece with wrong material turned in.", transform);
                    PointAddition(100);
                }
            }
        }
        else
        {
            Debug.Log("Item not in inventory");
        }
    }

    private void PointAddition(int points)
    {
        ObjectiveManager.Instance.AddPoints(points);
        ObjectiveManager.Instance.CompleteObjective("Turn in cut piece");
    }

    private void PointDeduction(int points)
    {
        ObjectiveManager.Instance.DeductPoints(points);
        ObjectiveManager.Instance.CompleteObjective("Turn in cut piece");
    }
}
