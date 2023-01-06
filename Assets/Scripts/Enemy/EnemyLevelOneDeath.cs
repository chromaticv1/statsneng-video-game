using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevelOneDeath : MonoBehaviour
{
    public GameObject particlePrefab;
    public Transform smokePoint;
    public GameObject backCube;
    Rigidbody rb;

    public static event Action<int> deathPing;

    bool isRunning = false;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("Pickable")) {
            if (isRunning) return;
            Instantiate(particlePrefab, smokePoint.position, smokePoint.rotation);
            Retardation();
            isRunning = true;
            Destroy(backCube);
            deathPing?.Invoke(3);
            Destroy(gameObject);
        }
    }

    void Retardation() {
        rb.isKinematic = false;
        rb.useGravity = true;
    }
}
