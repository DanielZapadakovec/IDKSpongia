using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class FuelTimeline : MonoBehaviour
{
    public PlayableDirector timeline;
    public GameObject FKey;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable") && Input.GetKeyDown(KeyCode.F) && other.gameObject.name.Equals("MachineGenerator"))
        {
            if (timeline != null)
            {
                timeline.Play();
                FKey.SetActive(false);
                other.gameObject.SetActive(false);
            }
        }
    }
}
