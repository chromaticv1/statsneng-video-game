using UnityEngine;
using TMPro;

public class UIButton : MonoBehaviour
{
    public Color buttonDefault;
    public Color buttonHover;
    TMP_Text buttonText;
    void ButtonClick() {
        buttonText.color = buttonHover;
    }
}
