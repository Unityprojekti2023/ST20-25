using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitInteractable : MonoBehaviour, IInteractable
{
    public EscapeMenu escapeMenu;
    public GameObject endScreenUI;
    public TextMeshProUGUI endScoreText;

    public void Interact()
    {
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

