using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject placeholderGameobject;
    
    private void Start()
    {
        placeholderGameobject = GameObject.Find("---- BULLETS ----");
        this.transform.parent = placeholderGameobject.transform;

        Destroy(this.gameObject, 5);
    }
}
