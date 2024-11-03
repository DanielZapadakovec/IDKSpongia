using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FuelTimeline : MonoBehaviour
{
    public PlayableDirector timeline; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fuel"))
        {
            if (timeline != null)
            {
                timeline.Play();
            }
        }
    }
}
