using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private ObjectiveManager objectiveManager;

    void Start()
    {
        objectiveManager = GetComponent<ObjectiveManager>();

        // Add initial objectives
        objectiveManager.AddObjective("Put on safety shoes");
        objectiveManager.AddObjective("Put on safety glasses");
    }

    // TODO: Make this work on multiple steps of objectives, currently only 2 is possible maybe with a counter that increases after each AllObjectionCompleted
    void Update()
    {
        // Check if all objectives are completed
        if (objectiveManager.AreAllObjectivesCompleted())
        {
            // All objectives are completed, proceed to the next step
            Debug.Log("All objectives completed! Proceed to the next step.");
            objectiveManager.EmptyObjective();
            objectiveManager.AddObjective("Pick up uncut piece");
            objectiveManager.AddObjective("Place piece in place");
        }
    }
}
