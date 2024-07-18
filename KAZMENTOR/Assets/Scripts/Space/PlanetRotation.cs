using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed = 1f;


    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed);
        //transform.Translate(-moveSpeed, 0, 0);
    }
}
