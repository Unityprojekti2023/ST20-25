using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;

    [Header("Clipboard image")]
    //Array of materials for clipboard image
    public Sprite[] clipboardImage;

    private int currentMaterialIndex;
    private string currentMaterialName;
    // Dictionary to map material names to material types
    private readonly Dictionary<string, string> materialToMaterialType = new()
    {
        { "Steel", "metal02_diffuse" },
        { "Aluminum", "metal06_diffuse" }
    };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    //TODO: Is there better logic for this?

    public void AssignRandomTask(Renderer clipboardImageSlot, TextMeshProUGUI clibBoardTextSlot)
    {
        List<string> materialList = new(materialToMaterialType.Keys);

        // Get a random material from the list
        currentMaterialName = materialList[Random.Range(0, materialList.Count)];

        // Get a random clipboard image
        currentMaterialIndex = Random.Range(0, clipboardImage.Length);

        // Assign the material to the clipboard text slot
        clibBoardTextSlot.text = currentMaterialName;

        // Assign the image to the clipboard image slot
        clipboardImageSlot.material.mainTexture = clipboardImage[currentMaterialIndex].texture;
    }

    // Getter methods to access the current material index and material name
    public string GetCurrentMaterialName()
    {
        return currentMaterialName;
    }

    public string GetMaterialType(string materialName)
    {
        return materialToMaterialType[materialName];
    }
}

