using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectiveUI : MonoBehaviour
{
    public TextMeshProUGUI objectiveListText;
    public Color completedColor = Color.green;

    void Update()
    {
        UpdateObjectiveList();
    }

    void UpdateObjectiveList()
    {
        string objectiveText = "Objectives:\n";

        foreach (Objective obj in ObjectiveManager.Instance.objectives)
        {
            string status = obj.isCompleted ? " [Completed]" : "";

            if (obj.isCompleted)
            {
                // Apply green color to completed objectives
                objectiveText += $"<color=#{ColorUtility.ToHtmlStringRGB(completedColor)}>{obj.description}{status}</color>\n";
            }
            else
            {
                objectiveText += $"{obj.description}{status}\n";
            }
        }

        objectiveListText.text = objectiveText;
    }
}
