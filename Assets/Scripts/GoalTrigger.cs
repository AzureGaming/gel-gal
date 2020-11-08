using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour {
    public delegate void RoomClear();
    public static event RoomClear OnRoomClear;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GameManager.PLAYER_TAG)) {
            OnRoomClear?.Invoke();
        }
    }
}
