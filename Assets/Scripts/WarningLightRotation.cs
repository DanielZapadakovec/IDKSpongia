using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLightRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;

    public void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
