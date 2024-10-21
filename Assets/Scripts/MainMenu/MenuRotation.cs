using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRotation : MonoBehaviour
{

    // PÙvodn· rot·cia objektu
    private Quaternion originalRotation;

    // Cieæov· rot·cia, na ktor˙ chceme objekt otoËiù
    public Quaternion targetRotationSettings = Quaternion.Euler(0, 180, 0);
    public Quaternion targetRotationLoadGame = Quaternion.Euler(0, 180, 0);

    // Premenn· na sledovanie stavu rot·cie
    private bool isRotated = false;

    void Start()
    {
        // Uloûenie pÙvodnej rot·cie pri ötarte
        originalRotation = transform.rotation;
    }


    public void ToggleObjectRotationSettings()
    {
        if (isRotated)
        {
            // Vr·tenie na pÙvodn˙ rot·ciu
            transform.rotation = originalRotation;
        }
        else
        {
            // Nastavenie na cieæov˙ rot·ciu
            transform.rotation = targetRotationSettings;
        }

        // Zmena stavu rot·cie
        isRotated = !isRotated;
    }
}

