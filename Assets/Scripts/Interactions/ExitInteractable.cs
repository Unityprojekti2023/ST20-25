using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitInteractable : MonoBehaviour, IInteractable
{
    public string mainMenuSceneName = "MainMenu";
    public void Interact()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadSceneAsync(mainMenuSceneName);
    }
}

