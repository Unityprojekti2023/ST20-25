using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    private TextInformation textInfo;
    // Variable to check if hands are full
    private bool handsFull = false;

    private List<string> inventory = new();

    private void Start()
    {
        textInfo = FindObjectOfType<TextInformation>();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(string itemID, string itemInfoText = "")
    {
        Debug.Log("Picked item " + itemID);
        textInfo.UpdateText(itemInfoText);
        inventory.Add(itemID);
    }

    public bool HasItem(string itemID)
    {
        return inventory.Contains(itemID);
    }

    public void RemoveItem(string itemID, string itemInfoText = "")
    {
        Debug.Log("Removed item " + itemID);
        textInfo.UpdateText(itemInfoText);
        inventory.Remove(itemID);
    }

    public string HeldItemID()
    {
        if (inventory.Count > 0)
        {
            return inventory[^1];
        }
        return "";
    }

    public bool CheckHands()
    {
        return handsFull;
    }

    // Toggle handsFull
    public void ToggleHands()
    {
        handsFull = !handsFull;
    }
}
