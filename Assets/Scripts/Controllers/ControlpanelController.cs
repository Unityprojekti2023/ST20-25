using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlpanelController : MonoBehaviour
{
    [Header("References to control panel images")]
    public GameObject BlackScreen;
    public GameObject AttentionScreen;
    public GameObject HomeScreen1;
    public GameObject HomeScreen2;
    public GameObject ProgramScreen0;
    public GameObject ProgramScreen1;
    public GameObject ProgramScreen3; //No animation yet for this program, and no program screen image made yet
    public GameObject ProgramScreen6; //No animation yet for this program, and no program screen image made yet
    public GameObject ProgramScreen7; //No animation yet for this program, and no program screen image made yet

    [Header("Variables")]
    public bool showBlackScreen = false;
    public bool showAttentionScreen = false;
    public bool showHomeScreen1 = false;
    public bool showHomeScreen2 = false;
    public bool isProgramSelectionActive = false;

    [Header("References to other scripts")]
    public DrillController drillController;

    void Start()
    {
        BlackScreen.SetActive(false);               // Setting all screens inActive on startup
        AttentionScreen.SetActive(false);
        HomeScreen1.SetActive(false);
        HomeScreen2.SetActive(false);
        ProgramScreen0.SetActive(false);
        ProgramScreen1.SetActive(false);

        ProgramScreen3.SetActive(false);
        ProgramScreen6.SetActive(false);
        ProgramScreen7.SetActive(false);
    }

    public void UpdateScreenImage()                             // Function to update control panel image
    {
        if(showBlackScreen){
            BlackScreen.SetActive(true);
        } else {
            BlackScreen.SetActive(false);
        }

        if(showAttentionScreen){
            AttentionScreen.SetActive(true);
        } else {
            AttentionScreen.SetActive(false);
        }

        if(showHomeScreen1){
            HomeScreen1.SetActive(true);
        } else {
            HomeScreen1.SetActive(false);
        }

        if(showHomeScreen2){
            HomeScreen2.SetActive(true);
        } else {
            HomeScreen2.SetActive(false);
        }

        if (!isProgramSelectionActive)
        {
            ProgramScreen0.SetActive(false);
            ProgramScreen1.SetActive(false);
        }

        if (isProgramSelectionActive)                           // Making sure program selection is active
        {
            switch (drillController.selectedProgram)            // Switch case for each program
            {
                case 0:
                    ProgramScreen0.SetActive(true);             // Setting the wanted image as active, and others as inactive
                    ProgramScreen1.SetActive(false);

                    HomeScreen1.SetActive(false);
                    HomeScreen2.SetActive(false);
                    break;

                case 1:
                    ProgramScreen0.SetActive(false);
                    ProgramScreen1.SetActive(true);             // Setting the wanted image as active, and others as inactive

                    HomeScreen1.SetActive(false);
                    HomeScreen2.SetActive(false);
                    break;

                // Add new cases if new programs are added
            }
        }
    }
}