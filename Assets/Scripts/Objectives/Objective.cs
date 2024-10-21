using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Objective : MonoBehaviour
{
    public string description;
    public Vector3 location;
    public float completeDistance = 5f;
    public bool isCompleted;

    [Header("Action Based")]
    public bool isDistanceBased;
    public bool isActionBased;

    [Header("Started Events")]
    public UnityEvent startObjectiveEvent;
    public UnityEvent endObjectiveEvent;
    public UnityEvent completeActionEvent; // Udalosù na dokonËenie ˙lohy

    public void completeButtonAction()
    {
        isCompleted = true;
    }
}