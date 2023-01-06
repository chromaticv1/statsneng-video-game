using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip clip;
    AudioSource source;

    private void Start() {
        source = this.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) {
        if (transform.localScale.x >= 2f) {
            source.volume = 1.5f;
            source.pitch = 0.5f;
            source.PlayOneShot(clip);
        } else if (transform.localScale.x <= 0.5f) {
            source.pitch = 1.8f;
            source.volume = 0.3f;
            PlayAudio();
        } else {
            source.pitch = 1f;
            source.volume = 0.93f;
            PlayAudio();
        }
    }

    public void PlayAudio() {
        source.PlayOneShot(clip);
    }
}
