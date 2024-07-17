using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnStartButton()
    {
        PlayerPrefs.SetInt("HideUI", 0);
        Debug.Log("Hide UI: " + PlayerPrefs.GetInt("HideUI"));

        SceneManager.LoadScene(1);
    }

    public void OnTestButton()
    {
        PlayerPrefs.SetInt("HideUI", 1);
        Debug.Log("Hide UI: " + PlayerPrefs.GetInt("HideUI"));

        SceneManager.LoadScene(1);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}

