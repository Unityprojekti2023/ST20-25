using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelController : MonoBehaviour
{
    public GameObject playerEmptyShovel;
    public GameObject playerFullShovel;
    public GameObject propShovel;
    public ScrapInteraction scrapInteraction;
    
    void Start()
    {
        playerEmptyShovel.SetActive(false);     // Setting both shovels attached to the player as inactive on startup
        playerFullShovel.SetActive(false);
    }

    void Update()
    {
        // TODO: Fix this code
        /*if(InventoryManager.Instance.handsFull && !scrapInteraction.isShovelFull)      // Checking if shovel is equipped and shovel is not full
        {
            playerEmptyShovel.SetActive(true);                                  // Setting empty shovel as active
            playerFullShovel.SetActive(false);
            propShovel.SetActive(false);
            ObjectiveManager.Instance.CompleteObjective("Equip shovel");
        } 
        else if(InventoryManager.Instance.handsFull && scrapInteraction.isShovelFull)  // Checking if shovel is equipped and shovel is full
        {
            playerEmptyShovel.SetActive(false);
            playerFullShovel.SetActive(true);                                   // Setting full shovel as active
            propShovel.SetActive(false);
        } 
        else 
        {
            playerEmptyShovel.SetActive(false);
            playerFullShovel.SetActive(false);
            propShovel.SetActive(true);                                         // If shovel is not equipped, setting prop shovel (on the table) active

            ObjectiveManager.Instance.CompleteObjective("Unequip shovel");
        }*/
    }
}
