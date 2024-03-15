using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkAreaCollider : MonoBehaviour
{
    [Header("References to other scripts")]
    public GlassesPickup glassesPickup;
    public ShoesPickup shoePickup;
    public TextInformation textInformation;
    public ObjectiveManager objectiveManager;

    [Header("Variables")]
    public bool hasPlayerEnteredWorkArea = false;

    private void OnTriggerEnter(Collider other)                                                                 //Checking for collisions
    {
        if(other.gameObject.tag == "PlayerModel")                                                               //Making sure we collided with the correct object
        {
            if(!hasPlayerEnteredWorkArea)                                                                       //Checking if the player has already entered the work area during this playthrough
            {
                if(!glassesPickup.areGlassesEquipped && !shoePickup.areShoesEquipped)                           //If neither shoes or glasses are equipped, deducting 200 points
                {
                    textInformation.UpdateText("-200 Points for not equipping shoes or safetyglasses!");
                    objectiveManager.DeductPoints(200);
                    hasPlayerEnteredWorkArea = true;
                } 

                else if(glassesPickup.areGlassesEquipped && !shoePickup.areShoesEquipped)                       //If shoes are not equipped, but glasses are, deducting 100 points
                {
                    textInformation.UpdateText("-100 Points for not equipping shoes!");
                    objectiveManager.DeductPoints(100);
                    hasPlayerEnteredWorkArea = true;
                } 

                else if(!glassesPickup.areGlassesEquipped && shoePickup.areShoesEquipped)                       //If glasses are not equipped, but shoes are, deducting 100 points
                {
                    textInformation.UpdateText("-100 Points for not equipping safetyglasses!");
                    objectiveManager.DeductPoints(100);
                    hasPlayerEnteredWorkArea = true;
                }
            }
        }
    }
}
