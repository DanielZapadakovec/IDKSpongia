using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    [SerializeField] private List<Objective> objectives = new List<Objective>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ObjectiveManager>().ObjectiveStart(objectives);
        }
    }
}