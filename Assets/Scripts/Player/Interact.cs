using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EvolveGames
{
    public class Interact : MonoBehaviour
    {
        [Header("Interact")]
        [SerializeField] LayerMask LayerMask = 8;
        [SerializeField] KeyCode ActionKey = KeyCode.E;
        [Header("Crosshair")]
        [SerializeField] bool CanCrosshair;
        [SerializeField] CrosshairNormal cross;

        Camera Camera;
        UnityEvent onInteract;
        bool OneSet;
        private void Start()
        {
            Camera = GetComponent<Camera>();
        }
        private void Update()
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, 2, LayerMask))
            {
                if (hit.collider.GetComponent<Interactable>() != false)
                {
                    onInteract = hit.collider.GetComponent<Interactable>().onInteract;
                    if (Input.GetKeyDown(ActionKey))
                    {
                        onInteract.Invoke();
                    }
                    if (CanCrosshair) { cross.StateActive(); OneSet = true; }
                }
                else if (CanCrosshair && OneSet) { cross.StateBase(); OneSet = false; }
            }
            else if (CanCrosshair && OneSet) { cross.StateBase(); OneSet = false; }

        }
    }

}