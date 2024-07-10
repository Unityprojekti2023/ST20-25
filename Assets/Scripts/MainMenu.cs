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
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnStartButton()
    {
        Debug.Log("Start practice game!");
        PlayerPrefs.SetInt("HideUI", 0);

        Debug.Log("Hide UI: " + PlayerPrefs.GetInt("HideUI"));

        SceneManager.LoadScene(1);
    }

    public void OnTestButton()
    {
        Debug.Log("Start test game!");
        PlayerPrefs.SetInt("HideUI", 1);

        Debug.Log("Hide UI: " + PlayerPrefs.GetInt("HideUI"));


        SceneManager.LoadScene(1);
    }

    public void OnQuitButton()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}

