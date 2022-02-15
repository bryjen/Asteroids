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

    [HideInInspector] public bool isInvulnerablele;
    
    private PlayerDeathHandler playerDeathHandler;

    void Start()
    {
        playerDeathHandler = gameObject.GetComponent<PlayerDeathHandler>();
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

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer != 10)
            return;
        
        if (isInvulnerablele)
            return;
        
        playerDeathHandler.PlayExplosion(5);
        playerDeathHandler.ExecuteRespawnSequence();
    }
}
