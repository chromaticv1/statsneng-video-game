using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int levelInt;

    private void OnTriggerEnter(Collider other) {
        print("not sex");
        if (other.CompareTag("Player")){
            SceneManager.LoadScene(levelInt);
            print("sex");
        }
    }
}
