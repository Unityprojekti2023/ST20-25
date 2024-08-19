using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    [Header("References to other scripts")]
    public GameObject escapeMenu;
    public GameObject optionsMenu;

    public static bool gameIsPaused = false;
    public bool isGamePaused = false;

    void Start()
    {
        escapeMenu.SetActive(false); // Hide menu initially
        optionsMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CameraController.Instance.IsCameraActive(0)) // Check if main camera is active, if yes then show escape menu
            {
                if (!gameIsPaused)
                {
                    Pause();
                }
                else
                {
                    Resume();
                }
                return; // Return to avoid further execution of code
            }
            
            CameraController.Instance.SwitchToCamera(0); // Activate main camera if in another camera
        }
    }

    public void Pause()
    {
        escapeMenu.SetActive(true);     // Show escape menu
        Time.timeScale = 0f;            // Pause game time
        AudioListener.pause = true;     // Pause audio listener
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameIsPaused = true;    //TODO: Figure this code out
        isGamePaused = true;
    }

    public void Resume()
    {
        escapeMenu.SetActive(false);                // Hide escape menu
        optionsMenu.SetActive(false);               // Hide options menu
        Time.timeScale = 1;                         // Resume game
        AudioListener.pause = false;                // Resume audio listener
        Cursor.lockState = CursorLockMode.Locked;   // Locking the cursor
        gameIsPaused = false;                       // Update game pause state

        // Deselect the button
        EventSystem.current.SetSelectedGameObject(null);

        isGamePaused = false;
    }

    public void Options()
    {
        escapeMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Back()
    {
        escapeMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        gameIsPaused = false;
        escapeMenu.SetActive(false);
        optionsMenu.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void ReturnMainMenu()
    {
        escapeMenu.SetActive(false);
        optionsMenu.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
