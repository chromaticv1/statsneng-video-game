using UnityEngine;
using TMPro;
using System;

public class AnotherDialogue : MonoBehaviour
{
    [Header("Text Object Setup")]
    public TMP_Text nameText;
    public TMP_Text dialogue;

    [Header("Dialogue Config")]
    public int dialogueStart;
    public int currentDialogue;
    public DialogueChain[] dialogueChains;

    public EnemyDialogue eDialogue;

    public static event Action<int> toastPing;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.F)) {
            if (currentDialogue > dialogueChains.Length-1) {
                eDialogue.DToggler(false);
                toastPing?.Invoke(0);
                Destroy(gameObject);
                return;
            }
            UpdateDialogue(currentDialogue);
            currentDialogue++;
        }
    }

    private void UpdateDialogue(int currentDialogue_)
    {
        nameText.text = dialogueChains[currentDialogue_].userName;
        nameText.color = dialogueChains[currentDialogue_].nameColor;        
        dialogue.text = dialogueChains[currentDialogue_].dialogue;
    }
}