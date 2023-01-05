using UnityEngine;
using TMPro;

public class AnotherDialogue : MonoBehaviour
{
    [Header("Text Object Setup")]
    public TMP_Text nameText;
    public TMP_Text dialogue;

    [Header("Dialogue Config")]
    public int dialogueStart;
    public int currentDialogue;
    public DialogueChain[] dialogueChains;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.F)) {
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