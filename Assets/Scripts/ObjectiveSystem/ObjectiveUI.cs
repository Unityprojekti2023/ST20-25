using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ObjectiveUI : MonoBehaviour
{

    public TextMeshProUGUI objectiveListText;
    public Color completedColor = Color.green;

    private void Start()
    {
        UpdateObjectiveList();
    }

    public void UpdateObjectiveList()
    {
        string objectiveText = "Objectives:\n";
        List<Objective> currentObjectives = ObjectiveManager.Instance.GetCurrentStageObjectives();

        foreach (Objective obj in currentObjectives)
        {
            string status = obj.isCompleted ? " [Completed]" : "";

            if (obj.isCompleted)
            {
                objectiveText += $"<color=#{ColorUtility.ToHtmlStringRGB(completedColor)}>{obj.Description}{status}</color>\n";
            }
            else
            {
                objectiveText += $"{obj.Description}{status}\n";
            }
        }

        objectiveListText.text = objectiveText;
        /*
        string objectiveText = "Objectives:\n";

        foreach (Objective obj in objectives)
        {
            string status = obj.isCompleted ? " [Completed]" : "";

            if (obj.isCompleted)
            {
                // Apply green color to completed objectives
                objectiveText += $"<color=#{ColorUtility.ToHtmlStringRGB(completedColor)}>{obj.Description}{status}</color>\n";
            }
            else
            {
                objectiveText += $"{obj.Description}{status}\n";
            }
        }

        objectiveListText.text = objectiveText;*/
    }

}
