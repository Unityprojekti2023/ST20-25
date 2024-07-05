using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("References to other scripts")]
    public TaskManager taskManager;
    public ClipboardPickup clipboardPickup;



    [Header("References to gameobjects")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI objectiveList;
    public GameObject endScreenUI;


    [Header("Other values")]
    public int currentStage = 0;
    public bool canDisplayNotes = false;

    [Header("Clipboard image")]
    //Array of materials for clipboard image
    public Sprite[] clipboardImage;

    void Start()
    {
        //Hide end score UI
        if (endScreenUI != null)
        {
            endScreenUI.SetActive(false);
        }

        // Get the TaskManager component
        taskManager = GetComponent<TaskManager>();
        clipboardPickup = FindObjectOfType<ClipboardPickup>();


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
        }
        else
        {
            canDisplayNotes = true;
        }

        // Initialize with the objectives of the first stage
        taskManager.AssignRandomTask(clipboardPickup.clipboardImageSlot, clipboardPickup.clibBoardTextSlot);
        InitializeStageObjectives();
    }

    void Update()
    {
        // Check if all objectives are completed for the current stage
        if (ObjectiveManager.Instance.AreAllObjectivesCompleted())
        {
            // Increase the stage counter
            currentStage++;

            // Empty objectives and add objectives for the next stage
            ObjectiveManager.Instance.EmptyObjective();
            InitializeStageObjectives();
        }

        //Display current score
        scoreText.text = $"Score: {ObjectiveManager.Instance.GetCurrentScore()}";
    }

    void InitializeStageObjectives()
    {
        // Add objectives based on the current stage
        switch (currentStage)
        {
            case 0:
                ObjectiveManager.Instance.AddObjective("Put on safety shoes", 100);
                ObjectiveManager.Instance.AddObjective("Put on safety glasses", 100);
                break;

            case 1:
                ObjectiveManager.Instance.AddObjective("Pick up the clipboard", 50);
                ObjectiveManager.Instance.AddObjective("Inspect the drawing", 50);
                ObjectiveManager.Instance.AddObjective("Place the clipboard on the table", 50);
                break;

            case 2:
                ObjectiveManager.Instance.AddObjective("Pick up correct raw piece", 100);
                ObjectiveManager.Instance.AddObjective("Place piece in place", 100);
                break;

            case 3:
                ObjectiveManager.Instance.AddObjective("Turn on the lathe", 100);
                ObjectiveManager.Instance.AddObjective("Initialize the lathe", 100);
                break;

            case 4:
                ObjectiveManager.Instance.AddObjective("Select a program", 100);
                ObjectiveManager.Instance.AddObjective("Run a program", 100);
                break;

            case 5:
                ObjectiveManager.Instance.AddObjective("Pick up cut piece", 100);
                ObjectiveManager.Instance.AddObjective("Place cut piece on the table", 100);
                break;

            case 6:
                ObjectiveManager.Instance.AddObjective("Equip shovel", 100);
                ObjectiveManager.Instance.AddObjective("Clean metal scraps", 0);
                break;

            case 7:
                ObjectiveManager.Instance.AddObjective("Unequip shovel", 100);
                ObjectiveManager.Instance.AddObjective("Turn the lathe off", 100);
                break;

            case 8:
                ObjectiveManager.Instance.AddObjective("Equip caliper", 100);
                ObjectiveManager.Instance.AddObjective("Use caliper to measure the piece", 100);
                break;

            case 9:
                ObjectiveManager.Instance.AddObjective("Unequip caliper", 100);
                break;

            case 10:
                ObjectiveManager.Instance.AddObjective("Remove safety shoes", 100);
                ObjectiveManager.Instance.AddObjective("Remove safety glasses", 100);

                break;

            default:
                Debug.Log("All stages completed!");
                break;
        }
    }
}
