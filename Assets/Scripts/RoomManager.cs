using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour {
    public delegate void RoomCleared(bool valid);
    public static RoomCleared OnRoomCleared;

    bool roomClear = false;

    private void OnEnable() {
        Button.OnButtonActivate += HandleButtonPress;
    }

    private void OnDisable() {
        Button.OnButtonActivate -= HandleButtonPress;
    }

    void HandleButtonPress(bool activated) {
        if (activated) {
            RoomClear();
        } else {
            RoomInvalid();
        }
    }

    void RoomClear() {
        OnRoomCleared?.Invoke(true);
    }

    void RoomInvalid() {
        OnRoomCleared?.Invoke(false);
    }
}
