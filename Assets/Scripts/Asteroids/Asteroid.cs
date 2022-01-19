using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    private const float DEFAULT_SIZE = 10f;
    private const float ASTEROID_LIFESPAN = 20f;

    private Rigidbody2D _rigidbody2D;
    private float size;

    private void Start()
    {
        Destroy(this.gameObject, ASTEROID_LIFESPAN);
    }

    /** <summary>Sets an asteroid's direction and speed, with a random size</summary> */
    public void Initialize(Vector2 impulseForce)
    {
        Initialize(Random.Range(6.5f, 12f), impulseForce);
    }
    
    /** <summary>Sets the asteroid's size, direction and speed.</summary>
     * <remarks>It is recomended to set the size in between the range defined in the previous Initialize method</remarks> */
    public void Initialize(float? size, Vector2 impulseForce)
    {
        if (size is null)
            this. size = DEFAULT_SIZE;
        else
        {
            this.size = size.Value;
            
            var localScaleMultiplier = this.size / DEFAULT_SIZE;
            transform.localScale = transform.localScale * localScaleMultiplier;
        }

        if (TryGetComponent<Rigidbody2D>(out _rigidbody2D))
            _rigidbody2D.AddForce(impulseForce, ForceMode2D.Impulse);
        else
            Debug.LogError("ASTEROID PREFAB DOES NOT HAVE A RigidBody2D COMPONENT");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer != 8)
            return;
        
        

        Destroy(this.gameObject);
    }
}
