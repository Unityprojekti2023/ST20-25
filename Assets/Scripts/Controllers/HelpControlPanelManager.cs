using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HelpControlPanelManager : MonoBehaviour
{
    [Header("References to other objects")]
    public Material highlightMaterial;
    public GameObject[] buttons;
    private Material[] originalMaterials;
    public SpriteRenderer[] helpImages;

    [Header("Optional Steps")]
    public bool[] optionalSteps;
    int currentButtonIndex = 0; 
    int mistakeCounter;
    bool isHighlightActive = false;
    private bool firstPartCompleted = false;

    void Start()
    {
        originalMaterials = new Material[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            originalMaterials[i] = buttons[i].GetComponent<Renderer>().material;
        }

        HideAllHelpImages();

        // Initialize the optionalSteps array if it is not set or its length doesn't match buttons length
        if (optionalSteps == null || optionalSteps.Length != buttons.Length)
        {
            optionalSteps = new bool[buttons.Length];
        }
    }

    public void ToggleHelpHighlight(bool isHelpActive)
    {
        isHighlightActive = isHelpActive;
        if (isHighlightActive)
        {
            ResetButtons();
            HighlightCurrentButton();
        }
        else
        {
            mistakeCounter = 0;
            HideAllHelpImages();
            ResetButtons();
        }
    }

    public void NextHelpHighlight(string hitButton)
    {
        Debug.Log("NextHelpHighlight: " + hitButton);
        
        // If first part is not completed, only show buttons 0-8
        if (!firstPartCompleted)
        {
            // Check if hit object button is current button index
            if (buttons[currentButtonIndex].name == hitButton)
            {
                // Check if current button index is not the last button in the first part
                if (currentButtonIndex < 8)
                {
                    currentButtonIndex++;
                    if (isHighlightActive)
                    {
                        // Highlight the current button
                        HighlightCurrentButton();
                    }
                    else
                    {
                        ResetButtons();
                    }
                }
                else
                {
                    // Complete the first part
                    firstPartCompleted = true;
                    currentButtonIndex = 0;
                    HideAllHelpImages();
                    ResetButtons();
                }
            }
            else if (optionalSteps[currentButtonIndex] && buttons[currentButtonIndex + 1].name == hitButton)
            {
                //TODO: Is there possibility of bug here?
                currentButtonIndex += 2;
                if (isHighlightActive)
                {
                    // Highlight the current button
                    HighlightCurrentButton();
                }
                else
                {
                    ResetButtons();
                }
            }
            else
            {
                // Exclude the dial handle from generating mistakes
                if (hitButton != "dialHandle")
                {
                    CheckIfTooManyMistakes();
                }
            }
        }
        else
        {
            // If first part is completed, show buttons 9-10
            if (buttons[currentButtonIndex].name == hitButton)
            {
                if (currentButtonIndex < buttons.Length - 1)
                {
                    currentButtonIndex++;
                    if (isHighlightActive)
                    {
                        HighlightCurrentButton();
                    }
                    else
                    {
                        ResetButtons();
                    }
                }
                else
                {
                    currentButtonIndex = 0;
                    HideAllHelpImages();
                    ResetButtons();
                }
            }
            else
            {
                // Exclude the dial handle from generating mistakes
                if (hitButton != "dialHandle")
                {
                    CheckIfTooManyMistakes();
                }
            }
        }
    }

    // Method to start the second part of the help sequence
    public void StartSecondPart()
    {
        if (firstPartCompleted)
        {
            currentButtonIndex = 9; // Start at button 9
            if (isHighlightActive)
            {
                HighlightCurrentButton();
            }
        }
    }

    private void HideAllHelpImages()
    {
        // Set all help images to hidden
        for (int i = 0; i < helpImages.Length; i++)
        {
            if (helpImages[i] != null)
                helpImages[i].enabled = false;
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

    private void HighlightCurrentButton()
    {
        ResetButtons();
        buttons[currentButtonIndex].GetComponent<Renderer>().material = highlightMaterial;

        if (helpImages[currentButtonIndex] != null)
        {
            HideAllHelpImages();
            // Enable the help image for the current button if it exists
            helpImages[currentButtonIndex].enabled = true;
        }
        else
        {
            HideAllHelpImages();
        }
    }
}
