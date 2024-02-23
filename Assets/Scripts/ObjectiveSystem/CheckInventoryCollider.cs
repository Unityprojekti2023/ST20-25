using UnityEngine;

public class CheckInventoryCollider : MonoBehaviour
{
    public ObjectiveManager objectiveManager;
    public GameController gameController; // Reference to the GameController to access the currentStage
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
                //int newScore = Mathf.Max(0, currentScore - deductionPoints);
                int newScore = currentScore - deductionPoints;
                objectiveManager.DeductPoints(deductionPoints);
                Debug.Log($"Player hasn't picked up required items. Deducting {deductionPoints} points. New score: {newScore}");

                // Set the flag to true to prevent further triggers
                hasTriggered = true;
            }
        }
    }
}
