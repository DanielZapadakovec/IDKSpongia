using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Particles")]
    public ParticleSystem hitParticlePrefab;
    public ParticleSystem hitParticlePrefabSmoke;

    [Header("Energy")]
    public GameObject energyPrefab;
    void OnTriggerEnter(Collider other)
    {
        // Ak stre¾ba narazí do iného objektu s kolíziou
        if (other.CompareTag("Target"))
        {
            // Vytvorenie efektu èastíc na pozícii kolízie
            Instantiate(hitParticlePrefab, other.transform.position, Quaternion.identity);
            Instantiate(hitParticlePrefabSmoke, other.transform.position, Quaternion.identity);
            Instantiate(energyPrefab, other.transform.position, Quaternion.identity);


            // Znièenie objektu, do ktorého stre¾ba narazila
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
