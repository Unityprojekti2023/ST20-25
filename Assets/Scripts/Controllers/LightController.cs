using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [Header("References to objects")]
    public GameObject[] lights; // Array of lights
    
    void Start()
    {
        // Hide all but the first light
        for (int i = 1; i < lights.Length; i++)
        {
            lights[i].SetActive(false);
        }
    }

    public void TurnOnLight(int lightIndex)
    {
        // Turn off all lights expect the one at the given index
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(i == lightIndex);
        }
    }
}
