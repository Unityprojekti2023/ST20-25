using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnStartButton ()
    {
        Debug.Log("Start Game!");
        SceneManager.LoadScene(1);
    }

    public void OnQuitButton ()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}

