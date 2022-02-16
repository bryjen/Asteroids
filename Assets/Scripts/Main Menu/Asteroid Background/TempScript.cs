using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TempScript : MonoBehaviour
{
    [SerializeField] private float frequency;
    [SerializeField] private float radius;
    [SerializeField] private float offset;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        
        Gizmos.DrawWireSphere(transform.parent.position, radius);
        Gizmos.DrawSphere(transform.position, .2f);     //draws a dot at the center of the object
    }

    private void Start()
    {
        GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-50f, 50f);

        frequency += Random.Range(-.2f, .2f);
        radius += Random.Range(-1f, 1f);
        offset += Random.Range(-1f, 1f);
    }

    private void Update()
    {
        transform.localPosition = new Vector3(
            Mathf.Sin(Time.time * frequency + offset) * radius,
            Mathf.Cos(Time.time * frequency + offset) * radius,
            transform.localPosition.z
            );
    }
}
