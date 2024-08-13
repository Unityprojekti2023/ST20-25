using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CleaningController : MonoBehaviour
{
    public GameObject[] scrapPile;
    public GameObject shovel;

    public GameObject[] trashCans;
    int cleaningCounter = 0;
    public string scrapMaterial = "";
    bool isShovelEmpty = true;

    private Dictionary<GameObject, string> trashCanMaterial = new();

    //TODO: See if need to set material for scrap piles to be same as lathes item.

    void Start()
    {
        // Hide all scrap piles at the start of the game
        foreach (GameObject pile in scrapPile)
        {
            pile.SetActive(false);
        }

        // Populate the dictionary with materials and corresponding trash cans
        trashCanMaterial.Add(trashCans[0], "metal02_diffuse");
        trashCanMaterial.Add(trashCans[1], "metal06_diffuse");

    }

    public void HandleCleaning(GameObject gameObject)
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the player is pressing the interact button
            if (InventoryManager.Instance.IsItemInInventory("Shovel"))
            {
                // Check if the scrap pile is active
                if (gameObject.CompareTag("Cleanable") && isShovelEmpty)
                {
                    isShovelEmpty = false;
                    scrapMaterial = gameObject.transform.GetChild(0).GetComponent<Renderer>().material.name;

                    // Find scrap pile object of shovel
                    GameObject scrapPileObject = shovel.transform.Find("Scraps").gameObject;
                    // Set material of shovels scrap piles children to that of picked up scrap pile
                    foreach (Transform child in scrapPileObject.transform)
                    {
                        child.GetComponent<Renderer>().material = gameObject.transform.GetChild(0).GetComponent<Renderer>().material;
                    }
                    scrapPileObject.SetActive(true);

                    // Hide the scrap pile
                    HideScrapPile(Array.IndexOf(scrapPile, gameObject));
                }
                else if (gameObject.CompareTag("TrashCan") && !isShovelEmpty)
                {
                    // Throw away the scrap pile
                    ThrowAwayScrapPile(gameObject);
                }
                else
                {
                    return;
                }
            }
            else if (InventoryManager.Instance.IsItemInInventory("cut item"))
            {
                ThrowAwayCutItem(gameObject);
            }

        }

    }

    public void ShowScrapPile(int pileNumber, Material material)
    {
        // Show the scrap pile based on the pile number
        scrapPile[pileNumber].SetActive(true);
        // set the material of the scrap piles children to the same as the lathe item
        foreach (Transform child in scrapPile[pileNumber].transform)
        {
            child.GetComponent<Renderer>().material = material;
        }
    }

    public void ShowAllScrapPiles(Material material)
    {
        foreach (GameObject pile in scrapPile)
        {
            pile.SetActive(true);
            foreach (Transform child in pile.transform)
            {
                child.GetComponent<Renderer>().material = material;
            }
        }
    }

    void HideScrapPile(int pileNumber)
    {
        // Hide the scrap pile based on the pile number
        scrapPile[pileNumber].SetActive(false);
        cleaningCounter++;
    }

    private void ThrowAwayCutItem(GameObject trashCan)
    {
        if (trashCan.CompareTag("TrashCan"))
        {
            GameObject item = InventoryManager.Instance.heldItem;
            string itemMaterial = item.transform.GetChild(0).GetComponent<Renderer>().material.name;

            // Check if the material of the item is the same as the trash can
            if (itemMaterial.Contains(trashCanMaterial[trashCan]))
            {
                if (item.CompareTag("WronglyCutItem"))
                {
                    // Add points if the item is wrongly cut and right trash can is used
                    ObjectiveManager.Instance.AddPoints(ScoreValues.MEDIUM);
                }
                else
                {
                    // Deduct points if the item is not wrongly cut
                    ObjectiveManager.Instance.DeductPoints(ScoreValues.LOW);
                }
            }
            else
            {
                // Deduct points if the item is not of the same material as the trash can
                ObjectiveManager.Instance.DeductPoints(ScoreValues.MEDIUM);
            }
            InventoryManager.Instance.RemoveItemFromInventory("cut item", "Item thrown away");
            Destroy(item);
        }

    }

    void ThrowAwayScrapPile(GameObject trashCan)
    {
        // Get trashcans material from the dictionary
        string trashCanMaterialName = trashCanMaterial[trashCan];

        // Compare the material of the scrap pile with the material assigned to the trash can
        if (scrapMaterial.Contains(trashCanMaterialName))
        {
            if (cleaningCounter == scrapPile.Length)
            {
                ObjectiveManager.Instance.AddPoints(ScoreValues.LOW);
                ObjectiveManager.Instance.CompleteObjective("Clean metal scraps");
            }
            else
            {
                ObjectiveManager.Instance.AddPoints(ScoreValues.LOW);
            }
            EmptyShovel();
        }
        else
        {
            if (cleaningCounter == scrapPile.Length)
            {
                ObjectiveManager.Instance.DeductPoints(ScoreValues.LOW);
                ObjectiveManager.Instance.CompleteObjective("Clean metal scraps");
            }
            else
            {
                ObjectiveManager.Instance.DeductPoints(ScoreValues.LOW);
            }

            EmptyShovel();
        }


    }

    private void EmptyShovel()
    {
        isShovelEmpty = true;
        shovel.transform.Find("Scraps").gameObject.SetActive(false);
    }
}
