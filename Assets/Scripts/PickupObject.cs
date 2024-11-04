using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public Transform holdPosition; // Pozícia, kde bude objekt pri draní (pripojená na ruku hráèa)
    public KeyCode dropKey = KeyCode.E; // Tlaèidlo na pustenie objektu
    private Transform originalParent; // Pôvodnı rodiè objektu
    private bool isBeingHeld = false; // Kontrola, èi je objekt zdvihnutı
    public GameObject fKey;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void PickUp(Transform player)
    {
        // Nastav objekt ako diea hráèa a presuò ho na pozíciu rúk
        originalParent = transform.parent;
        transform.SetParent(player);

        // Presuò objekt do pozície holdPosition
        UpdateHeldObjectPosition();

        // Deaktivuj fyziku poèas drania objektu
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
        // Nastaví pozíciu a rotáciu objektu pod¾a pozície a rotácie holdPosition
        transform.position = holdPosition.position;
        transform.rotation = holdPosition.rotation;
    }

    void Drop()
    {
        fKey.SetActive(false);
        // Odstráò objekt z pozície rúk hráèa
        transform.SetParent(originalParent);
        rb.isKinematic = false;
        isBeingHeld = false;
    }
}