using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("References to other scripts")]
    public GameObject player;

    [Header("References to gameobjects")]
    public GameObject[] popUps;
    
    [Header("Other values")]
    private int popUpIndex;
    private Vector3 startPosition;
    private float minDistanceTraveled = 100.0f;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("PlayerController reference not set in TutorialManager!");
            return;
        }
        
        startPosition = player.transform.position;
    }
    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                if(popUps[i] != null) {
                    popUps[i].SetActive(true);
                }
            }
            else
            {
                if(popUps[i] != null) {
                    popUps[i].SetActive(false);
                }
            }
        }

        switch (popUpIndex)
        {
            case 0:
                if (Vector3.Distance(startPosition, player.transform.position) >= minDistanceTraveled)
                {
                    popUpIndex++;
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    popUpIndex++;
                }
                break;
                //Add anyother tutorial popups as needed
        }

        if(popUpIndex >= popUps.Length)
        {
            //Tutorial completed or run out handling
        }
    }
}
