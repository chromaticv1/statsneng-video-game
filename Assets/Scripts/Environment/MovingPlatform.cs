using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Assign Case Here")]
    public float dipSpeed;
    public float buffer;
    public string direction;
    float time;
    float distance;

    private void Start() {
        switch (direction) {
            case "up":
                distance = transform.position.y;
                break;
            case "fwd":
                distance = transform.position.z;
                break;
            case "left":
                distance = transform.position.x;
                break;
            default:
            return;
         }
    }

    void Update()
    {
        if (gameObject.layer != 6) return;
        time += Time.deltaTime;
         switch (direction) {
            case "up":
                transform.position = new Vector3(transform.position.x, distance + MathF.Sin(time*dipSpeed) * buffer, transform.position.z);
                break;
            case "fwd":
                transform.position = new Vector3(transform.position.x , transform.position.y, distance + MathF.Sin(time*dipSpeed) * buffer);
                break;
            case "left":
                transform.position = new Vector3(distance + MathF.Sin(time*dipSpeed) * buffer , transform.position.y, transform.position.z);
                break;
            default:
            return;
         }
    }
}
