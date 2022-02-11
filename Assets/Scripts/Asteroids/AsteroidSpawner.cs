using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

/**
 * <summary>Contains methods that spawns asteroids with the given parameters. The behavior of spawning is controlled
 * by <see cref="AsteroidSpawnController"/></summary>
 */
public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    private GameObject placeholderGameObject;

    private void Start()
    {
        placeholderGameObject = GameObject.Find("---- ASTEROID ----");
    }

    public void SpawnDefaultAsteroid()
    {
        Vector3 spawnRadius = Random.insideUnitCircle.normalized * 15;
        Vector3 spawnpoint = spawnRadius + transform.position;
        Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0f, 180f));

        //spawns an asteroid and puts it in a placeholder gameobject
        var asteroid = Instantiate(asteroidPrefab, spawnpoint, rotation);
        asteroid.transform.parent = placeholderGameObject.transform;

        if (!asteroid.TryGetComponent<Asteroid>(out Asteroid asteroidScript))
            Debug.LogError("ASTEROID PREFAB DOES NOT HAVE Asteroid.cs SCRIPT!");  
        
        //initialize the asteroid's size, direction and velocity
        var variance = Random.Range(-15, 15);
        Quaternion directionRotation = Quaternion.AngleAxis(variance, Vector3.forward);
        
        asteroidScript.Initialize( (directionRotation * -spawnRadius).normalized * Random.Range(1.5f, 2f));
    }
}
