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
        // Ak stre�ba naraz� do in�ho objektu s kol�ziou
        if (other.CompareTag("Target"))
        {
            // Vytvorenie efektu �ast�c na poz�cii kol�zie
            Instantiate(hitParticlePrefab, other.transform.position, Quaternion.identity);
            Instantiate(hitParticlePrefabSmoke, other.transform.position, Quaternion.identity);
            Instantiate(energyPrefab, other.transform.position, Quaternion.identity);


            // Zni�enie objektu, do ktor�ho stre�ba narazila
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
