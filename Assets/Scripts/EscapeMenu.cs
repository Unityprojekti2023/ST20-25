using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour
{
    public GameObject escapeMenu;
    public GameObject optionsMenu;
    public static bool GameIsPaused = false;

    void Start()
    {
        escapeMenu.SetActive(false); // Hide menu initially
        optionsMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        escapeMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true; //Show mouse cursor
        Cursor.lockState = CursorLockMode.None;
        GameIsPaused = true;
    }

    public void Resume()
    {
        escapeMenu.SetActive(false);
        optionsMenu.SetActive(false);
        Time.timeScale = 1; // Resume game
        Cursor.visible = false; // Hide mouse cursor when menu is closed
        Cursor.lockState = CursorLockMode.Locked;
        GameIsPaused = false; // Update game pause state

        // Deselect the button
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void Options()
    {
        escapeMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
