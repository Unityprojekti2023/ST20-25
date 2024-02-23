using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    private ObjectiveManager objectiveManager;
    public TextMeshProUGUI scoreText;
    public int currentStage = 0;

    void Start()
    {
        objectiveManager = GetComponent<ObjectiveManager>();

        // Initialize with the objectives of the first stage
        InitializeStageObjectives();
    }

    void Update()
    {
        // Check if all objectives are completed for the current stage
        if (objectiveManager.AreAllObjectivesCompleted())
        {
            // All objectives for the current stage are completed, proceed to the next stage
            Debug.Log($"Stage {currentStage + 1} objectives completed! Proceed to the next stage.");

            // Increase the stage counter
            currentStage++;

            // Empty objectives and add objectives for the next stage
            objectiveManager.EmptyObjective();
            InitializeStageObjectives();
        }

        //Display current score
        scoreText.text = $"Score: {objectiveManager.GetCurrentScore()}";
    }

    void InitializeStageObjectives()
    {
        // Add objectives based on the current stage
        switch (currentStage)
        {
            case 0:
                objectiveManager.AddObjective("Put on safety shoes", 100);
                objectiveManager.AddObjective("Put on safety glasses", 100);
                break;

            case 1:
                objectiveManager.AddObjective("Pick up uncut piece", 100);
                objectiveManager.AddObjective("Place piece in place", 100);
                break;

            // Add more cases for additional stages as needed

            default:
                Debug.Log("All stages completed!");
                break;
        }
    }
}
