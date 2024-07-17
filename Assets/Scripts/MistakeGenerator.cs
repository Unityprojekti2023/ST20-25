using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistakeGenerator : MonoBehaviour
//TODO: Work on this so that not all parts are modified each time, and see if need to change modification logic
{
    [Header("Minimun and maximum values for random length and thickness")]
    // The minimum and maximum values for the length and thickness of the parts
    public Vector2 minMaxLength = new(0.9f, 1.1f);
    public Vector2 minMaxThickness = new(0.9f, 1.1f); 
    public void GenerateMistakes(Transform[] parts)
    {
        // Loop through all the parts
        foreach (Transform part in parts)
        {
            // Randomize the length and thickness of the part
            Debug.Log("Modifying part: " + part.name); //TODO: remove this line
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
