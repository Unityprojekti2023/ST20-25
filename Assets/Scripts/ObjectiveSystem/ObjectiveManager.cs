using System.Linq;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public System.Collections.Generic.List<Objective> objectives = new();

    // Add an objective to the list
    public void AddObjective(string description)
    {
        objectives.Add(new Objective(description));
    }

    // Clear objective list
    public void EmptyObjective()
    {
        objectives.Clear();
    }

    // Mark an objective as completed
    public void CompleteObjective(string description)
    {
        Objective obj = objectives.Find(o => o.description == description);
        if (obj != null)
            obj.isCompleted = true;
    }

    // Check if all objectives are completed
    public bool AreAllObjectivesCompleted()
    {
        return objectives.All(obj => obj.isCompleted);
    }
}
