using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningFeature : MonoBehaviour
{
    [Header("References to objects")]
    // Array of Transform objects to hold the scrap piles
    public GameObject[] scrapPile1;

    [Header("Variables")]
    public bool isPile1Visible = true;
    public bool isPile2Visible = true;
    public bool isPile3Visible = true;

    void Start()
    {
        // Hide all scrap piles at the start of the game
        foreach (GameObject pile in scrapPile1)
        {
           pile.SetActive(false);
        }
    }

    public void ShowScrapPile(int pileNumber)
    {
        // Show the scrap pile based on the pile number
        scrapPile1[pileNumber].SetActive(true);
    }
}
