using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [Header("Clipboard image")]
    //Array of materials for clipboard image
    public Sprite[] clipboardImage;

    [Header("Material list")]
    private int currentMaterialIndex ;
    private string currentMaterialName;
    
    // Dictionary to map material names to material types
    private Dictionary<string, string> materialToMaterialType = new()
    {
        { "Teräs", "metal02_diffuse" },
        { "Alumiini", "metal06_diffuse" }
    };

    public void AssignRandomTask(Renderer clipboardImageSlot, TMPro.TextMeshProUGUI clibBoardTextSlot)
    {
        List<string> materialList = new(materialToMaterialType.Keys);
        foreach (var material in materialList)
        {
            Debug.Log(material);
        }
        // Get a random material from the list
        currentMaterialName = materialList[UnityEngine.Random.Range(0, materialList.Count)];

        // Get a random clipboard image
        currentMaterialIndex = UnityEngine.Random.Range(0, clipboardImage.Length);

        // Assign the material to the clipboard text slot
        clibBoardTextSlot.text = currentMaterialName;

        // Assign the image to the clipboard image slot
        clipboardImageSlot.material.mainTexture = clipboardImage[currentMaterialIndex].texture;
    }

    // Getter methods to access the current material index and material name
    public int GetCurrentMaterialIndex()
    {
        return currentMaterialIndex;
    }
    public string GetCurrentMaterialName()
    {
        return currentMaterialName;
    }

    public string GetMaterialType(string materialName)
    {
        return materialToMaterialType[materialName];
    }
}

