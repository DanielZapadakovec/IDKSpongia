using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PropsTimeline : MonoBehaviour
{
    public PlayableDirector Satellitetimeline;
    public PlayableDirector Boxtimeline;
    public PlayableDirector Shiptimeline;

    public bool SatelliteTriggered;
    public bool BoxTriggered;
    public bool shipTriggered;
    public GameObject ShipFragment;
    public GameObject FKey;

    private void Start()
    {
        ShipFragment.SetActive(false);
        SatelliteTriggered = false;
        BoxTriggered = false;
        shipTriggered = false;

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable") && Input.GetKeyDown(KeyCode.F) && other.gameObject.name.Equals("SatelitteFragment"))
        {
            if (Satellitetimeline != null)
            {
                Satellitetimeline.Play();
                SatelliteTriggered = true;
                FKey.SetActive(false);
                other.gameObject.SetActive(false);
            }
        }
        else if (other.CompareTag("Interactable") && Input.GetKeyDown(KeyCode.F) && other.gameObject.name.Equals("CrateFragment"))
        {
            if (Boxtimeline != null)
            {
                Boxtimeline.Play();
                BoxTriggered = true;
                FKey.SetActive(false);
                other.gameObject.SetActive(false);
            }
        }
        else if (other.CompareTag("Interactable") && Input.GetKeyDown(KeyCode.F) && other.gameObject.name.Equals("ShipFragment"))
        {
            if (Shiptimeline != null)
            {
                Shiptimeline.Play();
                shipTriggered = true;
                FKey.SetActive(false);
                other.gameObject.SetActive(false);
            }
        }
    }
    public void Update()
    {
        if (SatelliteTriggered && BoxTriggered && !shipTriggered)
        {
            ShipFragment.SetActive(true);
        }
    }

    public void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
