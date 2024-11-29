using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ExitInteractable : MonoBehaviour, IInteractable
{
    public EscapeMenu escapeMenu;
    public GameObject endScreenUI;
    public GameObject objectivesScrollView;
    public TextMeshProUGUI endScoreText;
    public GameObject objectivePrefab;

    public ActionBasedController leftHandController;
    //TODO: Fix this
    private void Update()
    {
        if (leftHandController != null && leftHandController.selectAction.action.triggered)
        {
            Debug.Log("Quit.");
            Application.Quit();

#if UNITY_EDITOR
            // Stop play mode in the Unity Editor
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
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

