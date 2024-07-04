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
    public TextMeshProUGUI interactText;
    int shovelPileNumber = 0;
    public string scrapMaterial = "";
    bool isShovelEmpty = true;

    private Dictionary<GameObject, string> trashCanMaterial = new();

    //TODO: See if need to set material for scrap piles to be same as lathes item.

    void Start()
    {
        // Hide all scrap piles at the start of the game
        foreach (GameObject pile in scrapPile)
        {
            pile.SetActive(true);
        }

        // Populate the dictionary with materials and corresponding trash cans
        trashCanMaterial.Add(trashCans[0], "metal02_diffuse");
        trashCanMaterial.Add(trashCans[1], "metal06_diffuse");

    }

    void Update()
    {
        // Check if the player is holding the shovel
        if (InventoryManager.Instance.HasItem("Shovel"))
        {
            // Raycast to check if the player is looking at a scrap pile on tag "Cleanable"
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, 300f))
            {
                if (hit.collider.CompareTag("Cleanable"))
                {
                    interactText.gameObject.SetActive(true);
                    interactText.text = "Clean scrap pile: [LMB]";
                    HandleCleaning(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("TrashCan"))
                {
                    interactText.gameObject.SetActive(true);
                    interactText.text = "Throw away scrap pile: [LMB]";
                    HandleCleaning(hit.collider.gameObject);
                }
                else
                {
                    interactText.gameObject.SetActive(false);
                }
            }
            else
            {
                return;
            }
        }
    }

    public void HandleCleaning(GameObject gameObject)
    {
        // Check if the player is pressing the interact button
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the scrap pile is active
            if (gameObject.tag == "Cleanable" && isShovelEmpty)
            {
                isShovelEmpty = false;
                scrapMaterial = gameObject.transform.GetChild(0).GetComponent<Renderer>().material.name;

                // Find scrap pile object of shovel
                GameObject scrapPileObject = shovel.transform.Find("Scraps").gameObject;
                scrapPileObject.SetActive(true);

                // Hide the scrap pile
                HideScrapPile(Array.IndexOf(scrapPile, gameObject));
            }
            else if (gameObject.tag == "TrashCan" && !isShovelEmpty)
            {
                // Throw away the scrap pile
                ThrowAwayScrapPile(gameObject);
            }
            else
            {
                return;
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

    void HideScrapPile(int pileNumber)
    {
        // Hide the scrap pile based on the pile number
        scrapPile[pileNumber].SetActive(false);
        shovelPileNumber = pileNumber;
    }

    void ThrowAwayScrapPile(GameObject trashCan)
    {
        // Get trashcans material from the dictionary
        string trashCanMaterialName = trashCanMaterial[trashCan];

        // Compare the material of the scrap pile with the material assigned to the trash can
        if( scrapMaterial.Contains(trashCanMaterialName))
        {
            // If the materials match, add points
            ObjectiveManager.Instance.CompleteObjective("Throw away scrap pile");
            EmptyShovel();
        }
        else
        {
            ObjectiveManager.Instance.DeductPoints(50);
            EmptyShovel();
        }
    }

    private void EmptyShovel()
    {
        isShovelEmpty = true;
        shovel.transform.Find("Scraps").gameObject.SetActive(false);
    }
}
