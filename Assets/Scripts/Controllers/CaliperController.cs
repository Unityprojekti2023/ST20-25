using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaliperController : MonoBehaviour
{
    public GameObject playerCaliper;
    public GameObject tableCaliber;
    public RayInteractor rayInteractor;

    void Start()
    {
        playerCaliper.SetActive(false);     // Setting players caliber invisible and table visible
        tableCaliber.SetActive(true);
    }

    void Update()
    {
        // TODO: CHeck for caliper equipped status
        /*if (rayInteractor.caliperEquipped)      // Checking if caliber is equipped
        {
            playerCaliper.SetActive(true);                                  
            tableCaliber.SetActive(false);
        }
        else if (!rayInteractor.caliperEquipped)  
        {
            playerCaliper.SetActive(false);
            tableCaliber.SetActive(true);
            ObjectiveManager.Instance.CompleteObjective("Unequip caliper");
        }*/
    }
}
