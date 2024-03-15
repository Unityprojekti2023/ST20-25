using UnityEngine;

public class CheckInventoryCollider : MonoBehaviour
{
    [Header("References to other scripts")]
    public ObjectiveManager objectiveManager;
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
                // Deduct points if the objectives are not completed
                int currentScore = objectiveManager.GetCurrentScore();
                int newScore = currentScore - deductionPoints;
                objectiveManager.DeductPoints(deductionPoints);
                Debug.Log($"Player hasn't picked up required items. Deducting {deductionPoints} points. New score: {newScore}");

                // Set the flag to true to prevent further triggers
                hasTriggered = true;
            }
        }
    }
}
