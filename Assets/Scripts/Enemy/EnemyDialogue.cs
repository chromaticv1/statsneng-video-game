using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDialogue : MonoBehaviour
{
    public GameObject dialogueUI;
    public BoxCollider bCollider;
    public Canvas canvas;
    public GameObject dUI;
    bool hasTalked = false;

    public static event Action<bool> dialogueTrigger; 

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Default")) return;
        if (other.CompareTag("Player")) {
            print("yes?");
            Destroy(bCollider);
        }
        //GameObject i = (GameObject)Instantiate(dialogueUI, transform.position, Quaternion.identity);
        if (!dUI) return;
        dUI.SetActive(true);
        dUI.GetComponent<AnotherDialogue>().eDialogue = this;
        DToggler(true);
        hasTalked = true;
    }

    public void DToggler(bool b_) {        
        dialogueTrigger?.Invoke(b_);
    }
}
