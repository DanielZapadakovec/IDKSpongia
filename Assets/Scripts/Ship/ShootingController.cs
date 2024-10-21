using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{

    [Header("Bullet")]
    public GameObject bulletPrefab; // Prefab pre strelu
    public Transform firePoint; // Bod, odkiaæ bude strela vystrelen·
    public float bulletSpeed = 20f; // R˝chlosù strely
    public float bulletLifetime = 2f; // »as, po ktorom sa strela zniËÌ

    public AudioClip shootSound;
    private AudioSource audioSource;
    public KeyCode ShootKey;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(ShootKey))
        {
            Shoot();
            if (shootSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
        }
    }

    void Shoot()
    {
        GameObject bullet1 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb1 = bullet1.GetComponent<Rigidbody>();
        if (rb1 != null)
        {
            rb1.velocity = firePoint.forward * bulletSpeed;
        }
        Destroy(bullet1, bulletLifetime);

        GameObject bullet2 = Instantiate(bulletPrefab, firePoint.position + firePoint.right * 1f, firePoint.rotation);
        Rigidbody rb2 = bullet2.GetComponent<Rigidbody>();
        if (rb2 != null)
        {
            rb2.velocity = firePoint.forward * bulletSpeed;
        }
        Destroy(bullet2, bulletLifetime);

    }


}

