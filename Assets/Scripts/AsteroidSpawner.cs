using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs; // Pole prefabov asteroidov, z ktorıch sa bude spawnova
    public int initialAsteroidCount = 10; // Poèiatoènı poèet asteroidov na vygenerovanie
    public float spawnRadius = 50f; // Polomer, v ktorom sa asteroidy spawnujú

    private bool spawningEnabled = true; // Povolenie generovania asteroidov

    void Start()
    {
        // Vygenerovanie poèiatoèného mnostva asteroidov pri štarte hry
        GenerateInitialAsteroids();
    }

    void Update()
    {
        // Kontrola, èi je povolené generovanie asteroidov
        if (spawningEnabled)
        {
            SpawnAsteroid();
        }
    }

    void GenerateInitialAsteroids()
    {
        for (int i = 0; i < initialAsteroidCount; i++)
        {
            SpawnAsteroid();
        }

        // Zablokovanie ïalšieho generovania asteroidov
        spawningEnabled = false;
    }

    void SpawnAsteroid()
    {
        // Náhodnı vıber prefabu asteroidu zo zoznamu asteroidPrefabs
        GameObject randomAsteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

        // Náhodná pozícia vo vesmíre na spawnovanie asteroidu
        Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius;

        // Spawnovanie asteroidu na vybranej pozícii
        Instantiate(randomAsteroidPrefab, spawnPosition, Quaternion.identity);
    }
}
