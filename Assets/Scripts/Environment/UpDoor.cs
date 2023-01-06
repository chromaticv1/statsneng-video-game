using System.Collections;
using UnityEngine;

public class UpDoor : MonoBehaviour
{

    public float speed;
    public float height;
    Toggler toggler;
    bool isWorking;
    
    public AudioClip clip1;
    public AudioClip clip2;
    AudioSource source;
    

    float oldY;

    private void Start() {
        toggler = this.GetComponent<Toggler>();
        Toggler.triggered += Toggle;
        oldY = transform.position.y;
        source = this.GetComponent<AudioSource>();
    }

    void Toggle(bool t_, string s_) {
        if (!s_.Equals(toggler.id)) return;

        //Coroutine here
        if (t_) {
            StopAllCoroutines();
            StartCoroutine(DoorUpAnimation());
            if(source.isPlaying){source.Stop();}
            source.PlayOneShot(clip1);
        } else if (!t_) {
            StopAllCoroutines();
            StartCoroutine(DoorDownAnimation());
            if(source.isPlaying){source.Stop();}
            source.PlayOneShot(clip2);
        }
    }

    IEnumerator DoorUpAnimation()
    {
        isWorking = true;

        while (transform.position.y != oldY + height) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, oldY + height, transform.position.z), speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        isWorking = false;
    }

    IEnumerator DoorDownAnimation()
    {
        isWorking = true;

        while (transform.position.y != oldY) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, oldY, transform.position.z), speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        isWorking = false;
    }    
}
