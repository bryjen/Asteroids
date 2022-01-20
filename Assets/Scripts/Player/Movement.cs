using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private PolygonCollider2D _collider;

    [Header("Movement Options")] 
    [SerializeField] private float thrustSpeed;
    [SerializeField, Tooltip("0-1"), Range(0, 1)] private float turnSpeed;

    [Header("Death Options")] 
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float respawnTimer;
    

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        var keyboard = Keyboard.current;
        
        //thrusting forward
        if (keyboard.wKey.isPressed)
        {
            if (_rigidbody.velocity.magnitude <= 4.5)
                _rigidbody.AddForce(transform.up * thrustSpeed);
        }
        
        if (keyboard.aKey.isPressed)
            _rigidbody.AddTorque(turnSpeed);
        if (keyboard.dKey.isPressed)
            _rigidbody.AddTorque(-turnSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer != 10)
            return;

        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion
            .transform.GetChild(0).gameObject
            .GetComponent<Animator>()
            .Play("explode");
        explosion
            .transform.localScale = new Vector3(5, 5, 1);
        Destroy(explosion, 1);
        
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.angularVelocity = 0;
        _collider.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
