using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningController : MonoBehaviour
{
    public GameObject[] scrapPile1;
    int shovelPileNumber = 0;

    //TODO: See if need to set material for scrap piles to be same as lathes item.

    void Start()
    {
        // Hide all scrap piles at the start of the game
        foreach (GameObject pile in scrapPile1)
        {
            pile.SetActive(false);
        }
    }

    void HideScrapPile(int pileNumber)
    {
        // Hide the scrap pile based on the pile number
        scrapPile1[pileNumber].SetActive(false);
        shovelPileNumber = pileNumber;
    }

    void ThrowAwayScrapPile()
    {
        // Destroy the scrap pile based on the pile number
        Destroy(scrapPile1[shovelPileNumber]);

        // Ceck if right trash can is selected

    }
}
