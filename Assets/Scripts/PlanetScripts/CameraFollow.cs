using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Objekt, na ktor� sa kamera pozer� (hr��)
    public Transform planet; // Referencia na plan�tu
    public Vector3 offset = new Vector3(0, 5, -10); // Poz�cia kamery v relat�vnom offsete vo�i hr��ovi
    public float smoothSpeed = 5f; // R�chlos� plynul�ho pohybu kamery
    public bool isAttached = false; // Ur�uje, �i je kamera attachnut� na hr��a
    public float minDistanceFromPlanet = 2f; // Minim�lna vzdialenos� kamery od povrchu plan�ty
    public LayerMask collisionMask; // Vrstva kol�zi� (napr. plan�ta)

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
