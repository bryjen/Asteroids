using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * <summary>
 * Handles/Executes proce
 * </summary>
 */
public class OnStartup : MonoBehaviour
{
    void Awake()
    {
        #region SPAWN PLACEHOLDER

            var spawnedPrefabsGameObject = new GameObject();
            spawnedPrefabsGameObject.name = "---- SPAWNED PREFABS ---- ";
            
            var bulletPlaceHolderGameObject = new GameObject();
            bulletPlaceHolderGameObject.name = "---- BULLETS ----";
            bulletPlaceHolderGameObject.transform.parent = spawnedPrefabsGameObject.transform;

        #endregion
        
    }
}
