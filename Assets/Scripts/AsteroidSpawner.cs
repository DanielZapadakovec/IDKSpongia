using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs; // Pole prefabov asteroidov, z ktor�ch sa bude spawnova�
    public int initialAsteroidCount = 10; // Po�iato�n� po�et asteroidov na vygenerovanie
    public float spawnRadius = 50f; // Polomer, v ktorom sa asteroidy spawnuj�

    private bool spawningEnabled = true; // Povolenie generovania asteroidov

    void Start()
    {
        // Vygenerovanie po�iato�n�ho mno�stva asteroidov pri �tarte hry
        GenerateInitialAsteroids();
    }

    void Update()
    {
        // Kontrola, �i je povolen� generovanie asteroidov
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

        // Zablokovanie �al�ieho generovania asteroidov
        spawningEnabled = false;
    }

    void SpawnAsteroid()
    {
        // N�hodn� v�ber prefabu asteroidu zo zoznamu asteroidPrefabs
        GameObject randomAsteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

        // N�hodn� poz�cia vo vesm�re na spawnovanie asteroidu
        Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius;

        // Spawnovanie asteroidu na vybranej poz�cii
        Instantiate(randomAsteroidPrefab, spawnPosition, Quaternion.identity);
    }
}
