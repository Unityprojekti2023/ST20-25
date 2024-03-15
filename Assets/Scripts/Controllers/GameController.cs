using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Import the SceneManagement namespace

public class GameController : MonoBehaviour
{
    private ObjectiveManager objectiveManager;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endScoreText;
    public TextMeshProUGUI objectiveList;
    public int currentStage = 0;
    public int numberofStages = 7;
    public GameObject endScreenUI;
    public GameObject gameUI;

    void Start()
    {
        if (endScreenUI != null)
        {
            endScreenUI.SetActive(false);
        }

        objectiveManager = GetComponent<ObjectiveManager>();

        if (PlayerPrefs.GetInt("HideUI", 0) == 1)
        {
            // Hide UI element if it exists
            if (objectiveList != null)
            {
                objectiveList.gameObject.SetActive(false);
            }
            if (scoreText != null)
            {
                scoreText.gameObject.SetActive(false);
            }
        }

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

            // Check if all stages are completed
            if (currentStage >= numberofStages)
            {
                TransitionToEndScreen();
            }
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

            case 2:
                objectiveManager.AddObjective("Turn on the lathe", 100);
                objectiveManager.AddObjective("Initialize the lathe", 100);
                break;

            case 3:
                objectiveManager.AddObjective("Select a program", 100);
                objectiveManager.AddObjective("Run a program", 100);
                break;
            
            case 4:
                objectiveManager.AddObjective("Pick up cut piece", 100);
                objectiveManager.AddObjective("Place cut piece on the table", 100);
                break;

            case 5:
                objectiveManager.AddObjective("Equip shovel", 100);
                objectiveManager.AddObjective("Clean metal scraps", 100);
                break;

            case 6:
                objectiveManager.AddObjective("Un-equip shovel", 100);
                objectiveManager.AddObjective("Turn the lathe off", 100);
                break;

            default:
                Debug.Log("All stages completed!");
                break;
        }
    }

    void TransitionToEndScreen()
    {
        if (endScreenUI != null)
        {
            endScreenUI.SetActive(true);
        }

        // Find the player object and get the PlayerMovement component
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            // Disable player movement by disabling keyboard inputs
            playerController.ToggleMovement(false);
        }

        endScoreText.text = $"Your final Score: {objectiveManager.GetCurrentScore()}";

    }
}
