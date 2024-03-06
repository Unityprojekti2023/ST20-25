using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelController : MonoBehaviour
{
    public GameObject playerEmptyShovel;
    public GameObject playerFullShovel;
    public GameObject propShovel;

    public RayInteractor rayInteractor;
    public ScrapInteraction scrapInteraction;
    
    void Start()
    {
        playerEmptyShovel.SetActive(false);
        playerFullShovel.SetActive(false);
    }

    void Update()
    {
        if(rayInteractor.shovelEquipped && !scrapInteraction.isShovelFull)
        {
            playerEmptyShovel.SetActive(true);
            playerFullShovel.SetActive(false);
            propShovel.SetActive(false);
        } 
        else if(rayInteractor.shovelEquipped && scrapInteraction.isShovelFull)
        {
            playerEmptyShovel.SetActive(false);
            playerFullShovel.SetActive(true);
            propShovel.SetActive(false);
        } 
        else 
        {
            playerEmptyShovel.SetActive(false);
            playerFullShovel.SetActive(false);
            propShovel.SetActive(true);
        }
    }
}
