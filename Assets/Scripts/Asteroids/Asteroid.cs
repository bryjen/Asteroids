using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    protected const float DEFAULT_SIZE = 13f;
    protected const float ASTEROID_LIFESPAN = 20f;
    
    private Rigidbody2D _rigidbody2D;
    private float size;
    
    private Range<float> largeAsteroidRange;
    private Range<float> mediumAsteroidRange;
    private Range<float> smallAsteroidRange;
    
    private GameObject largeAsteroid;
    private GameObject mediumAsteroid;
    private GameObject smallAsteroid;

    private float largeAsteroidBaseSize;
    private float mediumAsteroidBaseSize;
    private float smallAsteroidBaseSize;

    private GameObject explosionPrefab;
    

    [SerializeField] private AsteroidData asteroidData;

    /** <summary>Takes Data from the AsteroidData.cs script attached to a data placeholder</summary>
     */
    private void SetAsteroidData()
    {
        largeAsteroidRange = asteroidData.GetLargeRange();
        mediumAsteroidRange = asteroidData.GetMediumRange();
        smallAsteroidRange = asteroidData.GetSmallRange();

        largeAsteroid = asteroidData.largeAsteroid;
        mediumAsteroid = asteroidData.mediumAsteroid;
        smallAsteroid = asteroidData.smallAsteroid;

        largeAsteroidBaseSize = asteroidData.largeAsteroidBaseSize;
        mediumAsteroidBaseSize = asteroidData.mediumAsteroidBaseSize;
        smallAsteroidBaseSize = asteroidData.smallAsteroidBaseSize;

        explosionPrefab = asteroidData.explosionPrefab;
    }
    
    /** <summary>Sets an asteroid's direction and speed, with a random size</summary>
     */
    public void Initialize(Vector2 impulseForce)
    {
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

        _rigidbody2D.angularVelocity = Random.Range(-50, 50);

        AdjustSize();
    }

    public void AdjustSize()
    {
        float localScaleMultiplier;
        
        switch (GetCurrentSizeRange())
        {
            case "large" :
                localScaleMultiplier = (size / largeAsteroidBaseSize);
                break;
            case "medium":
                localScaleMultiplier = size / mediumAsteroidBaseSize;
                break;
            default:    //small
                localScaleMultiplier = size / smallAsteroidBaseSize;
                break;
        }
        
        transform.localScale *= localScaleMultiplier;
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
                
                size = smallAsteroidRange.upperBound;
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
            var newAsteroid = Instantiate(asteroidPrefab,
                transform.position,
                Quaternion.identity);
            
            if (!newAsteroid.TryGetComponent<Rigidbody2D>(out Rigidbody2D newAsteroidRigidbody2D))
                Debug.LogError($"Asteroid prefab \"{asteroidPrefab.name}\" does not contain a RigidBody2D component");
            newAsteroid.transform.parent = transform.parent;
            
            if (!newAsteroid.TryGetComponent<Asteroid>(out Asteroid asteroidScript))
                Debug.LogError($"Asteroid prefab \"{asteroidPrefab.name}\" does not contain an Asteroid.cs component");
            
            asteroidScript.Initialize((size / 2) + Random.Range(-1f, 2f), 
                Random.insideUnitCircle.normalized * 
                Random.Range(speedMultiplierRange.lowerBound, speedMultiplierRange.upperBound));
        }
    }

    void Awake()
    {
        Destroy(this.gameObject, ASTEROID_LIFESPAN);
        SetAsteroidData();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer != 8)
            return;
        Destroy(other.gameObject);  //destroy the bullet

        Split(GetCurrentSizeRange());
        
        Destroy(this.gameObject);
    }
}
