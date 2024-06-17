using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Import the SceneManagement namespace

public class GameController : MonoBehaviour
{
    [Header("References to other scripts")]
    private ObjectiveManager objectiveManager;
    public EscapeMenu escapeMenu;

    [Header("References to gameobjects")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endScoreText;
    public TextMeshProUGUI objectiveList;
    public GameObject endScreenUI;
    public GameObject gameUI;


    [Header("Other values")]
    public int currentStage = 0;
    public int numberofStages = 9;
    public bool canDisplayNotes = false;

    void Start()
    {
        //Hide end score UI
        if (endScreenUI != null)
        {
            endScreenUI.SetActive(false);
        }

        objectiveManager = GetComponent<ObjectiveManager>();


        //Check if playing on Test mode to hide objective list and score.
        if (PlayerPrefs.GetInt("HideUI", 0) == 1)
        {
            canDisplayNotes = false;

            // Hide UI element if it exists
            if (objectiveList != null)
            {
                objectiveList.gameObject.SetActive(false);
            } 
            if (scoreText != null)
            {
                scoreText.gameObject.SetActive(false);
            }
        } else {
            canDisplayNotes = true;
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
                objectiveManager.AddObjective("Pick up the clipboard", 100);
                objectiveManager.AddObjective("Inspect the drawing", 100);
                break;

            case 2:
                objectiveManager.AddObjective("Pick up uncut piece", 100);
                objectiveManager.AddObjective("Place piece in place", 100);
                break;

            case 3:
                objectiveManager.AddObjective("Turn on the lathe", 100);
                objectiveManager.AddObjective("Initialize the lathe", 100);
                break;

            case 4:
                objectiveManager.AddObjective("Select a program", 100);
                objectiveManager.AddObjective("Run a program", 100);
                break;

            case 5:
                objectiveManager.AddObjective("Pick up cut piece", 100);
                objectiveManager.AddObjective("Place cut piece on the table", 100);
                break;

            case 6:
                objectiveManager.AddObjective("Equip shovel", 100);
                objectiveManager.AddObjective("Clean metal scraps", 100);
                break;

            case 7:
                objectiveManager.AddObjective("Unequip shovel", 100);
                objectiveManager.AddObjective("Turn the lathe off", 100);
                break;

            case 8:
                objectiveManager.AddObjective("Equip caliper", 100);
                break;

            case 9:
                objectiveManager.AddObjective("Unequip caliper", 100);
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

        escapeMenu.Pause();
    }
}
