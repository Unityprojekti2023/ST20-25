using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlpanelController : MonoBehaviour
{
    public GameObject BlackScreen;
    public GameObject AttentionScreen;

    public bool showBlackScreen = false;
    public bool showAttentionScreen = false;

    void Start()
    {
        BlackScreen.SetActive(false);
        AttentionScreen.SetActive(false);
    }

    void Update()
    {
        if(showBlackScreen)
        {
            BlackScreen.SetActive(true);
        } else {
            BlackScreen.SetActive(false);
        }

        if(showAttentionScreen)
        {
            AttentionScreen.SetActive(true);
        } else {
            AttentionScreen.SetActive(false);
        }
    }
}
