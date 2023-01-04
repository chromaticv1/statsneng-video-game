using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float buffer = 0.5f;
    [SerializeField] float dipSpeed = 2f;
    [SerializeField] GameObject cannon;
    [SerializeField] Transform gunTransform;
    float time;
    float originalY; 

    Transform player;
    public float pickupRange;

    // Start is called before the first frame update
    void Start()
    {
        originalY = transform.position.y;
        player = FindObjectOfType<ObjectPicking>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
        transform.position = new Vector3(transform.position.x, originalY + MathF.Sin(time*dipSpeed) * buffer, transform.position.z);
    }

    private void FixedUpdate() {
        if((player.position - transform.position).magnitude <= pickupRange) {
            GameObject cannonS = (GameObject)Instantiate(cannon, gunTransform.position, Quaternion.Euler(0, -95, 20));
            cannonS.GetComponent<Spinner>().enabled = false;
            cannonS.transform.SetParent(gunTransform);
            Destroy(gameObject);
        }

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}
