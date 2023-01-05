using System;
using UnityEngine;

public class Toggler : MonoBehaviour
{
    public bool triggerToggle = false;
    public string id;
    public static event Action<bool, string> triggered;

    public void ToggleTrigger(bool trig_) {
        triggerToggle = trig_;
        triggered?.Invoke(triggerToggle, id);
    }
}
