using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExitInteractable : MonoBehaviour, IInteractable
{
    public EscapeMenu escapeMenu;
    public GameObject endScreenUI;
    public TextMeshProUGUI endScoreText;

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

        escapeMenu.Pause();
    }
}

