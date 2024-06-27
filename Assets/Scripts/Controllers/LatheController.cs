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
    public void SetSelectedProgram(string sprite, int selectedProgramIndex)
    {
        // Set current timeline
        timelineController.currentTimeline = selectedProgramIndex;

        // Instantiate cut item
        //GameObject cutItem = Instantiate(cutItems[sprite], attachmentPoint.position, Quaternion.identity);
        //cutItem.transform.parent = attachmentPoint;
        // Show the cut item of the selected program
        cutItemPrefabs[selectedProgramIndex].SetActive(true);

        // Set material to all cut items children from the uncut item
        foreach (Transform child in  cutItemPrefabs[selectedProgramIndex].transform)
        {
            child.GetComponent<Renderer>().material = attachmentPoint.GetChild(0).GetComponent<Renderer>().material;
        }

        // Destroy the uncut item
        Destroy(attachmentPoint.GetChild(0).gameObject);
        // Show the cut item
        attachmentPoint.GetChild(0).gameObject.SetActive(true);
    }
}
