using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Objekt, na ktorı sa kamera pozerá (hráè)
    public Transform planet; // Referencia na planétu
    public Vector3 offset = new Vector3(0, 5, -10); // Pozícia kamery v relatívnom offsete voèi hráèovi
    public float smoothSpeed = 5f; // Rıchlos plynulého pohybu kamery
    public bool isAttached = false; // Urèuje, èi je kamera attachnutá na hráèa
    public float minDistanceFromPlanet = 2f; // Minimálna vzdialenos kamery od povrchu planéty
    public LayerMask collisionMask; // Vrstva kolízií (napr. planéta)

    void LateUpdate()
    {
        if (target == null || planet == null) return;

        Vector3 gravityUp = (target.position - planet.position).normalized;

        Vector3 desiredPosition = target.position + offset;

        RaycastHit hit;
        if (Physics.Raycast(target.position, -gravityUp, out hit, Mathf.Infinity, collisionMask))
        {
            float distanceToSurface = Vector3.Distance(hit.point, target.position);
            if (distanceToSurface < minDistanceFromPlanet)
            {
                desiredPosition = hit.point + gravityUp * minDistanceFromPlanet;
            }
        }

        if (!isAttached)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = target.position + offset;
        }

        Vector3 cameraUp = transform.up;
        Quaternion targetRotation = Quaternion.FromToRotation(cameraUp, gravityUp) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);

        Quaternion lookAtPlayer = Quaternion.LookRotation(target.position - transform.position, gravityUp);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtPlayer, smoothSpeed * Time.deltaTime);
    }
}
