using UnityEngine;

public class CollectibleCounter : MonoBehaviour
{
    public bool[] levelCollectibles;
    
    private void Start() {
        DontDestroyOnLoad(this.gameObject);
        Collectible.collectibleAction += CollectibleTracker;
    }

    private void CollectibleTracker(int cId_)
    {
        levelCollectibles[cId_] = true;
    }

    public bool HasPickedCollectible(int collectibleIdentity_) {
        return levelCollectibles[collectibleIdentity_];
    }
}
