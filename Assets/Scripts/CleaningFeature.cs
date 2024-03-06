using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningFeature : MonoBehaviour
{
    public Transform scrapPile1;
    public Transform scrapPile2;
    public Transform scrapPile3;

    public bool isPile1Visible = true;
    public bool isPile2Visible = true;
    public bool isPile3Visible = true;

    void Start()
    {
        HideScrapPile1();
        HideScrapPile2();
        HideScrapPile3();
    }

    public void ShowScrapPile1()
    {
        if(!isPile1Visible)
        {
            scrapPile1.Translate(0, 10f, 0);
            isPile1Visible = true;
        }
    }

    public void ShowScrapPile2()
    {
        if(!isPile2Visible)
        {
            scrapPile2.Translate(0, 10f, 0);
            isPile2Visible = true;
        }
    }

    public void ShowScrapPile3()
    {
        if(!isPile3Visible)
        {
            scrapPile3.Translate(0, 10f, 0);
            isPile3Visible = true;
        }
    }

    public void HideScrapPile1()
    {
        if(isPile1Visible)
        {
            scrapPile1.Translate(0, -10f, 0);
            isPile1Visible = false;
        }
    }

    public void HideScrapPile2()
    {
        if(isPile2Visible)
        {
            scrapPile2.Translate(0, -10f, 0);
            isPile2Visible = false;
        }
    }

    public void HideScrapPile3()
    {
        if(isPile3Visible)
        {
            scrapPile3.Translate(0, -10f, 0);
            isPile3Visible = false;
        }
    }
}
