using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour
{
    
    float time;
    float originalY;
    public float dipSpeed;
    public float buffer;
    void Start()
    {
        originalY = transform.position.y;
    }

    void Update()
    {        
        time += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, originalY + MathF.Sin(time*dipSpeed) * buffer, transform.position.z);
    }
}
