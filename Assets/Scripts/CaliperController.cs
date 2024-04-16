using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaliperController : MonoBehaviour
{
    public GameObject playerCaliper;
    public GameObject tableCaliber;
    public ObjectiveManager objectiveManager;
    public RayInteractor rayInteractor;

    void Start()
    {
        playerCaliper.SetActive(false);     // Setting players caliber invisible and table visible
        tableCaliber.SetActive(true);
    }

    void Update()
    {
        if (rayInteractor.caliperEquipped)      // Checking if caliber is equipped
        {
            playerCaliper.SetActive(true);                                  
            tableCaliber.SetActive(false);
            objectiveManager.CompleteObjective("Equip caliper");
        }
        else if (!rayInteractor.caliperEquipped)  
        {
            playerCaliper.SetActive(false);
            tableCaliber.SetActive(true);
            objectiveManager.CompleteObjective("Unequip caliper");
        }
    }
}
