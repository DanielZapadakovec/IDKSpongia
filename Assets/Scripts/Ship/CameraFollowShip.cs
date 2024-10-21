using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowShip : MonoBehaviour
{

    public Transform target;            // Cie�, ktor�m je lo� (transform�cia lode)
    public float followSpeed = 5.0f;    // R�chlos� nasledovania
    public Vector3 offsetPosition;      // Ofset poz�cie kamery (od lode)
    public Vector3 offsetRotation;      // Ofset rot�cie kamery (vzh�adom na lo�)

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Cie� kamery nie je nastaven�.");
            return;
        }

        // Vypo��ta� cie�ov� poz�ciu kamery a rot�ciu kamery na z�klade lode a ofsetov
        Vector3 desiredPosition = target.position + target.TransformDirection(offsetPosition);
        Quaternion desiredRotation = target.rotation * Quaternion.Euler(offsetRotation);

        // Hladk� pohyb kamery
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Hladk� rot�cia kamery
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, followSpeed * Time.deltaTime);
    }

}
