using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlpanelController : MonoBehaviour
{
    public GameObject BlackScreen;
    public GameObject AttentionScreen;
    public GameObject HomeScreen1;
    public GameObject HomeScreen2;

    public bool showBlackScreen = false;
    public bool showAttentionScreen = false;
    public bool showHomeScreen1 = false;
    public bool showHomeScreen2 = false;

    void Start()
    {
        BlackScreen.SetActive(false);
        AttentionScreen.SetActive(false);
        HomeScreen1.SetActive(false);
        HomeScreen2.SetActive(false);
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
    }
}
