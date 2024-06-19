using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapInteraction : MonoBehaviour
{
    [Header("References to other scripts")]
    public CleaningFeature cleaningFeature;
    public RayInteractor rayInteractor;

    [Header("References to objects")]
    public Transform scrapPile1;
    public Transform scrapPile2;
    public Transform scrapPile3;

    [Header("Variables")]
    public bool isPlayerNearScrapPiles = false;
    public bool isShovelFull = false;
    public bool isPile1Cleaned = false;
    public bool isPile2Cleaned = false;
    public bool isPile3Cleaned = false;

    void Update()
    {
        if(rayInteractor.shovelEquipped && isPlayerNearScrapPiles && DoorController.Instance.isDoorOpen)                                 // Making sure shovel is equipped, player is near scrappiles and the door is open
        {
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))                                                        // Checking for inputs
            {
                if(!isShovelFull && !isPile1Cleaned && cleaningFeature.isPile1Visible)                                          // Making sure shovel isnt already full, pile 1 isnt already cleaned and pile 1 is visible
                {
                    isPile1Cleaned = true;                                                                                      // Marking Pile 1 as cleaned
                    isShovelFull = true;
                    scrapPile1.Translate(0, -10f, 0);
                    cleaningFeature.isPile1Visible = false;
                }

                if(!isShovelFull && isPile1Cleaned && !isPile2Cleaned && cleaningFeature.isPile2Visible)                        // Making sure shovel isnt already full, pile 2 isnt already cleaned and pile 2 is visible
                {
                    isPile2Cleaned = true;
                    isShovelFull = true;
                    scrapPile2.Translate(0, -10f, 0);
                    cleaningFeature.isPile2Visible = false;
                }

                if(!isShovelFull && isPile1Cleaned && isPile2Cleaned && !isPile3Cleaned && cleaningFeature.isPile3Visible)      // Making sure shovel isnt already full, pile 3 isnt already cleaned and pile 3 is visible
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
