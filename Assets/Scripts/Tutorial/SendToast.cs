using UnityEngine;

public class SendToast : MonoBehaviour
{
   public GameObject tutPanel;
   public TMPro.TMP_Text text;
   public bool isToastTime;

   private void Start() {
    AnotherDialogue.toastPing += PushToast;
    ObjectPicking.pickedCube += PushToast;
    EnemyLevelOneDeath.deathPing += PushToast;
    print("Toast Sender " + transform.name);
   }

   void PushToast(int tCount_) {
    if (!tutPanel) return;
    tutPanel.SetActive(true);
    UpdateCanvas(tCount_);
   }

   void UpdateCanvas(int i_) {
    if (i_ == 1) {
        text.text = "Look around to move the cube.\nLeft click to drop it.";
    } else if (i_ == 2) {
        text.text = "Pick it up again and\ndrop it on top of the Robot.";
    } else if (i_ == 3) {
        text.text = "To fix what Dr. Jyoti has done,\nlook around and go to the\nsource of the buffer overflow!";
        Destroy(tutPanel, 5f);
    }
   }
}
