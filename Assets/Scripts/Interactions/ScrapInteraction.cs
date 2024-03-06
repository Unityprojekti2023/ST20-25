using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapInteraction : MonoBehaviour
{
    public CleaningFeature cleaningFeature;
    public RayInteractor rayInteractor;
    public bool isPlayerNearScrapPiles = false;
    public bool isShovelFull = false;

    bool isPile1Cleaned = false;
    bool isPile2Cleaned = false;
    bool isPile3Cleaned = false;

    public Transform scrapPile1;
    public Transform scrapPile2;
    public Transform scrapPile3;

    void Update()
    {
        if(rayInteractor.shovelEquipped && isPlayerNearScrapPiles)
        {
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
            {
                if(!isShovelFull && !isPile1Cleaned && cleaningFeature.isPile1Visible)
                {
                    isPile1Cleaned = true;
                    isShovelFull = true;
                    scrapPile1.Translate(0, -10f, 0);
                    cleaningFeature.isPile1Visible = false;
                }

                if(!isShovelFull && isPile1Cleaned && !isPile2Cleaned && cleaningFeature.isPile2Visible)
                {
                    isPile2Cleaned = true;
                    isShovelFull = true;
                    scrapPile2.Translate(0, -10f, 0);
                    cleaningFeature.isPile2Visible = false;
                }

                if(!isShovelFull && isPile1Cleaned && isPile2Cleaned && !isPile3Cleaned && cleaningFeature.isPile3Visible)
                {
                    isPile3Cleaned = true;
                    isShovelFull = true;
                    scrapPile3.Translate(0, -10f, 0);
                    cleaningFeature.isPile3Visible = false;
                }
            }
        }
    }
}
