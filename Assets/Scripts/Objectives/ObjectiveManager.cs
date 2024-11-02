using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    public Text objectiveText; // Textové pole UI, kde sa zobrazí aktuálny cie¾
    private int currentObjectiveIndex = 0;

    [System.Serializable]
    public class Objective
    {
        public string description; // Popis cie¾a
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
            currentObjectiveIndex++;
            UpdateObjectiveText();
        }
        else
        {
            objectiveText.text = "All objectives completed!";
            Debug.Log("All objectives completed!");
        }
    }

    void UpdateObjectiveText()
    {
        objectiveText.text = "Objective: " + objectives[currentObjectiveIndex].description;
        Debug.Log("Current objective: " + objectives[currentObjectiveIndex].description);
    }
}