using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheController : MonoBehaviour
{
    public LatheTimelineController timelineController;
    public Transform attachmentPoint;
    public GameObject[] cutItemPrefabs;
    public Dictionary<string, GameObject> cutItems = new();

    void Start()
    {
        // Add cut item prefabs to dictionary
        foreach (GameObject cutItem in cutItemPrefabs)
        {
            cutItems.Add(cutItem.name, cutItem);
        }
    }

    // Play the timeline
    public void PlayTimeline()
    {
        timelineController.PlayTimeline();
    }

    // Instantiate a cut item by its sprite at the same spot as the original item
    public void SetSelectedProgram(int selectedProgramIndex)
    {
        if (selectedProgramIndex < 0 || selectedProgramIndex >= cutItemPrefabs.Length)
        {
            Debug.LogError("Selected program index is out of bounds.");
            return;
        }

        // Set current timeline
        timelineController.currentTimeline = selectedProgramIndex;
        GameObject selectedPrefab = cutItemPrefabs[selectedProgramIndex];
        // Show the selected cut item prefab
        selectedPrefab.SetActive(true);

        if (attachmentPoint.childCount > 0)
        {
            Material originalMaterial = attachmentPoint.GetChild(0).GetComponent<Renderer>()?.material;
            if (originalMaterial != null)
            {
                foreach (Transform child in selectedPrefab.transform)
                {
                    Renderer childRenderer = child.GetComponent<Renderer>();
                    if (childRenderer != null)
                    {
                        childRenderer.material = originalMaterial;
                    }
                }
            }

            // Instantiate the cut item and set it as a child of the attachment
            GameObject instantiatedCutItem = Instantiate(cutItems[selectedPrefab.name], attachmentPoint);
        
            // Set intantiated cut items position and rotation of the uncut item
            instantiatedCutItem.transform.localPosition = Vector3.zero;
            instantiatedCutItem.transform.rotation = cutItems[selectedPrefab.name].transform.rotation;

            // Destroy outelayer objects of instantiated cut item
            RemoveOuterLayers(instantiatedCutItem);

            // Destroy the uncut item
            Destroy(attachmentPoint.GetChild(0).gameObject);
            // Hide instantiated cut item
            instantiatedCutItem.SetActive(false);
        }
        else
        {
            Debug.LogError("Attachment point does not contain any item.");
        }
    }

    private void RemoveOuterLayers(GameObject item)
    {
        foreach (Transform child in item.transform)
        {
            if (child.name.Contains("Outer"))
            {
                Destroy(child.gameObject);
            }
        }
    }

    // Hide the shown cut item prefab
    public void HideCutItem()
    {
        cutItemPrefabs[timelineController.currentTimeline].SetActive(false);
    }
}
