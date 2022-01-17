using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary></summary>
 */
public class AsteroidSpawnController : MonoBehaviour
{
    [SerializeField, Min(0), Tooltip("Cooldown inbetween asteroid spawns")] private float spawnRate;
    [SerializeField] private int spawnAmount;

    private AsteroidSpawner asteroidSpawner;

    void Start()
    {
        if (!this.gameObject.TryGetComponent<AsteroidSpawner>(out asteroidSpawner))
            Debug.LogError("Both the AsteroidSpawnController and the AsteroidSpawner scripts must be on the same gameobject");

        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
            asteroidSpawner.SpawnDefaultAsteroid();
    }
}