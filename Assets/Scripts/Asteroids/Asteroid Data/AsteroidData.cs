using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidData : MonoBehaviour
{
    [Header("Asteroid Size Ranges")] 
    [SerializeField] private Vector2 largeAsteroidSizeRange;
    [SerializeField] private Vector2 mediumAsteroidSizeRange;
    [SerializeField] private Vector2 smallAsteroidSizeRange;
    
    [Header("Asteroid Size Variants")] 
    public GameObject largeAsteroid;
    public GameObject mediumAsteroid;
    public GameObject smallAsteroid;
    
    [Header("Base Asteroid Sizes")] 
    public float largeAsteroidBaseSize;
    public float mediumAsteroidBaseSize;
    public float smallAsteroidBaseSize;

    [Space(10)] 
    
    public GameObject explosionPrefab;

    public Range<float> GetLargeRange()
        => new Range<float>(largeAsteroidSizeRange.x, largeAsteroidSizeRange.y);
    
    public Range<float> GetMediumRange()
        => new Range<float>(mediumAsteroidSizeRange.x, mediumAsteroidSizeRange.y);
    
    public Range<float> GetSmallRange()
        => new Range<float>(smallAsteroidSizeRange.x, smallAsteroidSizeRange.y);
}