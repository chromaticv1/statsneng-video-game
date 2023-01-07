using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int collectibleID = 0;
    public static event Action<int> collectibleAction;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            collectibleAction?.Invoke(collectibleID);
            Destroy(gameObject);
        }
    }
}
