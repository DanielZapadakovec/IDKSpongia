using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRotation : MonoBehaviour
{

    // P�vodn� rot�cia objektu
    private Quaternion originalRotation;

    // Cie�ov� rot�cia, na ktor� chceme objekt oto�i�
    public Quaternion targetRotationSettings = Quaternion.Euler(0, 180, 0);
    public Quaternion targetRotationLoadGame = Quaternion.Euler(0, 180, 0);

    // Premenn� na sledovanie stavu rot�cie
    private bool isRotated = false;

    void Start()
    {
        // Ulo�enie p�vodnej rot�cie pri �tarte
        originalRotation = transform.rotation;
    }


    public void ToggleObjectRotationSettings()
    {
        if (isRotated)
        {
            // Vr�tenie na p�vodn� rot�ciu
            transform.rotation = originalRotation;
        }
        else
        {
            // Nastavenie na cie�ov� rot�ciu
            transform.rotation = targetRotationSettings;
        }

        // Zmena stavu rot�cie
        isRotated = !isRotated;
    }
}

