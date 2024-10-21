using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowShip : MonoBehaviour
{

    public Transform target;            // Cieæ, ktor˝m je loÔ (transform·cia lode)
    public float followSpeed = 5.0f;    // R˝chlosù nasledovania
    public Vector3 offsetPosition;      // Ofset pozÌcie kamery (od lode)
    public Vector3 offsetRotation;      // Ofset rot·cie kamery (vzhæadom na loÔ)

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Cieæ kamery nie je nastaven˝.");
            return;
        }

        // VypoËÌtaù cieæov˙ pozÌciu kamery a rot·ciu kamery na z·klade lode a ofsetov
        Vector3 desiredPosition = target.position + target.TransformDirection(offsetPosition);
        Quaternion desiredRotation = target.rotation * Quaternion.Euler(offsetRotation);

        // Hladk˝ pohyb kamery
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Hladk· rot·cia kamery
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, followSpeed * Time.deltaTime);
    }

}
