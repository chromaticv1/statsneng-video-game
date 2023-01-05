using UnityEngine;

[CreateAssetMenu(fileName = "DialogueChain", menuName = "Uh..What/DialogueChain", order = 1)]
public class DialogueChain : ScriptableObject
{
    public string userName;
    public Color nameColor;
    public string dialogue;
}