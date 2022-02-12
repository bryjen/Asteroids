using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private PlayerInput playerInput;

    #region event functions

    void Start()
    {
        playerInput = new PlayerInput();
        playerInput.BasicMovement.Enable();
        playerInput.BasicMovement.Shoot.performed += SpawnProjectile;
    }
    
    private void OnEnable()
    {
        if (playerInput == null)
            return;
        
        playerInput.BasicMovement.Enable();
    }

    private void OnDisable()
    {
        playerInput.BasicMovement.Disable();
    }

    #endregion
    
    private void SpawnProjectile(InputAction.CallbackContext obj)
    {
        var bullet = Instantiate(bulletPrefab, 
            transform.position, transform.rotation);
        
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * 12f, ForceMode2D.Impulse);
    }
}
