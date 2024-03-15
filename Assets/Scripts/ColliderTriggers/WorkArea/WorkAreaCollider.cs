using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkAreaCollider : MonoBehaviour
{
    public GlassesPickup glassesPickup;
    public ShoesPickup shoePickup;
    public TextInformation textInformation;
    public ObjectiveManager objectiveManager;
    public bool hasPlayerEnteredWorkArea = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerModel")
        {
            if(!hasPlayerEnteredWorkArea)
            {
                //If neither shoes or glasses are equipped
                if(!glassesPickup.areGlassesEquipped && !shoePickup.areShoesEquipped)
                {
                    textInformation.UpdateText("-200 Points for not equipping shoes or safetyglasses!");
                    objectiveManager.DeductPoints(200);
                    hasPlayerEnteredWorkArea = true;
                } 

                //If shoes are not equipped, but glasses are
                else if(glassesPickup.areGlassesEquipped && !shoePickup.areShoesEquipped)
                {
                    textInformation.UpdateText("-100 Points for not equipping shoes!");
                    objectiveManager.DeductPoints(100);
                    hasPlayerEnteredWorkArea = true;
                } 

                //If glasses are not equipped, but shoes are
                else if(!glassesPickup.areGlassesEquipped && shoePickup.areShoesEquipped)
                {
                    textInformation.UpdateText("-100 Points for not equipping safetyglasses!");
                    objectiveManager.DeductPoints(100);
                    hasPlayerEnteredWorkArea = true;
                }
            }
        }
    }
}
