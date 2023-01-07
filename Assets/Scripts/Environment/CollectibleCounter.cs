using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectibleCounter : MonoBehaviour
{
    public TMPro.TMP_Text countText;
    public bool[] levelCollectibles;

    int collected = 0;

    int prevCollected = 0;
    
    private void Start() {
        if (SceneManager.GetActiveScene().buildIndex == 0) PlayerPrefs.SetInt("Collectibles", 0);
        prevCollected = PlayerPrefs.GetInt("Collectibles", 0);
        DontDestroyOnLoad(this.gameObject);
        Collectible.collectibleAction += CollectibleTracker;
        PauseMenu.isPauseMenuActive += TextEdit;
    }

    private void CollectibleTracker(int cId_)
    {
        levelCollectibles[cId_] = true;
    }

    public bool HasPickedCollectible(int collectibleIdentity_) {
        return levelCollectibles[collectibleIdentity_];
        PlayerPrefs.SetInt("Collectibles", collected);
    }

    void TextEdit(bool b_) {
        if (!b_) {
            countText.text = "";
        } else if (b_) {
            CCounter();
        }
    }

    void CCounter() {
        collected = 0;
        foreach (bool collectible in levelCollectibles) {
            if (collectible) {
                collected++;
            }
        }

        collected += prevCollected;

        countText.text = "Overflows collected: " + collected +"/" + levelCollectibles.Length;
        PlayerPrefs.SetInt("Collectibles", collected);
    }

}
