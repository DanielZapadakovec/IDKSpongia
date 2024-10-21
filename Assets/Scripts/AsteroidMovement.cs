using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [Header("Movement")]
    public float rotationSpeed = 10f;
    public float moveSpeed = 5;

    private Vector3 randomRotation;
    private Vector3 movementDirection;

    void Start()
    {
        // Generovanie n�hodnej rot�cie pri spusten�
        randomRotation = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * rotationSpeed;

        // Nastavenie n�hodn�ho smeru pos�vania
        movementDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        transform.Rotate(randomRotation * Time.deltaTime);

        transform.Translate(movementDirection * moveSpeed * Time.deltaTime);

        if (Random.value < 0.01f) 
        {
            randomRotation = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * rotationSpeed;
        }

        if (Random.value < 0.01f)
        {
            movementDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
    }
}
