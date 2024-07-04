using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreEvent
{
    public string description;
    public int scoreChange;

    public ScoreEvent(string description, int scoreChange)
    {
        this.description = description;
        this.scoreChange = scoreChange;
    }
}

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance { get; private set; }

    public List<Objective> objectives = new List<Objective>();
    private int score;
    private List<ScoreEvent> scoreEvents = new List<ScoreEvent>(); // List to store score events

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
            int scoreChange = obj.scoreValue;
            score += scoreChange;
            AddScoreEvent(description, scoreChange); // Add score event
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

    //Return current score
    public int GetCurrentScore()
    {
        return score;
    }

    //Method to add a score event
    private void AddScoreEvent(string description, int scoreChange)
    {
        ScoreEvent scoreEvent = new ScoreEvent(description, scoreChange);
        scoreEvents.Add(scoreEvent);
    }

    // Method to retrieve score events
    public List<ScoreEvent> GetScoreEvents()
    {
        return scoreEvents;
    }

    public void DeductPoints(int deductionPoints)
    {
        score -= deductionPoints;
        AddScoreEvent("Deduct Points", -deductionPoints); // Add score event for deduction
    }

    internal void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
    }
}
