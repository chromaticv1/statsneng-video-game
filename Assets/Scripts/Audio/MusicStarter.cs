using UnityEngine;

public class MusicStarter : MonoBehaviour
{
    bool isMusicOn;
    public AudioSource source;
    public static MusicStarter Instance;
    public AudioClip music;

    private void Awake() {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void Start() {
        //ObjectPicking.pickedCube += MusicStart;
        DontDestroyOnLoad(this.gameObject);
    }

    void MusicStart(int i_) {
        //if (i_ != 1) return;
        //Music starts here

        source.PlayOneShot(music);
    }
}
