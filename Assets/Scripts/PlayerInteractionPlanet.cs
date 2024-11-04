using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionPlanet : MonoBehaviour
{
    public float playerReach = 3f; // Dosah hráèa pre interakciu
    private Interactable currentInteractable;
    public GameObject interactionUI; // UI objekt, napr. ikona "Press E"

    void Update()
    {
        CheckInteraction();
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            Debug.Log("Interacting with: " + currentInteractable.name);
            currentInteractable.Interact();
        }
    }

    void CheckInteraction()
    {
        // Vyh¾adanie všetkých objektov s tagom "Interactable" v scéne
        GameObject[] interactableObjects = GameObject.FindGameObjectsWithTag("Interactable");

        float closestDistance = playerReach;
        Interactable nearestInteractable = null;

        // Nájdeme najbližší objekt v dosahu
        foreach (var obj in interactableObjects)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);

            if (distance <= playerReach && distance < closestDistance)
            {
                closestDistance = distance;
                nearestInteractable = obj.GetComponent<Interactable>();
            }
        }

        // Ak je v dosahu nejaký nový interaktívny objekt
        if (nearestInteractable != null)
        {
            SetNewCurrentInteractable(nearestInteractable);
        }
        else
        {
            DisableCurrentInteractable();
        }
    }

    void SetNewCurrentInteractable(Interactable newInteractable)
    {
        // Nastavenie aktuálneho objektu pre interakciu
        if (currentInteractable != newInteractable)
        {
            if (currentInteractable != null)
                currentInteractable.DisableOutline(); // Skryjeme outline ak existoval

            currentInteractable = newInteractable;
            currentInteractable.EnableOutline(); // Ukážeme outline na novom objekte

            // Zobrazíme interakèné UI nad objektom
            interactionUI.SetActive(true);
        }
    }

    void DisableCurrentInteractable()
    {
        if (currentInteractable != null)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }

        // Skryjeme interakèné UI
        interactionUI.SetActive(false);
    }
}
