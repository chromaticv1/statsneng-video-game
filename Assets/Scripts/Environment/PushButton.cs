using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    [SerializeField] [Tooltip("The main part which will be pushed down")]
    Transform button;
    [SerializeField] [Tooltip("The outline of the actual button")]
    GameObject border;

    [SerializeField] [Tooltip("The amount of depth to go down. Can be positive or negative")]
    float dipDistance;

    public Toggler toggler;
    bool isPressed = false;
    public AudioClip clip1;
    public AudioClip clip2;
    AudioSource source;

    private void Start() {
        dipDistance = transform.localScale.y * 0.085f;
        source = this.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) {
        if ((other.CompareTag("Player") || other.CompareTag("Pickable") || other.CompareTag("Picked")) && !isPressed) {
            isPressed = true;
            button.position = new Vector3 (button.position.x, button.position.y - Mathf.Abs(dipDistance), button.position.z);
            toggler.ToggleTrigger(true);
            source.PlayOneShot(clip1);
        }
    }

    void OnTriggerExit(Collider other) {
        if ((other.CompareTag("Player") || other.CompareTag("Pickable") || other.CompareTag("Picked")) && isPressed) {            
            isPressed = false;
            button.position = new Vector3 (button.position.x, button.position.y + Mathf.Abs(dipDistance), button.position.z);
            toggler.ToggleTrigger(false);
            source.PlayOneShot(clip2);
        }
    }
}
