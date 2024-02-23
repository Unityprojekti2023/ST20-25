using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlpanelController : MonoBehaviour
{
    public GameObject BlackScreen;
    public GameObject AttentionScreen;
    public GameObject HomeScreen1;
    public GameObject HomeScreen2;
    public GameObject ProgramScreen0;
    public GameObject ProgramScreen1;
    public GameObject ProgramScreen3; //No animation yet
    public GameObject ProgramScreen6; //No animation yet
    public GameObject ProgramScreen7; //No animation yet

    public bool showBlackScreen = false;
    public bool showAttentionScreen = false;
    public bool showHomeScreen1 = false;
    public bool showHomeScreen2 = false;

    public bool isProgramSelectionActive = false;

    public DrillController drillController;

    void Start()
    {
        BlackScreen.SetActive(false);
        AttentionScreen.SetActive(false);
        HomeScreen1.SetActive(false);
        HomeScreen2.SetActive(false);
        ProgramScreen0.SetActive(false);
        ProgramScreen1.SetActive(false);

        ProgramScreen3.SetActive(false);
        ProgramScreen6.SetActive(false);
        ProgramScreen7.SetActive(false);
    }

    public void updateScreenImage()
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

        if (isProgramSelectionActive)
        {
            switch (drillController.selectedProgram)
            {
                case 0:
                    ProgramScreen0.SetActive(true);
                    ProgramScreen1.SetActive(false);

                    HomeScreen1.SetActive(false);
                    HomeScreen2.SetActive(false);
                    break;

                case 1:
                    ProgramScreen0.SetActive(false);
                    ProgramScreen1.SetActive(true);

                    HomeScreen1.SetActive(false);
                    HomeScreen2.SetActive(false);
                    break;
            }
        }
    }
}