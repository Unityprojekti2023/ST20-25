using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningAreaTrigger : MonoBehaviour
{
    [Header("References to other scripts")]
    public ScrapInteraction scrapInteraction;

    private void OnTriggerEnter(Collider other)                 //Checking for collisions
    {
        if(other.gameObject.tag == "PlayerModel")               //Making sure we collided with the correct object
        {
            scrapInteraction.isPlayerNearScrapPiles = true; 
        }
    }

    private void OnTriggerExit(Collider other)                  //Checking for collisions
    {
        if(other.gameObject.tag == "PlayerModel")               //Making sure we collided with the correct object
        {
            scrapInteraction.isPlayerNearScrapPiles = false;
        }
    }
}
