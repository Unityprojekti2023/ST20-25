using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpControlPanelManager : MonoBehaviour
{
    [Header("References to other objects")]
    public GameObject[] buttons;
    public Material highlightMaterial;
    private Material[] originalMaterials;


    int currentButtonIndex = 0;
    bool isHighlightActive = false;
    public int mistakeCounter;

    //TODO: How to make this work so that player can't keep pressing same button to increasing the index

    void Start()
    {
        originalMaterials = new Material[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            originalMaterials[i] = buttons[i].GetComponent<Renderer>().material;
        }
    }

    public void ToggleHelpHighlight(bool isHelpActive)
    {
        isHighlightActive = isHelpActive;
        if (isHighlightActive)
        {
            ResetButtons();
            buttons[currentButtonIndex].GetComponent<Renderer>().material = highlightMaterial;
        }
        else
        {
            mistakeCounter = 0;
            ResetButtons();
        }
    }

    public void NextHelpHighlight(string hitButton)
    {
        // Check if hit object button is current button index
        if (buttons[currentButtonIndex].name == hitButton)
        {
            // Check if current button index is not the last button in the array
            if (currentButtonIndex < buttons.Length - 1)
            {
                // Increase current button index
                currentButtonIndex++;

                // Check if highlight is active
                if (isHighlightActive)
                {
                    // Reset all buttons and highlight current button
                    ResetButtons();
                    buttons[currentButtonIndex].GetComponent<Renderer>().material = highlightMaterial;
                }
                else
                {
                    ResetButtons();
                }
            }
            else
            {
                currentButtonIndex = 0;
                ResetButtons();
            }
        }
        else
        {
            // Check if hit button is included in the buttons array
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].name == hitButton)
                {
                    CheckIfTooManyMistakes();
                }
            }
        }
    }

    private void CheckIfTooManyMistakes()
    {
        // Check if mistake counter is greater than or equal to 3 and highlight is not active
        // This can be changed to a different number of mistakes as needed
        mistakeCounter++;

        if (mistakeCounter >= 3 && !isHighlightActive)
        {
            ToggleHelpHighlight(true);
        }
    }

    private void ResetButtons()
    {
        // Reset all buttons
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Renderer>().material = originalMaterials[i];
        }
    }
}
