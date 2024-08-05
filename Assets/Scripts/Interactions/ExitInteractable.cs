using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ExitInteractable : MonoBehaviour, IInteractable
{
    public EscapeMenu escapeMenu;
    public GameObject endScreenUI;
    public GameObject objectivesScrollView;
    public TextMeshProUGUI endScoreText;
    public GameObject objectivePrefab;

    //TODO: Fix this

    public void Interact()
    {
        ObjectiveManager.Instance.CompleteObjective("End assigment by exiting throught the corridor door");
        TransitionToEndScreen();
    }

    void TransitionToEndScreen()
    {
        if (endScreenUI != null)
        {
            endScreenUI.SetActive(true);
        }

        endScoreText.text = $"Your final Score: {ObjectiveManager.Instance.GetCurrentScore()}";

        // Populate scroll view with objectives
        foreach (Objective obj in ObjectiveManager.Instance.GetAllObjectivesCompleted())
        {
            GameObject objective = Instantiate(objectivePrefab);
            objective.transform.SetParent(objectivesScrollView.transform);   
            objective.GetComponentInChildren<TextMeshProUGUI>().text = obj.Description;
        }

        escapeMenu.Pause();
    }
}

