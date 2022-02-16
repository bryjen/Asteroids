using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempScript2 : MonoBehaviour
{
    
    void Start()
    {
        var asteroidToCopy = transform.GetChild(0);

        for (int i = 0; i < 150; i++)
        {
            var newAsteroid = Instantiate(asteroidToCopy, transform.position, Quaternion.identity);
            newAsteroid.parent = this.transform;
        }
    }

}
