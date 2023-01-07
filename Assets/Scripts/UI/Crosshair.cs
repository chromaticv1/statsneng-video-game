using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public Sprite grabbedSprite;
    public Sprite funnySprite;

    Image crosshairImage;
    RectTransform rect;

    private void Start() {
        crosshairImage = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        ObjectPicking.crosshairUpdater += UpdateCrosshair;
    }

    void UpdateCrosshair(int i_) {
        if (!crosshairImage) return;
        if (i_ == 2) {
            crosshairImage.sprite = grabbedSprite;
            crosshairImage.rectTransform.sizeDelta = new Vector2(32, 32);
        } else if (i_ == 1) {
            crosshairImage.sprite = funnySprite;
            crosshairImage.rectTransform.sizeDelta = new Vector2(32, 32);
        } else if (i_ == 0) {
            crosshairImage.sprite = null;
            crosshairImage.rectTransform.sizeDelta = new Vector2(3, 3);
        }
    }

}
