using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheController : MonoBehaviour
{
    [Header("References")]
    public LatheTimelineController timelineController;
    public MistakeGenerator mistakeGenerator;
    public CleaningController cleaningController;
    public TextInformation textInformation; //TODO This and one in controlpanel interactable should reworked to be in one place?

    [Header("Lathe settings")]
    public Transform attachmentPoint;
    public GameObject[] cutItemPrefabs;
    public Dictionary<string, GameObject> cutItems = new();

    private Material currentMaterial;

    //TODO: What to do if door is opened while lathe is running?

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
        if (attachmentPoint.childCount > 0)
        {
            GameObject selectedPrefab = cutItemPrefabs[timelineController.currentTimeline];

            // Generate mistakes on the cut item
            // 50% chance to generate mistake
            if (Random.Range(0, 2) == 0)
            {
                Debug.Log("Generating mistakes");
                GenerateMistake(selectedPrefab);
            }

            // Instantiate the cut item and set it as a child of the attachment
            GameObject instantiatedCutItem = Instantiate(cutItems[selectedPrefab.name], attachmentPoint);

            // Set intantiated cut items position and rotation of the uncut item
            instantiatedCutItem.transform.localPosition = Vector3.zero;
            instantiatedCutItem.transform.rotation = cutItems[selectedPrefab.name].transform.rotation;

            // Destroy outelayer objects of instantiated cut item
            RemoveOuterLayers(instantiatedCutItem);
            // Hide instantiated cut item
            instantiatedCutItem.SetActive(false);
            timelineController.PlayTimeline();
            ShowScrapPile();
        }
        else
        {
            textInformation.UpdateText("There is no item to cut.");
        }
    }

    // Instantiate a cut item by its sprite at the same spot as the original item

    //TODO: Would it be smart to move this partly to PlayTimeLine method? So if there is no uncut item when program is selected it would do that logic when play is pressed
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

        if (attachmentPoint.childCount > 0)
        {
            // Show the selected cut item prefab
            selectedPrefab.SetActive(true);

            // Set material of the cut item to match the material of the uncut item
            ChangeMaterial(selectedPrefab);

            // Destroy the uncut item
            Destroy(attachmentPoint.GetChild(0).gameObject);
        }
        else
        {
            textInformation.UpdateText("There is no item in lathe");
        }
    }

    public void ShowScrapPile()
    {
        cleaningController.ShowAllScrapPiles(currentMaterial);
    }

    private void GenerateMistake(GameObject item)
    {

        Transform[] allParts = item.transform.GetComponentsInChildren<Transform>();
        List<Transform> parts = new();
        foreach (Transform part in allParts)
        {
            if (part.name.Contains("Cylinder"))
            {
                parts.Add(part);
            }
        }

        mistakeGenerator.GenerateMistakes(parts.ToArray());
    }

    private void ChangeMaterial(GameObject gameObject)
    {
        currentMaterial = attachmentPoint.GetChild(0).GetComponent<Renderer>()?.material;
        if (currentMaterial != null)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.TryGetComponent<Renderer>(out var childRenderer))
                {
                    childRenderer.material = currentMaterial;
                }
            }
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
