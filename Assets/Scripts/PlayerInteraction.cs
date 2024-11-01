using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3f;
    Interactable currentInteractable;
    public Camera mainCamera;

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
        // Ray smerovaný z kamery dopredu
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

        // Skontroluj, èi lúè zasiahne objekt v rámci dosahu
        if (Physics.Raycast(ray, out RaycastHit hit, playerReach))
        {
            Debug.Log("Raycast hit: " + hit.collider.name); // Log názvu objektu, ktorý bol zasiahnutý

            if (hit.collider.CompareTag("Interactable"))
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();
                if (newInteractable == null)
                {
                    Debug.LogWarning("Interactable component not found on: " + hit.collider.name);
                    return;
                }

                if (currentInteractable && newInteractable != currentInteractable)
                {
                    currentInteractable.DisableOutline();
                }

                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
            }
            else
            {
                DisableCurrentInteractable();
            }
        }
        else
        {
            DisableCurrentInteractable();
        }
    }

    void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
        HudController.instance.EnableInteractionText(currentInteractable.message);
        Debug.Log("Set new interactable: " + newInteractable.name);
    }

    void DisableCurrentInteractable()
    {
        if (currentInteractable != null)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
            HudController.instance.DisableInteractionText();
        }
    }
}