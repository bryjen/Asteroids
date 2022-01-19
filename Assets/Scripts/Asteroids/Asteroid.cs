using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/**
 * <remarks>
 * Large Range: 10 - 16
 * Medium Range: 5 - 8
 * Small Range: 2 - 5
 * </remarks>
 */
public class Asteroid : MonoBehaviour
{
    protected const float DEFAULT_SIZE = 13f;
    protected const float ASTEROID_LIFESPAN = 20f;
    
    private Rigidbody2D _rigidbody2D;
    private float size;
    
    //Asteroid Size Ranges
    private Range<float> largeAsteroidRange;
    private Range<float> mediumAsteroidRange;
    private Range<float> smallAsteroidRange;

    [Header("Asteroid Size Variants")] 
    [SerializeField] private GameObject largeAsteroid;
    [SerializeField] private GameObject mediumAsteroid;
    [SerializeField] private GameObject smallAsteroid;
    
    /** <summary>Sets an asteroid's direction and speed, with a random size</summary>
     */
    public void Initialize(Vector2 impulseForce)
    {
        SetAsteroidSizeData();
        Initialize(Random.Range(largeAsteroidRange.lowerBound, largeAsteroidRange.upperBound), impulseForce);
    }
    
    /** <summary>Sets the asteroid's size, direction and speed.</summary>
     * <remarks>It is recomended to set the size in between the range defined in the previous Initialize method</remarks> */
    public void Initialize(float? size, Vector2 impulseForce)
    {
        if (size is null)
            this. size = DEFAULT_SIZE;
        else
            this.size = size.Value;

        if (TryGetComponent<Rigidbody2D>(out _rigidbody2D))
            _rigidbody2D.AddForce(impulseForce, ForceMode2D.Impulse);
        else
            Debug.LogError("ASTEROID PREFAB DOES NOT HAVE A RigidBody2D COMPONENT");
    }

    public void AdjustSize()
    {   //TODO USE ONLY ON LARGE ASTEROIDS BUGGY ON SMALLER ONES
        var localScaleMultiplier = this.size / DEFAULT_SIZE;
        transform.localScale = transform.localScale * localScaleMultiplier;
    }
    
    protected virtual void SetAsteroidSizeData()
    {
        largeAsteroidRange = new Range<float>(10, 16);
        mediumAsteroidRange = new Range<float>(5, 9);
        smallAsteroidRange = new Range<float>(2, 5);
    }

    private String GetCurrentSizeRange()
    {
        if (size >= largeAsteroidRange.lowerBound)
            return "large";
        else if (size >= mediumAsteroidRange.lowerBound && size < mediumAsteroidRange.upperBound)
            //i.e. if lowerBound <= size < upperBound
            return "medium";
        else
            return "small";
    }
    
    private void Split(String currentSizeRange)
    {
        int asteroidCount;

        switch (currentSizeRange)
        {
            case "large" : 
            {
                if (size / 3 > mediumAsteroidRange.lowerBound)
                    asteroidCount = 3;
                else
                    asteroidCount = 2;
                
                SpawnAsteroids(mediumAsteroid, asteroidCount, new Range<float>(.5f, 0.7f));
                break;
            }
            case "medium":
            {
                
                if (size / 3 > smallAsteroidRange.lowerBound)
                    asteroidCount = 3;
                else
                    asteroidCount = 2;
                
                SpawnAsteroids(smallAsteroid, asteroidCount, new Range<float>(.5f, 0.7f));
                break;
            }
            default:    //small
                break;
        }
    }

    private void SpawnAsteroids(GameObject asteroidPrefab, int asteroidCount, Range<float> speedMultiplierRange)
    {
        for (int i = 0; i < asteroidCount; i++)
        {
            var newAsteroid = Instantiate(asteroidPrefab, transform.position, Quaternion.identity);
            
            if (!newAsteroid.TryGetComponent<Rigidbody2D>(out Rigidbody2D newAsteroidRigidbody2D))
                Debug.LogError($"Asteroid prefab \"{asteroidPrefab.name}\" does not contain a RigidBody2D component");
            newAsteroidRigidbody2D.angularVelocity = Random.Range(-10f, 10f);
            newAsteroid.transform.parent = transform.parent;
            
            if (!newAsteroid.TryGetComponent<Asteroid>(out Asteroid asteroidScript))
                Debug.LogError($"Asteroid prefab \"{asteroidPrefab.name}\" does not contain an Asteroid.cs component");
            
            asteroidScript.Initialize(size / 2, 
                Random.insideUnitCircle.normalized * _rigidbody2D.velocity * 
                Random.Range(speedMultiplierRange.lowerBound, speedMultiplierRange.upperBound));
        }
    }

    void Start()
    {
        Destroy(this.gameObject, ASTEROID_LIFESPAN);
        SetAsteroidSizeData();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer != 8)
            return;
        
        Split(GetCurrentSizeRange());

        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }
}
