using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningAreaTrigger : MonoBehaviour
{
    public ScrapInteraction scrapInteraction;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerModel")
        {
            scrapInteraction.isPlayerNearScrapPiles = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "PlayerModel")
        {
            scrapInteraction.isPlayerNearScrapPiles = false;
        }
    }
}
