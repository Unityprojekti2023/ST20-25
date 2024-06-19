using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistakeGenerator : MonoBehaviour
{
    [Header("References to each part of the prefab")]
    public Transform[] parts;

    [Header("Minimun and maximum values for random length and thickness")]
    // The minimum and maximum values for the length and thickness of the parts
    public Vector2 minMaxLength = new(0.9f, 1.1f);
    public Vector2 minMaxThickness = new(0.9f, 1.1f); 
    void Start()
    {
        foreach (Transform part in parts)
        {
            Debug.Log($"Modifying part: {part.name}"); // TODO: remove this line
            ModifyPart(part);
        }        
    }

    void ModifyPart(Transform part)
    {
        // Randomize the length and thickness of the part
        float length = Random.Range(minMaxLength.x, minMaxLength.y);
        float thickness = Random.Range(minMaxThickness.x, minMaxThickness.y);
        
        Debug.Log($"Length: {length}, Thickness: {thickness}"); //TODO: remove this line
        // Modify the part's scale
        part.localScale = new Vector3(part.localScale.x * thickness, part.localScale.y * thickness, part.localScale.z * length);
    }
}
