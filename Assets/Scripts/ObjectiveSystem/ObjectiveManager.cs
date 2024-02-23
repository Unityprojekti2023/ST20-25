using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public List<Objective> objectives = new List<Objective>();
    private int score;

    // Add an objective to the list
    public void AddObjective(string description, int score)
    {
        objectives.Add(new Objective(description, score));
    }

    // Clear objective list
    public void EmptyObjective()
    {
        objectives.Clear();
    }

    // Mark an objective as completed and update the score
    public void CompleteObjective(string description)
    {
        Objective obj = objectives.Find(o => o.description == description);
        if (obj != null && !obj.isCompleted)
        {
            obj.isCompleted = true;
            score += obj.scoreValue;
        }
    }

    // Check if all objectives are completed
    public bool AreAllObjectivesCompleted()
    {
        return objectives.All(obj => obj.isCompleted);
    }

    // Check if specific objectives are completed
    public bool AreSpecificObjectivesCompleted(params string[] descriptions)
    {
        // Check if all specified objectives are completed
        return descriptions.All(description => objectives.Any(obj => obj.description == description && obj.isCompleted));
    }

    public int GetCurrentScore()
    {
        return score;
    }

    public void DeductPoints(int deductionPoints)
    {
        //score = Mathf.Max(0, score - deductionPoints);
        score = score - deductionPoints;
    }
}
