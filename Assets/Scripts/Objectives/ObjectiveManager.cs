using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    public Text objectiveText; // Textové pole UI, kde sa zobrazí aktuálny cie¾
    private int currentObjectiveIndex = 0;

    public bool waypointNeeded;
    private bool onetimeObjectiveCompleted;

    [System.Serializable]
    public class Objective
    {
        public string description; // Popis cie¾a
        
        public MissionWaypoint waypoint;
    }

    public List<Objective> objectives = new List<Objective>(); // Zoznam cie¾ov

    void Start()
    {
        if (objectives.Count > 0)
        {
            UpdateObjectiveText();
        }
        else
        {
            Debug.LogWarning("Objective list is empty!");
        }
    }

    public void CompleteObjective()
    {
        if (currentObjectiveIndex < objectives.Count - 1)
        {
            if (waypointNeeded) {
                objectives[currentObjectiveIndex+1].waypoint.enabled = true;
                objectives[currentObjectiveIndex].waypoint.DestroyWaypoint(); 

            }
            currentObjectiveIndex++;
            UpdateObjectiveText();
        }
    }

    void UpdateObjectiveText()
    {
        objectiveText.text = "Objective: " + objectives[currentObjectiveIndex].description;
        Debug.Log("Current objective: " + objectives[currentObjectiveIndex].description);
    }

    public void CompleteObjectiveOnce()
    {
        if (!onetimeObjectiveCompleted)
        {
            if (waypointNeeded)
            {
                objectives[currentObjectiveIndex + 1].waypoint.enabled = true;
                objectives[currentObjectiveIndex].waypoint.DestroyWaypoint();

            }
            currentObjectiveIndex++;
            UpdateObjectiveText();
            onetimeObjectiveCompleted = true;
        }

    }

}