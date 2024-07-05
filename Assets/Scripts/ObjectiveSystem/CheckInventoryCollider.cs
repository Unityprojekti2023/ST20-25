using UnityEngine;

public class CheckInventoryCollider : MonoBehaviour
{
    [Header("References to other scripts")]
    public GameController gameController;

    [Header("Other values")]
    public int deductionPoints = 50; // Adjust the points to deduct as needed
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("PlayerModel"))
        {
            // Check if the current stage is past the first one
            if (gameController.currentStage == 0)
            {
                ObjectiveManager.Instance.DeductPoints(deductionPoints);

                // Set the flag to true to prevent further triggers
                hasTriggered = true;
            }
        }
    }
}
