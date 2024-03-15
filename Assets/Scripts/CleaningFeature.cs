using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningFeature : MonoBehaviour
{
    [Header("References to objects")]
    public Transform scrapPile1;
    public Transform scrapPile2;
    public Transform scrapPile3;

    [Header("Variables")]
    public bool isPile1Visible = true;
    public bool isPile2Visible = true;
    public bool isPile3Visible = true;

    void Start()
    {
        scrapPile1.Translate(0, -10f, 0);               // Hiding scrap piles under the floor on startup
        isPile1Visible = false;

        scrapPile2.Translate(0, -10f, 0);
        isPile2Visible = false;

        scrapPile3.Translate(0, -10f, 0);
        isPile3Visible = false;
    }

    public void ShowScrapPile1()                        // Function to show scrap pile 1
    {
        if(!isPile1Visible)                             // Making sure the pile isn´t already in position
        {
            scrapPile1.Translate(0, 10f, 0);
            isPile1Visible = true;
        }
    }

    public void ShowScrapPile2()                        // Function to show scrap pile 2
    {
        if(!isPile2Visible)                             // Making sure the pile isn´t already in position
        {
            scrapPile2.Translate(0, 10f, 0);
            isPile2Visible = true;
        }
    }

    public void ShowScrapPile3()                        // Function to show scrap pile 3
    {
        if(!isPile3Visible)                             // Making sure the pile isn´t already in position
        {
            scrapPile3.Translate(0, 10f, 0);
            isPile3Visible = true;
        }
    }
}
