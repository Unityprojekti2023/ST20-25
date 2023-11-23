using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;
    private Vector3 startPosition;
    private float minDistanceTraveled = 100.0f;
    public PlayerController playerController;

    void Start()
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerController reference not set in TutorialManager!");
            return;
        }
        
        startPosition = playerController.transform.position;
    }
    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);

            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        switch (popUpIndex)
        {
            case 0:
                if (Vector3.Distance(startPosition, playerController.transform.position) >= minDistanceTraveled)
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
