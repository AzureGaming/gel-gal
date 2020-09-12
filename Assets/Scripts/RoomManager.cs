using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour {
    private void OnEnable() {
        ButtonTrigger.OnButtonActivate += HandleButtonPress;
    }

    private void OnDisable() {
        ButtonTrigger.OnButtonActivate -= HandleButtonPress;
    }

    void HandleButtonPress(bool activated) {
        if (activated) {
            Debug.Log("Pressed");
        } else {
            Debug.Log("Un preesseed");
        }
    }
}
