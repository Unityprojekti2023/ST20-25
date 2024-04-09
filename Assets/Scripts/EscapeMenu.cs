using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    [Header("References to other scripts")]
    public GameObject escapeMenu;
    public GameObject optionsMenu;
    public LatheSoundFX latheSoundFX;
    public DoorController doorController;
    public MouseControlPanelInteractable mouseControlPanelInteractable;

    [Header("References to audio sources")]
    public AudioSource beginningSource;
    public AudioSource endingSource;
    public AudioSource lathingSource;
    public AudioSource idlingSource;
    public AudioSource openingAudioSource;
    public AudioSource closingAudioSource;

    public static bool GameIsPaused = false;
    public bool isGamePaused = false;

    void Start()
    {
        escapeMenu.SetActive(false); // Hide menu initially
        optionsMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //TODO: Change to Escape once game is ready for builds, since Escape doens't work in the editors Game mode
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
        escapeMenu.SetActive(true);     // Show escape menu
        Time.timeScale = 0f;            // Pause game time
        Cursor.visible = true;          // Show mouse cursor
        Cursor.lockState = CursorLockMode.None;
        GameIsPaused = true;
        beginningSource.Pause();        // Pausing "Beginning" audio clip
        endingSource.Pause();           // Pausing "Endind" audio clip
        lathingSource.Pause();          // Pausing "Lathing" audio clip
        idlingSource.Pause();           // Pausing "Idling" audio clip
        openingAudioSource.Pause();     // Pausing door opening audio clip
        closingAudioSource.Pause();     // Pausing door closing audio clip
        isGamePaused = true;
    }

    public void Resume()
    {
        escapeMenu.SetActive(false);                // Hide escape menu
        optionsMenu.SetActive(false);               // Hide options menu
        Time.timeScale = 1;                         // Resume game
        Cursor.visible = false;                     // Hide mouse cursor when menu is closed
        Cursor.lockState = CursorLockMode.Locked;   // Locking the cursor
        GameIsPaused = false;                       // Update game pause state

        // Deselect the button
        EventSystem.current.SetSelectedGameObject(null);

        isGamePaused = false;

        // If "Beginning" audio clip was playing when game was paused, resuming the audio clip
        if(latheSoundFX.isBeginningClipPlaying) 
        {
            beginningSource.Play();
        }

        // If "Ending" audio clip was playing when game was paused, resuming the audio clip
        if(latheSoundFX.isEndingClipPlaying && latheSoundFX.endingClipPlayCounter == 0)
        {
            endingSource.Play();
        }

        // If "Lathing" audio clip was playing when game was paused, resuming the audio clip
        if(latheSoundFX.isLathingClipPlaying)
        {
            lathingSource.Play();
        }

        // If "Idling" audio clip was playing when game was paused, resuming the audio clip
        if(latheSoundFX.isIdlingClipPlaying)
        {
            idlingSource.Play();
        }

        // If door opening audio clip was playing when game was paused, resuming audio clip
        if (doorController.isDoorOpeningActive && !mouseControlPanelInteractable.isLathingActive) {
            openingAudioSource.Play();
        }
        
        // If door closing audio clip was playing when game was paused, resuming audio clip
        if (doorController.isDoorClosingActive && !mouseControlPanelInteractable.isLathingActive) {
            closingAudioSource.Play();
        }
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
        GameIsPaused = false;
        escapeMenu.SetActive(false);
        optionsMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void ReturnMainMenu()
    {
        escapeMenu.SetActive(false);
        optionsMenu.SetActive(false);
        GameIsPaused = false;
        SceneManager.LoadScene(0);
    }
}
