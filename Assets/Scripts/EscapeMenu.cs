using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour
{
    [Header("References to other scripts")]
    public GameObject escapeMenu;
    public GameObject optionsMenu;
    public LatheSoundFX latheSoundFX;
    public DoorController doorController;
    public MouseControlPanelInteractable mouseControlPanelInteractable;

    [Header("References to audio sources")]
    public AudioSource latheAudioSource;
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
        if (Input.GetKeyDown(KeyCode.Q)) //TODO: Change to Escape once game is ready for builds, since Escape doens't work in the editors Game mode
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
        escapeMenu.SetActive(true);     //Show escape menu
        Time.timeScale = 0f;            //Pause game time
        Cursor.visible = true;          //Show mouse cursor
        Cursor.lockState = CursorLockMode.None;
        GameIsPaused = true;
        latheAudioSource.Pause();       //Pausing lathe audio clip
        openingAudioSource.Pause();     //Pausing door opening audio clip
        closingAudioSource.Pause();     //Pausing door closing audio clip
        isGamePaused = true;
    }

    public void Resume()
    {
        escapeMenu.SetActive(false);    //Hide escape menu
        optionsMenu.SetActive(false);   //Hide options menu
        Time.timeScale = 1;             // Resume game
        Cursor.visible = false;         // Hide mouse cursor when menu is closed
        Cursor.lockState = CursorLockMode.Locked;
        GameIsPaused = false;           // Update game pause state

        // Deselect the button
        EventSystem.current.SetSelectedGameObject(null);

        isGamePaused = false;

        if (latheSoundFX.isLatheCuttingClipAlreadyPlaying) {
            latheAudioSource.Play();                            // If lathing audio clip was playing when game was paused, resuming audio clip
        }

        if (doorController.isDoorOpeningActive && !mouseControlPanelInteractable.isLathingActive) {
            openingAudioSource.Play();                          // If door opening audio clip was playing when game was paused, resuming audio clip
        }
        
        if (doorController.isDoorClosingActive && !mouseControlPanelInteractable.isLathingActive) {
            closingAudioSource.Play();                          // If door closing audio clip was playing when game was paused, resuming audio clip
        }
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
