using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
 [SerializeField] private Transform player;        // Referencia na hráèa
    [SerializeField] private LayerMask collisionLayer; // Vrstva, ktorá urèuje kolízne objekty
    [SerializeField] private float defaultDistance = 5f; // Predvolená vzdialenos kamery od hráèa
    [SerializeField] private float minDistance = 2f;     // Minimálna vzdialenos pri kolízii
    [SerializeField] private float smoothSpeed = 10f;    // Rıchlos presunu kamery pri kolízii

    private float currentDistance;

    private void Start()
    {
        // Nastavenie poèiatoènej vzdialenosti kamery
        currentDistance = defaultDistance;
    }

    private void LateUpdate()
    {
        // Vypoèíta cie¾ovú pozíciu kamery na základe aktuálnej vzdialenosti
        Vector3 desiredPosition = player.position - transform.forward * currentDistance;

        // Raycast medzi hráèom a kamerou na detekciu kolízií
        RaycastHit hit;
        if (Physics.Raycast(player.position, -transform.forward, out hit, defaultDistance, collisionLayer))
        {
            // Ak sa zistí kolízia, posunie kameru blišie k hráèovi
            currentDistance = Mathf.Clamp(hit.distance, minDistance, defaultDistance);
        }
        else
        {
            // Ak nie je iadna kolízia, kamera sa vráti na pôvodnú vzdialenos
            currentDistance = Mathf.Lerp(currentDistance, defaultDistance, smoothSpeed * Time.deltaTime);
        }

        // Nastaví pozíciu kamery
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}

