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
    public Canvas practiceModeUI;
    public GameObject endScreenUI;
    public GameObject highlightSpot;


    [Header("Other values")]
    public int currentStage = 0; //TODO: remove this and change collision check to objectivemanagers stage

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
        if (PlayerPrefs.GetInt("HideUI") == 1)
        {
            practiceModeUI.gameObject.SetActive(false);
        }

        // Initialize with the objectives of the first stage
        taskManager.AssignRandomTask(clipboardPickup.clipboardImageSlot, clipboardPickup.clibBoardTextSlot);
        //TODO: How to initialize objects
    }

    void Update()
    {
        if(ObjectiveManager.Instance.GetCurrentStage() == 1 && !ObjectiveManager.Instance.CheckIfObjectiveIsCompleted("Pick up the clipboard"))
        {
            highlightSpot.SetActive(true);
        }
        else
        {
            highlightSpot.SetActive(false);
        }
    }
}
