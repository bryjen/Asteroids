using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary></summary>
 */
public class AsteroidSpawnController : MonoBehaviour
{
    
    #region SINGLETON METHODS

        private static AsteroidSpawnController _instance;
        public static AsteroidSpawnController Instance { get { return _instance;  } }
        
        private void Awake()	//or start
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
    
            _instance = this;
        }

    #endregion

    [SerializeField, Min(0), Tooltip("Cooldown inbetween asteroid spawns")] private float spawnRate;
    [SerializeField] private int spawnAmount;

    private AsteroidSpawner asteroidSpawner;

    public void StartSpawning()
        => InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    
    public void StopSpawning() 
        => CancelInvoke();

    private void Start()
    {
        if (!this.gameObject.TryGetComponent<AsteroidSpawner>(out asteroidSpawner))
            Debug.LogError("Both the AsteroidSpawnController and the AsteroidSpawner scripts must be on the same gameobject");
        
        //Spawn();
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
            asteroidSpawner.SpawnDefaultAsteroid();
    }
}
