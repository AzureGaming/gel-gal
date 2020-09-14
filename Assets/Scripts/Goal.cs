using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    public Animator top;
    public Animator bottom;

    private void OnEnable() {
        RoomManager.OnRoomCleared += HandleRoomClear;
    }

    void HandleRoomClear(bool valid) {
        if (valid) {
            Open();
        } else {
            Close();
        }
    }

    void Open() {
        top.SetTrigger("Open");
        bottom.SetTrigger("Open");
    }

    void Close() {
        top.SetTrigger("Close");
        bottom.SetTrigger("Close");
    }
}
