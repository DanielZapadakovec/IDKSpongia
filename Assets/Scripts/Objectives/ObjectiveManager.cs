using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private GameObject objectiveParent;
    [SerializeField] private Text objectiveText;
    [SerializeField] private Text distanceText;

    [SerializeField] public float updateInterval = 0.5f;

    private List<Objective> objectiveList;
    private int currentObjectiveIndex = 0;

    [Header("Hráè")]
    [SerializeField] public Transform playerTransform;

    private void Start()
    {
        objectiveParent.SetActive(false);
    }

    public void ObjectiveStart(List<Objective> objectives)
    {
        objectiveParent.SetActive(true);
        objectiveList = objectives;
        currentObjectiveIndex = 0;

        StartCoroutine(UpdateObjective());
    }

    private IEnumerator UpdateObjective()
    {
        while (currentObjectiveIndex < objectiveList.Count)
        {
            Objective objective = objectiveList[currentObjectiveIndex];
            objectiveText.text = objective.description;
            objective.startObjectiveEvent?.Invoke();

            if (objective.isDistanceBased)
            {
                yield return StartCoroutine(CheckDistanceObjective(objective));
            }
            else if (objective.isActionBased)
            {
                yield return StartCoroutine(CheckActionObjective(objective));
            }

            objective.endObjectiveEvent?.Invoke();
            currentObjectiveIndex++;
        }

        ObjectiveStop();
    }

    public void SkipObjective(Objective objective)
    {
        objective.isCompleted = true;
    }

    private IEnumerator CheckDistanceObjective(Objective objective)
    {
        distanceText.enabled = true;
        while (!objective.isCompleted && Vector3.Distance(playerTransform.position, objective.location) > objective.completeDistance)
        {
            distanceText.text = "Location: " + Vector3.Distance(playerTransform.position, objective.location).ToString("F2") + "m";
            yield return new WaitForSeconds(updateInterval);
        }
        objective.isCompleted = true;
    }

    private IEnumerator CheckActionObjective(Objective objective)
    {
        distanceText.enabled = false;
        objective.completeActionEvent.AddListener(() => objective.isCompleted = true);
        yield return new WaitUntil(() => objective.isCompleted);
    }

    private void ObjectiveStop()
    {
        StopAllCoroutines();
        objectiveText.text = "";
        distanceText.text = "";
        objectiveParent.SetActive(false);

        Debug.Log("Úlohy dokonèené.");
    }
}