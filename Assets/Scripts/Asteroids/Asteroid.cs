using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    [SerializeField] protected Sprite[] asteroidSprites;

    private const float BASE_SIZE = 8f;
    private const float MIN_SIZE = 3f;
    
    private Rigidbody2D _rigidbody2D;
    public float size;


    #region ASTEROID SPLITTING

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer != 8)    //8 being the layer of the bullet
                return;
            
            Destroy(other.gameObject);

            if (size / 2 < MIN_SIZE)
            {
                Destroy(gameObject);
                return;
            }
                
            
            int numberOfSmallerAsteroids = 
                (size / 3.5 > MIN_SIZE) ? 3 : 2;
    
            for (int i = 0; i < numberOfSmallerAsteroids; i++)
                Split(size, 
                    numberOfSmallerAsteroids == 3 ? 
                        Random.Range(2.5f, 3.3f): Random.Range(1.5f, 2f));
            
            Destroy(gameObject);
        }
    
        private void Split(float size, float shrinkFactor)
        {
            var asteroid = Instantiate(gameObject, transform.position, quaternion.identity);
            asteroid.transform.parent = transform.parent;
    
            asteroid.GetComponent<Asteroid>().Initialize(size / shrinkFactor, 
                Random.insideUnitCircle.normalized * Random.Range(1f, 2f));
        }

    #endregion

    #region INITIALIZATION
    
        //RANDOM SIZE
        public void Initialize(Vector2 impulseForce)
        {
            //generates random asteroid size
            Initialize(Random.Range(8f, 12f),
                impulseForce);
        }

        public void Initialize(float size, Vector2 impulseForce)
        {
            this.size = size;
    
            if (TryGetComponent<Rigidbody2D>(out _rigidbody2D))
                _rigidbody2D.AddForce(impulseForce, ForceMode2D.Impulse);
            else
                Debug.LogError("ASTEROID PREFAB DOES NOT HAVE A RigidBody2D COMPONENT");
    
            SetRandomSprite();
            AdjustSize();
            
            _rigidbody2D.angularVelocity = Random.Range(-55, 55);
        }
    
        

    #endregion

    private void AdjustSize()
        => transform.localScale *= size / BASE_SIZE;

    private void SetRandomSprite() =>
        GetComponent<SpriteRenderer>().sprite = asteroidSprites[Random.Range(0, asteroidSprites.Length - 1)];
}