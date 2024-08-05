using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public static class ScoreValues
{
    public const int HIGH = 200;
    public const int MEDIUM = 100;
    public const int LOW = 50;
}

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance { get; private set; }

    public ObjectiveUI objectiveUI;
    public TextMeshProUGUI scoreText;
    private Dictionary<int, List<Objective>> stageObjectives = new();
    int currentStage = 0;
    int score;

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

        InitializeObjectives();
    }

    private void InitializeObjectives()
    {
        stageObjectives[0] = new List<Objective>()
        {
        new("Put on safety shoes", ScoreValues.HIGH),
        new("Put on safety glasses", ScoreValues.HIGH)
        };
        stageObjectives[1] = new List<Objective>()
        {
        new("Pick up the clipboard", ScoreValues.HIGH),
        new("Inspect the drawing", ScoreValues.LOW), //Optional objective
        new("Place the clipboard on the worktable", ScoreValues.LOW)
        };
        stageObjectives[2] = new List<Objective>()
        {
        new("Pick up correct blank", ScoreValues.HIGH),
        new("Place piece into the machine", ScoreValues.LOW)
        };
        stageObjectives[3] = new List<Objective>()
        {
        new("Turn on the lathe", ScoreValues.HIGH),
        new("Initialize the lathe", ScoreValues.LOW)
        };
        stageObjectives[4] = new List<Objective>()
        {
        new("Select a program", ScoreValues.MEDIUM),
        new("Run a program", ScoreValues.LOW)
        };
        stageObjectives[5] = new List<Objective>()
        {
        new("Pick up cut piece", ScoreValues.MEDIUM),
        new("Place cut piece on the table", ScoreValues.LOW)
        };
        stageObjectives[6] = new List<Objective>()
        {
        new("Equip shovel", ScoreValues.MEDIUM),
        new("Clean metal scraps", 0) //Score is given by cleaning script that call objective Complete
        };
        stageObjectives[7] = new List<Objective>()
        {
        new("Unequip shovel", ScoreValues.LOW),
        new("Turn the lathe off", ScoreValues.MEDIUM)
        };
        stageObjectives[8] = new List<Objective>()
        {
        new("Equip caliper", ScoreValues.LOW),
        new("Use caliper to measure the piece", ScoreValues.MEDIUM),
        new("Unequip caliper", ScoreValues.LOW),
        new("Turn in cut piece", 0) //Score is given by turnin script that call objective Complete
        };
        stageObjectives[9] = new List<Objective>()
        {
        new("Remove safety shoes", ScoreValues.LOW),
        new("Remove safety glasses", ScoreValues.LOW)
        };
        stageObjectives[10] = new List<Objective>()
        {
        new("End assignment by exiting through the corridor door", 0)
        };

    }

    private void Update()
    {
        scoreText.text = $"Score: {score}";

        if (stageObjectives[currentStage].All(obj => obj.isCompleted))
        {
            GoToNextStage();
        }
    }

    // Mark an objective as completed and update the score
    public void CompleteObjective(string description)
    {
        foreach (var stage in stageObjectives)
        {
            Objective objective = stage.Value.FirstOrDefault(obj => obj.Description == description);

            if (objective != null && !objective.isCompleted)
            {
                objective.isCompleted = true;
                score += objective.Points;

                objectiveUI.UpdateObjectiveList();
                break;
            }
        }

    }

    public List<Objective> GetCurrentStageObjectives()
    {
        if (stageObjectives.ContainsKey(currentStage))
        {
            return stageObjectives[currentStage];
        }
        return new List<Objective>();
    }

    // Get all objectives completed from all stages
    public List<Objective> GetAllObjectivesCompleted()
    {
        List<Objective> allObjectives = new();
        foreach (var stage in stageObjectives)
        {
            allObjectives.AddRange(stage.Value.FindAll(obj => obj.isCompleted));
        }
        return allObjectives;
    }

    public void GoToNextStage()
    {
        currentStage++;
        if (stageObjectives.ContainsKey(currentStage))
        {
            objectiveUI.UpdateObjectiveList();
        }
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
