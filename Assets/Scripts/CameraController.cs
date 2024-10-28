using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
 [SerializeField] private Transform player;        // Referencia na hr��a
    [SerializeField] private LayerMask collisionLayer; // Vrstva, ktor� ur�uje kol�zne objekty
    [SerializeField] private float defaultDistance = 5f; // Predvolen� vzdialenos� kamery od hr��a
    [SerializeField] private float minDistance = 2f;     // Minim�lna vzdialenos� pri kol�zii
    [SerializeField] private float smoothSpeed = 10f;    // R�chlos� presunu kamery pri kol�zii

    private float currentDistance;

    private void Start()
    {
        // Nastavenie po�iato�nej vzdialenosti kamery
        currentDistance = defaultDistance;
    }

    private void LateUpdate()
    {
        // Vypo��ta cie�ov� poz�ciu kamery na z�klade aktu�lnej vzdialenosti
        Vector3 desiredPosition = player.position - transform.forward * currentDistance;

        // Raycast medzi hr��om a kamerou na detekciu kol�zi�
        RaycastHit hit;
        if (Physics.Raycast(player.position, -transform.forward, out hit, defaultDistance, collisionLayer))
        {
            // Ak sa zist� kol�zia, posunie kameru bli��ie k hr��ovi
            currentDistance = Mathf.Clamp(hit.distance, minDistance, defaultDistance);
        }
        else
        {
            // Ak nie je �iadna kol�zia, kamera sa vr�ti na p�vodn� vzdialenos�
            currentDistance = Mathf.Lerp(currentDistance, defaultDistance, smoothSpeed * Time.deltaTime);
        }

        // Nastav� poz�ciu kamery
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}

