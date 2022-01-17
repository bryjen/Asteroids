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
    
    public void SpawnDefaultAsteroid()
    {
        Vector3 spawnDirection = Random.insideUnitCircle.normalized * 15;
        Vector3 spawnpoint = spawnDirection + transform.position;

        var variance = Random.Range(-30, 30);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
        
        var asteroid = Instantiate(asteroidPrefab, spawnpoint, rotation);

        asteroid.GetComponent<Rigidbody2D>().AddForce((rotation * -spawnDirection).normalized * 2.5f, ForceMode2D.Impulse);
    }
}
