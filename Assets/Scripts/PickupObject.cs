using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public Transform holdPosition; // Poz�cia, kde bude objekt pri dr�an� (pripojen� na ruku hr��a)
    public KeyCode dropKey = KeyCode.E; // Tla�idlo na pustenie objektu
    private Transform originalParent; // P�vodn� rodi� objektu
    private bool isBeingHeld = false; // Kontrola, �i je objekt zdvihnut�
    public GameObject fKey;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void PickUp(Transform player)
    {
        // Nastav objekt ako die�a hr��a a presu� ho na poz�ciu r�k
        originalParent = transform.parent;
        transform.SetParent(player);

        // Presu� objekt do poz�cie holdPosition
        UpdateHeldObjectPosition();

        // Deaktivuj fyziku po�as dr�ania objektu
        rb.isKinematic = true;
        isBeingHeld = true;
        fKey.SetActive(true);
    }

    void Update()
    {
        if (isBeingHeld && Input.GetKeyDown(dropKey))
        {
            Drop();
        }
    }

    void LateUpdate()
    {
        if (isBeingHeld)
        {
            UpdateHeldObjectPosition();
        }
    }

    void UpdateHeldObjectPosition()
    {
        // Nastav� poz�ciu a rot�ciu objektu pod�a poz�cie a rot�cie holdPosition
        transform.position = holdPosition.position;
        transform.rotation = holdPosition.rotation;
    }

    void Drop()
    {
        fKey.SetActive(false);
        // Odstr�� objekt z poz�cie r�k hr��a
        transform.SetParent(originalParent);
        rb.isKinematic = false;
        isBeingHeld = false;
    }
}