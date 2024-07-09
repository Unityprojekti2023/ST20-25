using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance { get; private set; }

    public List<Objective> allObjectives = new();
    public List<Objective> currentObjectives = new();

    private int score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Add an objective to the list
    public void AddObjective(string description, int score)
    {
        currentObjectives.Add(new Objective(description, score));
    }

    // Clear objective list
    public void EmptyObjective()
    {
        currentObjectives.Clear();
    }

    // Mark an objective as completed and update the score
    public void CompleteObjective(string description)
    {
        //Add completed objective to AllObjectives list
        allObjectives.Add(currentObjectives.Find(o => o.Description == description));

        Objective obj = currentObjectives.Find(o => o.Description == description);
        if (obj != null && !obj.isCompleted)
        {
            obj.isCompleted = true;
            int scoreChange = obj.Points;
            score += scoreChange;
        }
    }

    // Check if all objectives are completed
    public bool AreAllObjectivesCompleted()
    {
        return currentObjectives.All(obj => obj.isCompleted);
    }

    //Return current score
    public int GetCurrentScore()
    {
        return score;
    }

    public void DeductPoints(int deductionPoints)
    {
        score -= deductionPoints;
    }

    internal void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
    }
}
