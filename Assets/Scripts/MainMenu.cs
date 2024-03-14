using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Toggle hideUIToggle;

    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnStartButton()
    {
        Debug.Log("Start Game!");

        // Set PlayerPrefs based on toggle state
        if (hideUIToggle != null)
        {
            int hideUIValue = hideUIToggle.isOn ? 1 : 0;
            PlayerPrefs.SetInt("HideUI", hideUIValue);
        }

        SceneManager.LoadScene(1);
    }

    public void OnQuitButton()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
